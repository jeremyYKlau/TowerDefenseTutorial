using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

    public SceneFader fader;

    public Button[] levelButtons;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);//get saved data stored for player, level reached defaulted at level 1 and goes up as we complete levels


        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i +1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void levelSelect(string levelName)
    {
        fader.fadeTo(levelName);
    }

    public void toMainMenu(string mainMenu)
    {
        fader.fadeTo("MainMenu");
    }
}
