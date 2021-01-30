using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class CarryObjects : RobotModule, IStartup
    {
        private readonly List<IPickable> pickablesNearby = new List<IPickable>();
        public IPickable CurrentPickable { get; private set; }

        public override void UseModule()
        {
            if(CurrentPickable == null)
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

            IPickable nearest = pickablesNearby[0];
            float nearestDist = Vector3.Distance(nearest.transform.position, transform.position);

            if (pickablesNearby.Count == 1)
                return nearest;

            for (int i = 1; i < pickablesNearby.Count; i++)
            {
                float dist = Vector3.Distance(pickablesNearby[i].transform.position, transform.position);
                if (dist < nearestDist)
                {
                    nearest = pickablesNearby[i];
                    nearestDist = dist;
                }
            }
            return nearest;
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
            IPickable pickable = other.GetComponent<IPickable>();
            if (pickable != null && pickablesNearby.Contains(pickable))
            {
                pickablesNearby.Remove(pickable);
            }
        }

        public void Startup()
        {

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