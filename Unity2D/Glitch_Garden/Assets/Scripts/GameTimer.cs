using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Time in seconds")]
    [SerializeField] float levelTime = 10;

    bool triggeredLevelFinished = false;

    Slider slider;
    LevelController levelController;

    private void Start()
    {
        slider = GetComponent<Slider>();
        levelController = FindObjectOfType<LevelController>();
    }

    private void Update()
    {
        if (triggeredLevelFinished) { return; }
        slider.value = Time.timeSinceLevelLoad / levelTime;

        bool timerFinished = (Time.timeSinceLevelLoad >= levelTime);
        if (timerFinished)
        {
            levelController.LevelTimerFinished();
            triggeredLevelFinished = true;
        }
    }
}
