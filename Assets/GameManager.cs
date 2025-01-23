using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using TMPro; // For TextMeshPro UI (optional)

public class GameManager : MonoBehaviour
{
    public Color[] colorArray; // Shared array of colors for the ball and paddle
    private int currentColorIndex = 0; // Tracks the current color index

    public int score = 0; // Player's current score
    public int strikes = 0; // Player's current strikes
    public int maxStrikes = 3; // Maximum allowed strikes before game over

    public TextMeshProUGUI scoreText; // Reference to the score UI element
    public TextMeshProUGUI strikesText; // Reference to the strikes UI element
    public GameObject gameOverScreen; // Reference to the Game Over UI panel

    private bool isGameOver = false;

    private void Start()
    {
        score = 0;
        strikes = 0;

        // Initialize UI
        UpdateScoreUI();
        UpdateStrikesUI();

        // Hide the Game Over screen at the start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    // Adds points to the player's score
    public void AddScore(int points)
    {
        if (isGameOver) return;

        score += points;
        UpdateScoreUI();
    }

    // Handles strikes and checks for Game Over condition
    public void AddStrike()
    {
        if (isGameOver) return;

        strikes++;
        UpdateStrikesUI();

        // Check if strikes have reached the maximum
        if (strikes >= maxStrikes)
        {
            GameOver();
        }
    }

    // Updates the score UI element
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Updates the strikes UI element
    private void UpdateStrikesUI()
    {
        if (strikesText != null)
        {
            strikesText.text = "Strikes: " + strikes + "/" + maxStrikes;
        }
    }

    // Triggers the Game Over state
    private void GameOver()
    {
        isGameOver = true;

        // Show the Game Over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Pause the game
        Time.timeScale = 0;
    }

    // Restarts the game (attach this to a button in the Game Over UI)
    public void RestartGame()
    {
        // Resume time and reload the current scene
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    // Retrieves the next color in the array and cycles back to the start if needed
    public Color GetNextColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colorArray.Length;
        return colorArray[currentColorIndex];
    }

    // Retrieves a color at a specific index in the array
    public Color GetColorAt(int index)
    {
        return colorArray[index];
    }

    // Returns the total number of colors in the array
    public int GetColorCount()
    {
        return colorArray.Length;
    }
}
