using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seesaw : MonoBehaviour
{
    public Slider LeftHP;
    public Text LeftHpText, RightHpText;
    // Start is called before the first frame update
    void Start()
    {
        LeftHP.value = 50;
        LeftHpText.text = LeftHP.value.ToString();
        RightHpText.text = (100 - LeftHP.value).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LeftHP.value = LeftHP.value - 5;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            LeftHP.value = LeftHP.value + 5;
        }

        LeftHpText.text = LeftHP.value.ToString();
        RightHpText.text = (100 - LeftHP.value).ToString();

        if (LeftHP.value >= 100)
        {
            // left wins
            print("left wins");

        }
        else if (LeftHP.value == 0)
        {
            // right wins
            print("right wins");
        }
    }
}
