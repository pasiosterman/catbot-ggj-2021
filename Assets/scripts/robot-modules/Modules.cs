using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class Modules : MonoBehaviour, IStartup
    {
        private RobotModule[] modules;

        public void Startup()
        {
            modules = GetComponentsInChildren<RobotModule>(true);
        }

        public void ActivateModule(RobotModules moduletype)
        {
            for (int i = 0; i < modules.Length; i++)
            {
                if (modules[i].ModuleType == moduletype)
                {
                    if (moduletype == RobotModules.OverheadModule)
                        RoboGame.TipWindow.AddTip("Now.. to find that arm");

                    modules[i].gameObject.SetActive(true);
                }
            }
        }

        public bool HasModule(RobotModules moduleType)
        {
            for (int i = 0; i < modules.Length; i++)
            {
                if (modules[i].ModuleType == moduleType)
                    return true;
            }
            return false;
        }
    }
}