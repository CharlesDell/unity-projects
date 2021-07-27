using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    // Cached references
    GameSession session;

    // State variables
    private static bool playingClassic = true;

    private void Start()
    {
        session = FindObjectOfType<GameSession>();
        if (scoreText != null)
        {
            scoreText.text = session.GetScore().ToString();
            if (highScoreText != null)
            {
                highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
            }
        }
        
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        session.ResetGame();
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(2);
        playingClassic = true;
    }

    public void LoadInfiniteLevel()
    {
        SceneManager.LoadScene(1);
        playingClassic = false;
    }

    public void PlayAgain()
    {
        if (playingClassic)
        {
            LoadFirstLevel();
            session.ResetScore();
        }
        else
        {
            LoadInfiniteLevel();
            session.ResetScore();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // For debug purposes
    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }
}
