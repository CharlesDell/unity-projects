using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    // Cached references
    GameSession session;

    private void Start()
    {
        session = FindObjectOfType<GameSession>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Game Over");
        if (PlayerPrefs.GetInt("HighScore") < session.GetScore())
        {
            PlayerPrefs.SetInt("HighScore", session.GetScore());
        }
    }
}
