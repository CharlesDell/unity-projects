using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    LifeDisplay lifeDisplay;

    private void Start()
    {
        lifeDisplay = FindObjectOfType<LifeDisplay>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lifeDisplay.LoseLife();
        Destroy(collision.gameObject);
    }
}
