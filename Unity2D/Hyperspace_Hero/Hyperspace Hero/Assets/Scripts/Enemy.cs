using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Configuration parameters
    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] int pointValue = 100;
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 0.4f;
    [SerializeField] float projectileSpeed = 5f;
    [Header("VFX")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float vfxDestroyDelay = 1f;
    [Header("SFX")]
    [SerializeField] AudioClip laserClip;
    [SerializeField] AudioClip deathClip;
    [SerializeField] float laserVol = 1;
    [SerializeField] float deathVol = 1;

    // State variables
    float shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    // Header

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            FindObjectOfType<GameSession>().AddToScore(pointValue);
            Die();
        }
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        var laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        TriggerLaserSFX();
    }

    private void Die()
    {
        Destroy(gameObject);
        TriggerDeathSFX();
        TriggerVFX();
    }

    private void TriggerVFX()
    {
        GameObject vfx = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(vfx, vfxDestroyDelay);
    }

    private void TriggerDeathSFX()
    {
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, deathVol);
    }

    private void TriggerLaserSFX()
    {
        AudioSource.PlayClipAtPoint(laserClip, Camera.main.transform.position, laserVol);
    }
}
