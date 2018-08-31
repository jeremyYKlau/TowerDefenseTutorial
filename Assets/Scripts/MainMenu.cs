using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string levelToLoad = "MainGame";

    public SceneFader sceneFader;

    public void play()
    {
        sceneFader.fadeTo(levelToLoad);
    }

    public void quit()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
}
