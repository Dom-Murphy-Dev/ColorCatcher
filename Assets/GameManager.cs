using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int strikes = 0;
    public int maxStrikes = 3;
    public TMP_Text scoreText;
    public TMP_Text strikesText;
    public GameObject gameOverScreen;
    public BallController ball;
    public PaddleController paddle;

    public Color[] colorArray; // Shared array of colors for the ball and paddle

    private bool isGameOver = false;

    void Start()
    {
        score = 0;
        strikes = 0;
        UpdateUI();
        gameOverScreen.SetActive(false);

        // Assign the shared color array to the ball and paddle
        ball.SetColorArray(colorArray);
        paddle.SetColorArray(colorArray);
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
        else
        {
            ball.ResetBall();
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
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
