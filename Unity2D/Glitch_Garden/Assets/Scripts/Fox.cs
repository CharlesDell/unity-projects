using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Attacker attacker;
    Animator animator;

    private void Start()
    {
        attacker = GetComponent<Attacker>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.GetComponent<Defender>())
        {
            if (otherObject.GetComponent<Gravestone>())
            {
                animator.SetTrigger("jumpTrigger");
            }
            else
            {
                attacker.Attack(otherObject);
            }
        }

        
    }
}
