using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;
    [SerializeField] float waitTime = 3f;
    int numberOfAttackers = 0;
    bool levelTimerFinished = false;

    AttackerSpawner[] spawners;

    private void Start()
    {
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        spawners = FindObjectsOfType<AttackerSpawner>();
    }

    public void AttackerSpawned()
    {
        numberOfAttackers += 1;
    }

    public void AttackerDestroyed()
    {
        numberOfAttackers -= 1;
        if (numberOfAttackers <= 0 && levelTimerFinished)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    public void HandleLoseCondition()
    {
        loseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator HandleWinCondition()
    {
        winCanvas.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waitTime);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        foreach (var spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }
}
