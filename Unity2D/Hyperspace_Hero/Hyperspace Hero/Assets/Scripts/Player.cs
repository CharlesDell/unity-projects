using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration parameters
    [Header("Player")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] float maxRot = 0.45f;
    [SerializeField] int health = 500;
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [Header("VFX")]
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float vfxDestroyDelay = 1f;
    [Header("SFX")]
    [SerializeField] AudioClip laserClip;
    [SerializeField] AudioClip deathClip;
    [Range(0, 10)] [SerializeField] float laserVol = 1;
    [Range(0, 10)] [SerializeField] float deathVol = 1;

    // Cached references
    Coroutine firingCoroutine;

    // State variables
    float xMin, xMax, yMin, yMax;
    float yRot = 0;



    // Start is called before the first frame update
    void Start()
    {
        SetMovementBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void SetMovementBounds()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        var dx = playerSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        var dy = playerSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

        var xPos = Mathf.Clamp(transform.position.x + dx, xMin, xMax);
        var yPos = Mathf.Clamp(transform.position.y + dy, yMin, yMax);

        transform.position = new Vector2(xPos, yPos);
        
        TurningEffect();
    }

    private void TurningEffect()
    {
        yRot += Input.GetAxis("Horizontal") * 0.04f - (yRot * 0.1f);
        yRot = Mathf.Clamp(yRot, -maxRot, maxRot);
        transform.rotation = new Quaternion(0, yRot, 0, 1);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            var laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            TriggerLaserSFX();

            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

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
            Die();
        }
    }

    private void Die()
    {
        TriggerDeathVFX();
        TriggerDeathSFX();
        Destroy(gameObject);
        FindObjectOfType<Level>().LoadGameOver();
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

    private void TriggerLaserSFX()
    {
        AudioSource.PlayClipAtPoint(laserClip, Camera.main.transform.position, laserVol);
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
