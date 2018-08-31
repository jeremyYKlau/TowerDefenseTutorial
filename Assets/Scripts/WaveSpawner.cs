using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public static int enemiesAlive = 0;

    public Wave[] waves;
    public Transform spawnPoint;

    public float timeBetweenWaves = 10f; //time between waves though in my version i'd rather have a button to press
    private float countdown = 3f; //same as above except for the first wave

    public Text waveCountdownText;
    public GameManager gameManager;

    private int waveIndex = 0;

    void Update()
    {
        //make sure new wave doesn't spawn until all enemies of current wave are gone from scene
        //return statements are important because the order of these if statement checks are important to prevent going further into the functions functionality
        if (enemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length && PlayerStats.lives != 0)
        {
            gameManager.levelComplete();
            this.enabled = false;
        }

        if (countdown<= 0)
        {
            StartCoroutine(spawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    //coroutine used to spawn separate from the main game to not spawn duplicates
    IEnumerator spawnWave()
    {
        PlayerStats.roundsComplete++;
        Wave wave = waves[waveIndex];
        enemiesAlive = wave.enemyCount;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            spawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f/wave.spawnRate);//delayed by wave.spawnrate in the wave class
        }
        waveIndex++;
    }

    void spawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
