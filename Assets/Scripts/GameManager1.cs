using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager1 : MonoBehaviour
{
    [SerializeField]
    GameObject[] ballPrefabs;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    Transform canvas;
    
    Ball ball;

    [SerializeField]
    Paddle paddle;
    //public Brick[] bricks { get; private set; }

    [SerializeField]
    TMP_Text Score;

    [SerializeField]
    TMP_Text AirTime;

    [SerializeField]
    TMP_Text Lives;    

    [SerializeField]
    Transform leftWall;

    [SerializeField]
    Transform rightWall;

    [SerializeField]
    Transform topWall;

    [SerializeField]
    Transform bottomWall;

    public int score = 0;
    public int lives = 3;
    float sessionTime = 0f;
    List<float> sessionTimes = new List<float>();
    bool recordTime = true;

    private void Awake()
    {
        int colorIndex = PlayerPrefs.GetInt("Ball");
        GameObject GO = Instantiate(ballPrefabs[colorIndex]);
        ball = GO.GetComponent<Ball>();    }

    private void Update()
    {
        if (!recordTime)
        {
            return;
        }

        sessionTime += Time.deltaTime;
        AirTime.text = "Time: " + sessionTime.ToString("0.0");
    }

    private void Start()
    {
        NewGame();        
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        Score.text = "Score: ";
        Lives.text = "Lives: " + lives.ToString();

    }

    private void SetText(GameObject GO, string myString)
    {
        TMP_Text text = GO.GetComponent<TMP_Text>();
        text.text = myString;
    }

    public void Miss()
    {
        this.lives--;
        recordTime = false;
        Lives.text = "Lives: " + lives;

        if (this.lives > 0) {
            ResetLevel();
        } else {
            GameOver();
        }
    }

    public void RestartGame()
    {
        NewGame();
        ResetLevel();
        gameOverPanel.SetActive(false);
    }

    private void ResetLevel()
    {
        this.paddle.ResetPaddle();
        this.ball.ResetBall();
        recordTime = true;
    }

    private void GameOver()
    {
        sessionTimes.Add(sessionTime);
        sessionTime = 0f;
        gameOverPanel.SetActive(true);
        paddle.FreezePaddle();
        //DisplayAalytics();
    }

    public string GetMaxTime()
    {
        sessionTimes.Sort();
        return sessionTimes[sessionTimes.Count - 1].ToString("0.0");
    }

    public string GetAvgTime()
    {
        float avgTime = sessionTimes.Aggregate((x, y) => x + y) / sessionTimes.Count;
        return avgTime.ToString("0.0");
    }

    public string GetScore()
    {
        return score.ToString();
    }

    public string GetLives()
    {
        return lives.ToString();
    }

    private void DisplayAalytics()
    {
        sessionTimes.Sort();

        GameObject MaxTime = gameOverPanel.transform.GetChild(0).gameObject;
        SetText(MaxTime, "Max Time In Air: " + sessionTimes[sessionTimes.Count - 1].ToString("0.0"));

        GameObject AvgTime = gameOverPanel.transform.GetChild(1).gameObject;
        float avgTime = sessionTimes.Aggregate((x, y) => x + y) / sessionTimes.Count;
        SetText(AvgTime, "Avg Time In Air: " + avgTime.ToString("0.0"));

        GameObject Score = gameOverPanel.transform.GetChild(2).gameObject;
        SetText(Score, "Score: " + score.ToString());

        GameObject Lives = gameOverPanel.transform.GetChild(3).gameObject;
        SetText(Lives, "Lives: " + lives.ToString());
    }

    public void Hit()
    {
        this.score += 100;
        Score.text = "Score: " + score.ToString();
    }

    public float GetRandomXPosition()
    {
        return UnityEngine.Random.Range(leftWall.position.x + 3, rightWall.position.x - 3);
    }

    public float GetRandomYPosition()
    {
        return UnityEngine.Random.Range(topWall.position.y - 3, bottomWall.position.x + 8);
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("Main");
    }
}
