using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Configuration parameters
    [Header("Shield")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 300;
    [SerializeField] Sprite[] shieldSprites;
    [Header("VFX")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float vfxDestroyDelay = 1f;
    [Header("SFX")]
    [SerializeField] AudioClip deathClip;
    [Range(0, 10)] [SerializeField] float deathVol = 1;

    // Cached references
    Player player;

    // State variables
    int timesHit;



    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = player.GetTransform().position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        if (damageDealer.name == "Player Laser(Clone)") { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        
        if (health <= 0)
        {
            Explode();
            return;
        }

        ShowNextDamageSprite();
    }

    private void ShowNextDamageSprite()
    {
        int spriteIndex = timesHit + 1;
        if (shieldSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = shieldSprites[spriteIndex];
            timesHit++;
        }
        else
        {
            Debug.LogError("Block sprite is missing from array of " + gameObject.name);
        }
    }

    private void Explode()
    {
        TriggerDeathVFX();
        TriggerDeathSFX();
        Destroy(gameObject);
    }

    private void TriggerDeathVFX()
    {
        GameObject vfx = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(vfx, vfxDestroyDelay);
    }

    private void TriggerDeathSFX()
    {
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, deathVol);
    }
}
