using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText == null) { scoreText = FindObjectOfType<TextMeshProUGUI>(); }
        if (gameSession == null) { gameSession = FindObjectOfType<GameSession>(); }

        scoreText.text = gameSession.GetScore().ToString();
    }
}
