using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameIsOver;//static variable carrys over between scenes so we have start reset to false

    public GameObject gameOverUi;
    public GameObject levelCompleteUI;

    void Start()
    {
        gameIsOver = false;   
    }

    // Update is called once per frame
    void Update () {
        if (gameIsOver)
        {
            return;
        }
		if(PlayerStats.lives <= 0)
        {
            gameOver();
        }
	}

    public void gameOver()
    {
        gameIsOver = true;
        gameOverUi.SetActive(true);
    }

    public void levelComplete()
    {
        gameIsOver = true;
        levelCompleteUI.SetActive(true);
    }
}
