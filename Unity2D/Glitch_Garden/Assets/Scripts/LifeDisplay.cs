using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    [SerializeField] float baseLives = 3;
    [SerializeField] int damage = 1;

    float lives;
    Text lifeText;
    LevelController levelController;

    void Start()
    {
        lifeText = GetComponent<Text>();
        levelController = FindObjectOfType<LevelController>();

        lives = baseLives - PlayerPrefsController.GetDifficulty();

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        lifeText.text = lives.ToString();
    }

    public void LoseLife()
    {
        lives -= damage;
        UpdateDisplay();
        if (lives <= 0)
        {
            levelController.HandleLoseCondition();
        }
    }
}
