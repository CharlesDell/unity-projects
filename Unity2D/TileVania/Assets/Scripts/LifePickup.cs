using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickup : MonoBehaviour
{
    [SerializeField] AudioClip lifeSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(lifeSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddLife(1);
        Destroy(gameObject);
    }
}
