using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class Player : MonoBehaviour, IStartup
    {
        public void Startup()
        {
            Mover = GetComponent<Mover>();
            Mover.ChangeState(new TankMovementState());
            RoboGame.AddTool(GameTools.Player, this);
            RoboGame.AddTool(GameTools.PlayerMover, Mover);
        }

        public Mover Mover { get; private set; }
    }
}
