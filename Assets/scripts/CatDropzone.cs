using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2021
{
    public class CatDropzone : MonoBehaviour, IStartup
    {
        public List<Reward> rewards = new List<Reward>();
        private List<Cat> _cats = new List<Cat>();
        private OneShotSoundEffectPlayer _soundEffectPlayer;
        float timeStamp = -100.0f;

        public void Startup()
        {
            _soundEffectPlayer = GetComponent<OneShotSoundEffectPlayer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Cat cat = other.GetComponentInParent<Cat>();
            if (cat != null && !_cats.Contains(cat))
            {
                _cats.Add(cat);

                for (int i = rewards.Count - 1; i >= 0; i--)
                {
                    if (_cats.Count >= rewards[i].catRequirement)
                    {
                        if (rewards[i].module != null)
                            rewards[i].module.gameObject.SetActive(true);

                        if (rewards[i].WinGame)
                        {
                            RoboGame.TipWindow.AddTip("Contratulations! you found all the cats!");
                            Invoke("loadendscreen", 3.0f);
                        }

                        Debug.Log("Adding after reward tip!");
                        RoboGame.TipWindow.AddTip(rewards[i].message);
                        rewards.Remove(rewards[i]);
                    }
                }

                if (Time.time - timeStamp > 5.0f)
                {
                    Cat[] arr = FindObjectsOfType<Cat>();
                    RoboGame.TipWindow.AddTip("Found cats: " + _cats.Count + " / " + arr.Length);
                    _soundEffectPlayer.PlayRandomOneShot();
                    timeStamp = Time.time;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Cat cat = other.GetComponentInParent<Cat>();
            if (cat != null && _cats.Contains(cat))
                _cats.Remove(cat);
        }

        public void loadendscreen()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("YouWin");
        }
    }



    [System.Serializable]
    public class Reward
    {
        public int catRequirement = 0;
        public ModulePickUp module;
        [TextArea]
        public string message = "";
        public bool WinGame = false;
    }
}