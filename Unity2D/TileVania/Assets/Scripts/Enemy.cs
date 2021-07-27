using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Serialized parameters
    [Range(-1, -10)][Tooltip("Movement speed")]
    [SerializeField] float movementSpeed = -1f;

    // Cached references
    Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        myRigidbody.velocity = new Vector2(movementSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(-myRigidbody.velocity.x)), 1f);
        movementSpeed = -1 * movementSpeed;
    }
}