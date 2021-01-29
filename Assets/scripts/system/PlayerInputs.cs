using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class PlayerInputs : MonoBehaviour, IStartup
    {
        private BehaviorStartup _behaviorStartup;

        public void Startup()
        {
            RoboGame.AddTool(GameTools.PlayerInputs, this);
            _behaviorStartup = GetComponent<BehaviorStartup>();
        }

        public void Update()
        {
            if (_behaviorStartup == null) return;
            if (!_behaviorStartup.Ready) return;
            if (RoboGame.Player == null) return;

            RoboGame.Player.MoveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
