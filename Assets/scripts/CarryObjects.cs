using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class CarryObjects : MonoBehaviour
    {
        public Transform carryTransform;
        private readonly List<IPickable> pickablesNearby = new List<IPickable>();

        public IPickable CurrentPickable { get; private set; }

        public void PickUpNearest()
        {
            IPickable nearest = GetNearestPickable();
            if(nearest != null)
            {
                nearest.OnPickUp();
                nearest.transform.SetParent(carryTransform);
                nearest.transform.position = Vector3.zero;
            }
        }

        public IPickable GetNearestPickable()
        {
            if(pickablesNearby.Count == 0) return null;

            IPickable nearest = pickablesNearby[0];
            float nearestDist = Vector3.Distance(nearest.transform.position, transform.position);

            if(pickablesNearby.Count == 1)
                return nearest;

            for (int i = 1; i < pickablesNearby.Count; i++)
            {
                float dist = Vector3.Distance(pickablesNearby[i].transform.position, transform.position);
                if(dist < nearestDist)
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
    }

    public interface IPickable
    {
        void OnPickUp();
        void OnPutDown();
        bool CanPickUp { get; }
        bool IsHeavy { get; }
        Transform transform {get; }
    }
}