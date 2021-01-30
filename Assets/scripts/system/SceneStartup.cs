using UnityEngine;
using System.Linq;

namespace GGJ2021
{
    [DefaultExecutionOrder(-100)]
    public class SceneStartup : MonoBehaviour
    {
        public void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            RoboGame.tools.ClearTools();

            BehaviorStartup[] objs = FindObjectsOfType<BehaviorStartup>();
            objs = objs.OrderBy(x => x.Priority).ToArray();
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].Startup();
            }
        }

        private void OnApplicationQuit()
        {
            RoboGame.ApplicationQuitting = true;
        }
    }
}