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
        volumeSliders[0].value = AudioManager.instance.MasterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.MusicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.SfxVolumePercent;
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
    public void SetMasterVol(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }
    public void SetMusicVol(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }
    public void SetSfxVol(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }
}
