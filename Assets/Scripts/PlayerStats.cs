using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int money;
    public int startMoney = 400;
    public static int lives;
    public int startLives = 10;

    public static int roundsComplete;

    void Start()
    {
        money = startMoney;
        lives = startLives;

        roundsComplete = 0;
    }
}
