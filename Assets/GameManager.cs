using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshPro support

public class GameManager : MonoBehaviour
{
    public int score = 0; // Player's current score
    public TMP_Text scoreText; // Reference to the score UI TextMeshPro
    public GameObject gameOverScreen; // Game Over UI screen
    public BallController ball; // Reference to the BallController script
    public PaddleController paddle; // Reference to the PaddleController script

    private bool isGameOver = false; // Tracks if the game is over

    void Start()
    {
        // Initialize the game state
        score = 0;
        UpdateScoreUI();
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Game Over screen is not assigned in the Inspector!");
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("ScoreText is not assigned in the Inspector!");
        }
    }

    public void GameOver()
    {
        if (isGameOver) return; // Prevent multiple game-over calls

        isGameOver = true;

        // Stop the ball and show the game over screen
        if (ball != null)
        {
            ball.StopBall();
        }
        else
        {
            Debug.LogWarning("Ball is not assigned in the Inspector!");
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        Time.timeScale = 0; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void ResetBall()
    {
        if (ball != null)
        {
            ball.ResetBall();
        }
        else
        {
            Debug.LogWarning("Ball is not assigned in the Inspector!");
        }
    }
}
