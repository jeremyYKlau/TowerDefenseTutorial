using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public string levelToLoad = "MainGame";
    public GameObject optionsUI;
    public Slider[] volumeSliders;

    public SceneFader sceneFader;

    private void Start()
    {
        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;
    }

    public void play()
    {
        sceneFader.fadeTo(levelToLoad);
    }

    public void quit()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }
    public void options()
    {
        optionsUI.SetActive(!optionsUI.activeSelf);

        if (optionsUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void setMasterVol(float value)
    {
        AudioManager.instance.setVolume(value, AudioManager.AudioChannel.Master);
    }
    public void setMusicVol(float value)
    {
        AudioManager.instance.setVolume(value, AudioManager.AudioChannel.Music);
    }
    public void setSfxVol(float value)
    {
        AudioManager.instance.setVolume(value, AudioManager.AudioChannel.Sfx);
    }
}
