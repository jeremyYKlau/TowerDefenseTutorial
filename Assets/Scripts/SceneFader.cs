using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    public Image img;
    public AnimationCurve fadeCurve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void fadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0)
        {
            t -= Time.deltaTime; //can also multiply by 1 if you want it in 1 second or 2 in half a second for while loop
            float a = fadeCurve.Evaluate(t);//art stuff for animation curve based off fade time to give us more control, personally unneeded for me but i'll keep it for now
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime; //can also multiply by 1 if you want it in 1 second or 2 in half a second for while loop
            float a = fadeCurve.Evaluate(t);//art stuff for animation curve based off fade time to give us more control, personally unneeded for me but i'll keep it for now
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        SceneManager.LoadScene(scene);
    }

}
