using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class Player : MonoBehaviour, IStartup
    {
        public List<RobotModule> modules = new List<RobotModule>();
        public Mover Mover { get; private set; }

        public RobotModule CurrenModule { get; private set; }

        public void Startup()
        {
            Mover = GetComponent<Mover>();
            Mover.ChangeState(new StrafeMovementState());
            RoboGame.AddTool(GameTools.Player, this);
            RoboGame.AddTool(GameTools.PlayerMover, Mover);
        }

        public void UseModule()
        {
            CurrenModule?.UseModule();
        }

        public void AddModule(RobotModule module)
        {
            if (!modules.Contains(module) && module != null)
            {
                modules.Add(module);
                if(CurrenModule == null)
                    CurrenModule = module;
            }
        }

        public void RemoveModule(RobotModule module)
        {
            if (modules.Contains(module))
                modules.Remove(module);
        }
    }
}
