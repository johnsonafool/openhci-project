using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    void Start()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void Show()
    {
        animator.SetTrigger("show");
        audioSource.Play();
    }
}
