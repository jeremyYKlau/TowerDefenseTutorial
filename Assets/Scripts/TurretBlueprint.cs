using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need this to show in inspector
[System.Serializable]
public class TurretBlueprint{

    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int getSellPrice()
    {
        return cost / 2;
    }

}
