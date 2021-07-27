using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float timeToWait = 2.0f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitToLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(2);
    }
}
