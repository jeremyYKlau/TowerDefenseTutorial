using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSuvived : MonoBehaviour {

    public Text roundsText;
    public int levelToUnlock = 2;

    void OnEnable()
    {
        StartCoroutine(AnimateText());
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.7f);

        while (round < PlayerStats.roundsComplete)
        {
            round++;
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(.05f);
        }
    }
}
