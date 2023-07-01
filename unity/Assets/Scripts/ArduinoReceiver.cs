using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
public class ArduinoReceiver : MonoBehaviour
{
    [SerializeField]
    string ArduinoCOM = "COM12";
    [SerializeField]
    int ArduinoBaud = 9600;

    SerialPort sp;
    [SerializeField]
    GameManager gameManager;

    void Start()
    {
        sp = new SerialPort(ArduinoCOM, ArduinoBaud);
        sp.Open();
        sp.ReadTimeout = 1;
        if(gameManager == null) {
            gameManager = GetComponent<GameManager>();
        }
    }

    void FixedUpdate()
    {
        if (sp.IsOpen)
        {
            try
            {
                string value = sp.ReadLine();
                if (value != "")
                {
                    switch (value)
                    {
                        case "far1":
                            gameManager.ClearRecycle();
                            break;
                        case "far2":
                            gameManager.ClearNormal();
                            break;
                        case "close1":
                            gameManager.ThrowRecycle();
                            break;
                        case "close2":
                            gameManager.ThrowNormal();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (TimeoutException)
            {
                //Debug.Log("error="+ errorpiece);
            }
        }
    }
}