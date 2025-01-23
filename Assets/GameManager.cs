using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0; // Player's current score
    public Text scoreText; // Reference to the score UI Text
    public GameObject gameOverScreen; // Game Over UI screen
    public BallController ball; // Reference to the BallController script
    public PaddleController paddle; // Reference to the PaddleController script

    private bool isGameOver = false; // Tracks if the game is over

    void Start()
    {
        // Initialize the game state
        score = 0;
        UpdateScoreUI();
        gameOverScreen.SetActive(false);
    }

    // Adds points to the player's score
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    // Updates the score UI
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Handles the Game Over state
    public void GameOver()
    {
        isGameOver = true;
        ball.StopBall(); // Stop the ball from moving
        gameOverScreen.SetActive(true); // Show Game Over screen
        Time.timeScale = 0; // Pause the game
    }

    // Restarts the game
    public void RestartGame()
    {
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // Resets the ball to its initial state
    public void ResetBall()
    {
        ball.ResetBall(); // Call the BallController reset logic
    }
}