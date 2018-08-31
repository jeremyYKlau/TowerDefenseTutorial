using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class is just to store information
[System.Serializable]
public class Wave {

    //public GameObject enemyPrefab 2 then choose between the 2 or make it a list and randomly pick between the list
    public GameObject enemyPrefab;
    public int enemyCount;
    public float spawnRate;

}
