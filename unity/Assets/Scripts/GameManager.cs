using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float LeftPointsAll = 0f;
    public float LeftPointsCorrect = 0f;
    public int LeftPercentage = 0;

    public float RightPointsAll = 0f;
    public float RightPointsCorrect = 0f;
    public int RightPercentage = 0;

    public bool hasRecycle = false;
    public bool hasNormal = false;

    [SerializeField]
    UIManager uIManager;
    [SerializeField]
    Chicken leftChicken;
    [SerializeField]
    Chicken rightChicken;
    [SerializeField]
    Animator Danmaku;
    [SerializeField]
    Answer circle;
    [SerializeField]
    Answer cross;
    [SerializeField]
    Answer RB;
    [SerializeField]
    AudioSource scream;


    const int powerCorrect = 3;
    const int powerRivalCorrect = 3;
    int accumulatedCorrect = 0;
    int accumulatedRivalCorrect = 0;

    void Start()
    {  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowRecycle();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ThrowNormal();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            RivalThrowRecycle();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            RivalThrowNormal();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            ResetToZero();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Randomize(100);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Randomize(1000);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            Randomize(10000);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            PowerAttack();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            RivalPowerAttack();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Danmaku.SetTrigger("scroll");
        }
        else if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void UIupdate() {
        // Debug.Log(LeftPercentage.ToString()+", "+LeftPointsCorrect.ToString()+", "+RightPercentage.ToString()+", "+RightPointsCorrect);
        uIManager.UIupdate(LeftPercentage,LeftPointsCorrect,RightPercentage,RightPointsCorrect);
    }

    public void ThrowRecycle()
    {
        if (hasRecycle == false)
        {
            accumulatedCorrect += 1;
            if (accumulatedCorrect >= powerCorrect)
            {
                PowerAttack();
                accumulatedCorrect = 0;
            }
            else
            {
                leftChicken.Attack();
                rightChicken.Hurt();
            }
            circle.Show();
            hasRecycle = true;
            LeftPointsAll += 1f;
            LeftPointsCorrect += 1f;
            LeftPercentage = (int) Mathf.Round(100 * LeftPointsCorrect / LeftPointsAll);
            UIupdate();
        }
    }

    public void ThrowNormal()
    {
        if (hasNormal == false)
        {
            accumulatedCorrect = 0;
            cross.Show();
            leftChicken.Hurt();
            hasNormal = true;
            LeftPointsAll += 1f;
            LeftPercentage = (int) Mathf.Round(100 * LeftPointsCorrect / LeftPointsAll);
            UIupdate();
        }
    }

    public void ClearRecycle()
    {
        if (hasRecycle == true)
        {
            hasRecycle = false;
        }
    }

    public void ClearNormal()
    {
        if (hasNormal == true)
        {
            hasNormal = false;
        }
    }

    void RivalThrowRecycle()
    {
        accumulatedRivalCorrect += 1;
        if (accumulatedRivalCorrect >= powerRivalCorrect)
        {
            RivalPowerAttack();
            accumulatedRivalCorrect = 0;
        }
        else
        {
            rightChicken.Attack();
            leftChicken.Hurt();
        }
        
        RightPointsAll += 1.0f;
        RightPointsCorrect += 1.0f;
        RightPercentage = (int) Mathf.Round(100 * RightPointsCorrect / RightPointsAll);
        UIupdate();
    }

    void RivalThrowNormal()
    {
        accumulatedRivalCorrect = 0;
        RightPointsAll += 1.0f;
        RightPercentage = (int) Mathf.Round(100 * RightPointsCorrect / RightPointsAll);
        UIupdate();
    }

    void ResetToZero()
    {
        LeftPointsAll = 0f;
        LeftPointsCorrect = 0f;
        LeftPercentage = 0;

        RightPointsAll = 0f;
        RightPointsCorrect = 0f;
        RightPercentage = 0;
        UIupdate();
    }

    void Randomize(int maxValue)
    {
        LeftPointsAll = (float) Random.Range(1, maxValue);
        LeftPointsCorrect = (float) Random.Range((int)LeftPointsAll/2, (int)LeftPointsAll);
        

        RightPointsAll = (float) Random.Range(1, maxValue);
        RightPointsCorrect = (float) Random.Range((int)RightPointsAll/2, (int)RightPointsAll);
        LeftPercentage = (int) Mathf.Round(100 * LeftPointsCorrect / LeftPointsAll);
        RightPercentage = (int) Mathf.Round(100 * RightPointsCorrect / RightPointsAll);
        UIupdate();
    }

    void PowerAttack()
    {
        leftChicken.PowerAttack();
        rightChicken.PowerHurt();
        RB.Show();
        StartCoroutine(ScreamTimeAdjust());
    }

    void RivalPowerAttack()
    {
        rightChicken.PowerAttack();
        leftChicken.PowerHurt();
        RB.Show();
        StartCoroutine(ScreamTimeAdjust());
    }

    IEnumerator ScreamTimeAdjust() {
        yield return new WaitForSeconds(1.1f);
        scream.Play();
        yield return null;
    }
}
