using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2021
{
    public class TipWindow : MonoBehaviour, IStartup
    {
        public Text tipElement;
        public List<string> TipPool = new List<string>();
        public float tipDuration = 5.0f;
        public float fadeDuration = 0.3f;

        public DisplayTip CurrenTip { get; private set; }

        private float timer = 0.0f;
        private CanvasGroup _canvasGroup;

        [ContextMenu("test tip")]
        public void TestTip()
        {
            AddTip("testing testing...");
        }

        void Update()
        {
            if (CurrenTip == null)
            {
                if (TipPool.Count > 0)
                {
                    CurrenTip = new DisplayTip() { tip = TipPool[0], lifetime = tipDuration };
                    tipElement.text = CurrenTip.tip;
                    _canvasGroup.alpha = 0.0f;
                    TipPool.RemoveAt(0);
                }
            }
            else
            {
                switch (CurrenTip.state)
                {
                    case 0:
                        timer += Time.deltaTime;
                        if (timer > fadeDuration)
                        {
                            timer = fadeDuration;
                            CurrenTip.state = 1;
                        }
                        _canvasGroup.alpha = timer / fadeDuration;
                        break;
                    case 1:
                        CurrenTip.lifetime -= Time.deltaTime;
                        if (CurrenTip.lifetime < 0.0f)
                            CurrenTip.state = 2;
                        break;
                    case 2:
                        timer -= Time.deltaTime;
                        if (timer < 0.0f)
                            CurrenTip = null;

                        _canvasGroup.alpha = timer / fadeDuration;
                        break;
                    default:
                        break;
                }
            }
        }

        public void AddTip(string tip)
        {
            TipPool.Add(tip);
        }

        public void Startup()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            RoboGame.AddTool(GameTools.TipWindow, this);
        }

        public class DisplayTip
        {
            public int state = 0;
            public string tip = "";
            public float lifetime = 0.0f;
        }
    }
}


