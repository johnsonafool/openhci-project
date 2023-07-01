using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    Transform star1;
    [SerializeField]
    Transform star2;
    [SerializeField]
    Transform star3;

    void Start()
    {
        if(star1 == null)
        {
            star1 = GetComponent<Transform>();
        }
    }

    void Update()
    {
        
    }
}
