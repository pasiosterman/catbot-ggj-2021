using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class JetbackModule : RobotModule
    {
        public GameObject jetback;
        public override RobotModules ModuleType { get{ return RobotModules.JetbackModule; }}

        private void OnEnable() 
        {
            Player player = GetComponentInParent<Player>();
            player.Mover.canJump = true;
            jetback?.SetActive(true);
        }
    }
}