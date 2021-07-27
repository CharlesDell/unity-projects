using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 100;
    [SerializeField]AudioClip coinSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(value);
        Destroy(gameObject);
    }
}
