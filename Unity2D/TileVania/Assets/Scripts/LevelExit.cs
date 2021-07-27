using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadDelay = 3f;
    [SerializeField] float slowFactor = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = slowFactor;
        yield return new WaitForSeconds(loadDelay);
        Time.timeScale = 1f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Destroy(FindObjectOfType<ScenePersist>());
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
