using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float rotationIncrement = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationIncrement * Time.deltaTime);
    }
}
