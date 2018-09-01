using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public AudioClip gameMusic;
    public AudioClip menuMusic;

    string sceneName;

    void Awake()
    {
        SceneManager.sceneLoaded += LoadScene;
    }

    void Start()
    {
        SceneManager.sceneLoaded += LoadScene;
    }

    void LoadScene(Scene scene, LoadSceneMode mode)
    {
        string newSceneName = SceneManager.GetActiveScene().name;
        if (newSceneName != sceneName)
        { 
            sceneName = newSceneName;
            Invoke("PlayMusic", .5f); //invoked with small delay to avoid playing music before destroyed audio manager causing overlaps
        }
    }

    void PlayMusic()
    {
        AudioClip cliptoPlay = null;
        if (sceneName == "MainMenu" || sceneName == "LevelSelect")
        {
            cliptoPlay = menuMusic;
        }
        else
        {
            cliptoPlay = gameMusic;
        }

        if (cliptoPlay != null)
        {
            AudioManager.instance.playMusic(cliptoPlay, 2);
            Invoke("PlayMusic", cliptoPlay.length);
        }
    }
}
