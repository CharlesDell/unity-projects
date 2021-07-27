using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameSession : MonoBehaviour
{
    // Configuration parameters
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] TextMeshProUGUI scoreText;
    
    [SerializeField] float period = 6f;
    [SerializeField] Block blockType;
    [SerializeField] bool isAutoPlayEnabled;

    // State variables
    private int score = 0;
    private float time = 0.0f;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        if (FindObjectOfType<Level>().tag == "Infinite")
        {
            GenerateCloud();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        scoreText.text = score.ToString();
        time += Time.deltaTime;
        if (FindObjectOfType<Level>() != null)
        {
            if ((time >= period) && (FindObjectOfType<Level>().tag == "Infinite"))
            {
                time = 0.0f;
                GenerateCloud();
            }
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(Block block)
    {
        score += block.GetPoints();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void ResetScore()
    {
        score = 0;
    }

    private void GenerateCloud()
    {
        var y = UnityEngine.Random.Range(6f, 10.5f);
        var x = UnityEngine.Random.Range(17f, 19.1f);
        Instantiate(blockType, new Vector3(17f, y), Quaternion.identity);
        Instantiate(blockType, new Vector3(18f, y), Quaternion.identity);
        Instantiate(blockType, new Vector3(19f, y), Quaternion.identity);
        Instantiate(blockType, new Vector3(x, y + 1), Quaternion.identity);
        Instantiate(blockType, new Vector3(x + 1f, y + 1), Quaternion.identity);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
