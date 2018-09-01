using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesLeftUI : MonoBehaviour {

    public Text enemiesLeft;
    public WaveSpawner waveSpawner;
	
	// Update is called once per frame
	void Update () {
        enemiesLeft.text =  waveSpawner.getEnemyCount() + " ENEMIES LEFT";//this converts string automatically without ToString
	}
}
