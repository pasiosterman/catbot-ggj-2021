using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class CarryObjectsModule : RobotModule
    {
        public GameObject leftHand;

        public readonly List<IPickable> pickablesNearby = new List<IPickable>();
        public IPickable CurrentPickable { get; private set; }
        private Player _player;
        float max_distance = 5.0f;

        public void UseModule()
        {
            if (CurrentPickable == null)
                PickUpNearest();
            else
                DropCurrentPickable();
        }

        private void DropCurrentPickable()
        {
            CurrentPickable.transform.SetParent(null);
            CurrentPickable.transform.position = transform.position + transform.forward;
            CurrentPickable.OnPutDown();
            CurrentPickable = null;
        }

        private void PickUpNearest()
        {
            if (CurrentPickable != null)
                return;

            IPickable nearest = GetNearestPickable();
            if (nearest != null)
            {
                CurrentPickable = nearest;
                CurrentPickable.OnPickUp();
                CurrentPickable.transform.SetParent(transform);
                CurrentPickable.transform.localPosition = Vector3.zero;
                CurrentPickable.transform.forward = transform.forward;
            }
        }

        public IPickable GetNearestPickable()
        {
            if (pickablesNearby.Count == 0) return null;

            CleanUpBuggedOutCatsNotReallyNearby();

            IPickable nearest = pickablesNearby[0];
            float nearestDist = Vector3.Distance(nearest.transform.position, transform.position);

            if (pickablesNearby.Count > 1)
            {
                for (int i = 1; i < pickablesNearby.Count; i++)
                {
                    float dist = Vector3.Distance(pickablesNearby[i].transform.position, transform.position);
                    if (dist < nearestDist)
                    {
                        nearest = pickablesNearby[i];
                        nearestDist = dist;
                    }
                }
            }

            return nearest;
        }

        private void CleanUpBuggedOutCatsNotReallyNearby()
        {
            for (int i = pickablesNearby.Count - 1; i >= 0; i--)
            {
                IPickable it = pickablesNearby[i];
                if(it == null)
                    Debug.Log("wat");

                float dist = Vector3.Distance(it.transform.position, _player.transform.position);
                Vector3 nearestDir = (it.transform.position - _player.transform.position).normalized;
                nearestDir.y = 0.0f;
                if (dist > max_distance || Vector3.Dot(nearestDir, transform.forward) < 0.0f)
                    pickablesNearby.Remove(it);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            IPickable pickable = other.GetComponent<IPickable>();
            if (pickable == null)
                pickable = other.GetComponentInParent<IPickable>();

            if (pickable != null && !pickablesNearby.Contains(pickable))
            {
                pickablesNearby.Add(pickable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IPickable pickable = other.GetComponentInParent<IPickable>();
            if (pickable != null && pickablesNearby.Contains(pickable))
            {
                pickablesNearby.Remove(pickable);
            }
        }

        public override RobotModules ModuleType {get{ return RobotModules.CarryModule; }}

        protected void OnEnable()
        {
            _player = GetComponentInParent<Player>();
            _player.CarryObjectsModule = this;
            leftHand.gameObject.SetActive(true);
            RoboGame.TipWindow.AddTip("Time to find and bring those cats back here");
            RoboGame.TipWindow.AddTip("Pick up cats using the F-key or left mouse button");
        }
    }

    public interface IPickable
    {
        void OnPickUp();
        void OnPutDown();
        bool CanPickUp { get; }
        bool IsHeavy { get; }
        Transform transform { get; }
    }
}