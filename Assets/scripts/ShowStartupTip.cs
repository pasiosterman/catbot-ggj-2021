using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class ShowStartupTip : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            Invoke("Tip", 1.0f);
        }

        public void Tip()
        {
            RoboGame.TipWindow.AddTip("You've lost your head! find it!");
            Destroy(gameObject, 0.1f);
        }
    }
}

