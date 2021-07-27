using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] Paddle paddle0;
    [SerializeField] float xVol;
    [SerializeField] float yVol = 15f;
    [SerializeField] AudioClip[] sounds;
    [SerializeField] float randomFactor = 0.2f;

    // State
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component references
    AudioSource audioSource;
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle0.transform.position;
        audioSource = GetComponent<AudioSource>();
        myRigidBody = GetComponent<Rigidbody2D>();
        xVol = Mathf.Ceil(UnityEngine.Random.Range(-3f, 3f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody.velocity = new Vector2(xVol, yVol);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle0.transform.position.x, paddle0.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 dv = new Vector2
            (UnityEngine.Random.Range(-randomFactor, randomFactor), 
            UnityEngine.Random.Range(-randomFactor, randomFactor));
        if (hasStarted)
        {
            TriggerSFX();
            myRigidBody.velocity += dv;
        }
    }

    private void TriggerSFX()
    {
        AudioClip clip = sounds[UnityEngine.Random.Range(0, sounds.Length)];
        audioSource.PlayOneShot(clip);
    }
}
