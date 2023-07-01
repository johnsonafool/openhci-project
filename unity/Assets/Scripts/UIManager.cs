using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{   
    [SerializeField]
    TextMeshProUGUI LeftPercentage;
    [SerializeField]
    TextMeshProUGUI LeftPoints;
    [SerializeField]
    TextMeshProUGUI LeftMain;
    [SerializeField]
    TextMeshProUGUI RightPercentage;
    [SerializeField]
    TextMeshProUGUI RightPoints;
    [SerializeField]
    TextMeshProUGUI RightMain;
    
    [SerializeField]
    Slider Seesaw;

    int lastLeftMain = 50;
    int lastRightMain = 50;
    int lastLeftPerc = 0;
    int lastRightPerc = 0;

    void Start()
    {
        LeftPercentage.SetText("0%");
        RightPercentage.SetText("0%");
        Seesaw.value = 0.5f;
    }

    void Update()
    {
    }

    public void LeftUIupdate(int newValue)
    {
        LeftPercentage.SetText(newValue.ToString()+'%');
    }

    public void RightUIupdate(int newValue)
    {
        RightPercentage.SetText(newValue.ToString()+'%');
    }

    public void UIupdate(int leftPercentage, float leftPoints, int rightPercentage, float rightPoints)
    {
        LeftPoints.SetText(leftPoints.ToString());
        RightPoints.SetText(rightPoints.ToString());

        if(lastLeftPerc != leftPercentage) {
            StartCoroutine(SmoothChangePercL(leftPercentage));
        }
        // Debug.Log(lastRightPerc);
        // Debug.Log(rightPercentage);
        if(lastRightPerc != rightPercentage) {
            StartCoroutine(SmoothChangePercR(rightPercentage));
        }

        float NewSeesaw;
        if (leftPoints == 0 && rightPoints== 0)
        {
            NewSeesaw = 0.5f;
        }
        else
        {
            NewSeesaw = (float) leftPoints / (float) (leftPoints + rightPoints);
        }

        if(Seesaw.value < NewSeesaw)
        {
            StartCoroutine(SmoothSlideRight(NewSeesaw));
        } else
        {
            StartCoroutine(SmoothSlideLeft(NewSeesaw));
        }
    }

    IEnumerator SmoothChangeMain(int left, int right)
    {
        float lerpTime = 1f;
        float currentLerpTime = 0f;
        int ml = lastLeftMain;
        int mr = lastRightMain;

        while(ml != left || mr != right)
        {   
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;
            perc = Mathf.Sin(perc * Mathf.PI * 0.5f);
            
            if (lastLeftMain != left)
            {
                ml = (int) Mathf.Lerp(lastLeftMain, left, perc);
                LeftMain.SetText(ml.ToString()+'%');
            }
            
            if (lastRightMain != right)
            {
                mr = (int) Mathf.Lerp(lastRightMain, right, perc);
                RightMain.SetText(mr.ToString()+'%');
            }
            

            yield return null;
        }
        lastLeftMain = left;
        lastRightMain = right;
        yield return null;
    }

    IEnumerator SmoothChangePercL(int leftPercentage)
    {
        float lerpTime = 1f;
        float currentLerpTime = 0f;
        int pl = lastLeftPerc;

        while(pl != leftPercentage)
        {   
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;
            perc = Mathf.Sin(perc * Mathf.PI * 0.5f);
            
            if (lastLeftPerc != leftPercentage)
            {
                pl = (int) Mathf.Lerp(lastLeftPerc, leftPercentage, perc);
                LeftPercentage.SetText(pl.ToString()+'%');
            }
            yield return null;
        }
        LeftPercentage.SetText(leftPercentage.ToString()+'%');
        lastLeftPerc = leftPercentage;
        yield return null;
    }

    IEnumerator SmoothChangePercR(int rightPercentage)
    {
        float lerpTime = 1f;
        float currentLerpTime = 0f;
        int pr = lastRightPerc;

        while(pr != rightPercentage)
        {   
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;
            perc = Mathf.Sin(perc * Mathf.PI * 0.5f);
            
            if(lastRightPerc != rightPercentage)
            {
                pr = (int) Mathf.Lerp(lastRightPerc, rightPercentage, perc);
                RightPercentage.SetText(pr.ToString()+'%');
            }

            yield return null;
        }
        RightPercentage.SetText(rightPercentage.ToString()+'%');
        lastRightPerc = rightPercentage;
        yield return null;
    }

    IEnumerator SmoothSlideLeft(float targetValue)
    {
        float startValue = Seesaw.value;
        
        float lerpTime = 1f;
        float currentLerpTime = 0f;
        
        while(Seesaw.value > targetValue)
        {   
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;
            perc = Mathf.Sin(perc * Mathf.PI * 0.5f);
            Seesaw.value = Mathf.Lerp(startValue, targetValue, perc);
            int showNumL = (int) Mathf.Round(100 * Seesaw.value);
            int showNumR = 100-showNumL;
            LeftMain.SetText(showNumL.ToString()+'%');
            RightMain.SetText(showNumR.ToString()+'%');
            yield return null;
        }
        yield return null;
    }

    IEnumerator SmoothSlideRight(float targetValue)
    {
        float startValue = Seesaw.value;
        
        float lerpTime = 1f;
        float currentLerpTime = 0f;
        
        while(Seesaw.value < targetValue)
        {   
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime) {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;
            perc = Mathf.Sin(perc * Mathf.PI * 0.5f);
            Seesaw.value = Mathf.Lerp(startValue, targetValue, perc);
            
            int showNumL = (int) Mathf.Round(100 * Seesaw.value);
            int showNumR = 100-showNumL;
            LeftMain.SetText(showNumL.ToString()+'%');
            RightMain.SetText(showNumR.ToString()+'%');
            yield return null;
        }
        yield return null;
    }

    void RedEffect() {

    }
}
