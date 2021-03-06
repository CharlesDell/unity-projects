using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // State
    [SerializeField] int breakableBlocks; // Serialized for debugging purposes

    // Cached references
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if ((breakableBlocks <= 0) && (tag == "Finite"))
        {
            sceneLoader.LoadNextScene();
        }
    }
}
