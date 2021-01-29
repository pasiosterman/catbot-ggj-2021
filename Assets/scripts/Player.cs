using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class Player : MonoBehaviour, IStartup
    {
        public Vector3 MoveDir { get; set; } = Vector3.zero;

        private BehaviorStartup _behaviorStartup;
        private Rigidbody _rb;

        public float speed = 5.0f;

        public void Startup()
        {
            _behaviorStartup = GetComponent<BehaviorStartup>();
            _rb = GetComponent<Rigidbody>();
            RoboGame.AddTool(GameTools.Player, this);
        }

        public void FixedUpdate()
        {
            if (_behaviorStartup == null) return;
            if (!_behaviorStartup.Ready ) return;
            if (_rb == null) return;

            Vector3 velocity = new Vector3(MoveDir.x, 0, MoveDir.y).normalized;
            velocity *= speed;
            _rb.velocity = velocity;
        }
    }
}
