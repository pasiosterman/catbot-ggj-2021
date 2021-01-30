using UnityEngine;

namespace GGJ2021
{
    public abstract class RobotModule : MonoBehaviour
    {
        public virtual void UseModule() { }
        public virtual void OnEquipModule() { }
        public virtual void OnUnequipModule() { }

        protected virtual void OnEnable()
        {
            Player player = GetComponentInParent<Player>();
            player.AddModule(this);
        }

        protected virtual void OnDisable()
        {
            if (RoboGame.ApplicationQuitting)
                return;

            Player player = GetComponentInParent<Player>();
            player.RemoveModule(this);
        }
    }
}