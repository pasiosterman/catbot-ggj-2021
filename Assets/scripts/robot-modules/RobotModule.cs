using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class RobotModule : MonoBehaviour
    {
        public virtual RobotModules ModuleType { get { return RobotModules.None; } }
    }

    public enum RobotModules
    {
        None = 0,
        CarryModule = 1,
        JetbackModule = 2
    }
}