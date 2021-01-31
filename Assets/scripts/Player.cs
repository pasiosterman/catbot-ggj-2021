using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class Player : MonoBehaviour, IStartup
    {
        public Mover Mover { get; private set; }
        public CarryObjectsModule CarryObjectsModule { get; set; }
        public Modules Modules { get; private set; }

        public void Startup()
        {
            Modules = GetComponentInChildren<Modules>();
            Mover = GetComponent<Mover>();
            Mover.ChangeState(new StrafeMovementState());
            RoboGame.AddTool(GameTools.Player, this);
            RoboGame.AddTool(GameTools.PlayerMover, Mover);
        }
    }
}