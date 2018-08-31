using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject ui;
    public SceneFader sceneFader;

    public string menuSceneName = "MainMenu";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.P))
        {
            toggle();
        }
    }

    public void toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;   
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void menu()
    {
        toggle();
        sceneFader.fadeTo(menuSceneName);
    }

    public void retry()
    {
        toggle();
        sceneFader.fadeTo(SceneManager.GetActiveScene().name);
    }
}
