using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    void Start()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    
    void Update()
    {
        
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }

    public void PowerAttack()
    {
        animator.SetTrigger("power_attack");
    }

    public void Hurt()
    {
        animator.SetTrigger("hurt");
    }

    public void PowerHurt()
    {
        animator.SetTrigger("power_hurt");
    }
}
