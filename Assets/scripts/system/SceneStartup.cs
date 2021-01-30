using UnityEngine;
using System.Linq;

namespace GGJ2021
{
    [DefaultExecutionOrder(-100)]
    public class SceneStartup : MonoBehaviour
    {
        public void Start()
        {
            Debug.Log(LogTags.SYSTEM + "Scene startup!");
            RoboGame.tools.ClearTools();

            BehaviorStartup[] objs = FindObjectsOfType<BehaviorStartup>();
            objs = objs.OrderBy(x => x.Priority).ToArray();
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].Startup();
            }
        }
    }
}