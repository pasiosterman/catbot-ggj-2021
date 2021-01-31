using UnityEngine;
using UnityEngine.Events;

namespace GGJ2021
{
    public class ModulePickUp : MonoBehaviour, IStartup
    {
        private OneShotSoundEffectPlayer _soundEffectPlayer;
        private bool _consumed = false;
        public RobotModules moduleType = RobotModules.None;
        public string moduleName = "";

        public void Startup()
        {
            _soundEffectPlayer = GetComponentInChildren<OneShotSoundEffectPlayer>();
        }

        public void Pickup()
        {
            _consumed = true;

            if(moduleName == null)
                moduleName = "new module";

            RoboGame.TipWindow.AddTip("Picked up: " + moduleName);
            RoboGame.Player.Modules.ActivateModule(moduleType);
            _soundEffectPlayer.PlayRandomOneShot();
            _soundEffectPlayer.transform.SetParent(null);
            Destroy(_soundEffectPlayer.gameObject, 5.0f);
            Destroy(gameObject, 0.1f);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_consumed)
                return;

            Player player = other.GetComponentInParent<Player>();
            if (player != null)
                Pickup();
        }
    }
}