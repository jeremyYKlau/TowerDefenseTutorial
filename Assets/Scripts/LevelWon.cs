using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWon : MonoBehaviour {

    public string nextLevel = "Level2";

    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    public void menu()
    {
        sceneFader.fadeTo(menuSceneName);
    }
    //continue is a keyword so i can't use it as a method name
    public void continueOn()
    {
        sceneFader.fadeTo(nextLevel);
    }
}
