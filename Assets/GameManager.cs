using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int strikes = 0; // Track player strikes
    public int maxStrikes = 3; // Max allowed strikes before game over
    public TMP_Text scoreText;
    public TMP_Text strikesText; // Display strikes
    public GameObject gameOverScreen;
    public BallController ball;

    private bool isGameOver = false;

    void Start()
    {
        score = 0;
        strikes = 0;
        UpdateUI();
        gameOverScreen.SetActive(false);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    public void AddStrike()
    {
        strikes++;
        UpdateUI();
        if (strikes >= maxStrikes)
        {
            GameOver();
        }
    }

    public void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        if (strikesText != null)
        {
            strikesText.text = "Strikes: " + strikes + "/" + maxStrikes;
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        ball.StopBall();
        gameOverScreen.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
