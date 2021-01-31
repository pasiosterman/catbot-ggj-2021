using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject EntryScreen;
    public GameObject LoadingScreen;

    float t = 0.0f;
    bool loading = false;

    // Start is called before the first frame update
    void Start()
    {
        EntryScreen.gameObject.SetActive(true);
        LoadingScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if(t < 2.0f || loading)
            return;

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            LoadingScreen.gameObject.SetActive(true);
            EntryScreen.gameObject.SetActive(false);
            loading = true;
            Invoke("LoadScene", 0.3f);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Level");
    }
}
