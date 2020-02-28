using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangerScreenFader : MonoBehaviour
{
    public CanvasGroup uiElement;
    public GameObject dangerScreen;
    private bool stop = false;
    private int currentValue;

    void Awake()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
        //dangerScreen.SetActive(true);
    }

    public void startFade()
    {
        Debug.Log ("function startFade is working");
        //StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
        stop = false;
        StartCoroutine(fadeInFadeOut());
        //currentValue = 1;
        //StartCoroutine(FadeCanvasGroup());
    }

    public void stopFade()
    {
        Debug.Log ("function stopFade is working");
        //StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
        stop = true;
        StartCoroutine(fadeInFadeOut());
    }
    //public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1f)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }

        print("done");
    }

    private IEnumerator fadeInFadeOut()
    {
        if (stop == true)
        {
            yield break;
        }
        Debug.Log ("starting loop");
        
        
        //currentValue = 1;
        //StartCoroutine(FadeCanvasGroup());
        //StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
        
        yield return StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
        //StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
        Debug.Log ("fade in");
        //yield return new WaitForSeconds(1);
        
        //yield return new WaitForEndOfFrame();
        yield return StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
        //currentValue = 0;
        //StartCoroutine(FadeCanvasGroup());
        //StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
        Debug.Log ("fade out");
        //yield return new WaitForSeconds(1);
        //yield return new WaitForEndOfFrame();
    }
}
