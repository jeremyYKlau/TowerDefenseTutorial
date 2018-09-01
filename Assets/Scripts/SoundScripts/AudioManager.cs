using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public enum AudioChannel { Master, Sfx, Music };

    public float MasterVolumePercent { get; private set; }
    public float SfxVolumePercent { get; private set; }
    public float MusicVolumePercent { get; private set; }

    AudioSource sfx2Dsource;
    AudioSource[] musicScores;
    int activeMusicIndex;
    //this makes it so everyone function can access the audio manager which is what we want as this method only plays sounds and musics, it accesses no data
    public static AudioManager instance;

    Transform audioListener;//for player source noise but idk if i ever will use noise coming from a source
    Transform sourcePos;//same as above

    SoundLibrary library;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); //destroys duplicate sound managers however to stop overlapping of same noise
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //this is to make sure audio manager keeps playing between scene changes

            library = GetComponent<SoundLibrary>();
            musicScores = new AudioSource[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject newMusicSource = new GameObject("Music source " + (i + 1));
                musicScores[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
            }
            GameObject newSfx2DSource = new GameObject("2D sfx source ");
            sfx2Dsource = newSfx2DSource.AddComponent<AudioSource>();
            newSfx2DSource.transform.parent = transform;

            audioListener = FindObjectOfType<AudioListener>().transform;
            /*if (FindObjectOfType<Player>() != null)
            {
                playerPos = FindObjectOfType<Player>().transform;
            }*/
            MasterVolumePercent = PlayerPrefs.GetFloat("master volume", 1);
            SfxVolumePercent = PlayerPrefs.GetFloat("sfx volume", 1);
            MusicVolumePercent = PlayerPrefs.GetFloat("music volume", 1);
            PlayerPrefs.Save();
        }
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                MasterVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                SfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                MusicVolumePercent = volumePercent;
                break;
        }

        musicScores[0].volume = MusicVolumePercent * MasterVolumePercent;
        musicScores[1].volume = MusicVolumePercent * MasterVolumePercent;

        PlayerPrefs.SetFloat("master volume", MasterVolumePercent);
        PlayerPrefs.SetFloat("sfx volume", SfxVolumePercent);
        PlayerPrefs.SetFloat("music volume", MusicVolumePercent);
    }
    void Update()
    {
        if (sourcePos != null)
        {
            audioListener.position = sourcePos.position;
        }
    }
    public void playMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicIndex = 1 - activeMusicIndex;
        musicScores[activeMusicIndex].clip = clip;
        musicScores[activeMusicIndex].Play();

        StartCoroutine(MusicFade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, SfxVolumePercent * MasterVolumePercent);
        }
    }

    public void play2DSound(string soundName)
    {
        sfx2Dsource.PlayOneShot(library.getSoundName(soundName), SfxVolumePercent * MasterVolumePercent);
    }

    IEnumerator MusicFade(float duration)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicScores[activeMusicIndex].volume = Mathf.Lerp(0, MusicVolumePercent * MasterVolumePercent, percent);
            musicScores[1 - activeMusicIndex].volume = Mathf.Lerp(MusicVolumePercent * MasterVolumePercent, 0, percent);
            yield return null;
        }
    }
}
