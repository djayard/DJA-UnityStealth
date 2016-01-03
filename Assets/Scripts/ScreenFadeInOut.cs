using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    private bool sceneStarting = true;

    void Update()
    {
        if( sceneStarting )
        {
            StartScene();
        }

    }

    void FadeToClear()
    {
        GetComponent<RawImage>().color = Color.Lerp(GetComponent<RawImage>().color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        GetComponent<RawImage>().color = Color.Lerp(GetComponent<RawImage>().color, Color.black, fadeSpeed * 2 * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();

        if( GetComponent<RawImage>().color.a <= 0.05f)
        {
            GetComponent<RawImage>().color = Color.clear;
            GetComponent<RawImage>().enabled = false;
            sceneStarting = false;
        }
    }

    public void EndScene()
    {
        GetComponent<RawImage>().enabled = true;
        FadeToBlack();

        if(GetComponent<RawImage>().color.a >= 0.95f)
        {
            SceneManager.LoadScene(1);
        }
    }
}
