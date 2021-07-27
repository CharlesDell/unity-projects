using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Range(0.1f, 1f)]
    [SerializeField] float scrollRate = 0.1f;

    private void Update()
    {
        Vector3 translation = new Vector3
            (Mathf.Sin(transform.position.y),
            (transform.position.y + (scrollRate * Time.deltaTime)));
        transform.position = translation;
    }
}
