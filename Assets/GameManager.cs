using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int strikes = 0;
    public int maxStrikes = 3;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI strikesText;
    public GameObject gameOverScreen;

    private bool gameStarted = false;
    private bool isGameOver = false;
    private bool canTakeStrike = true;
    private Coroutine scoreCoroutine;

    // Color array for both paddle and ball
    public Color[] colorArray = { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, Color.cyan };

    public Color GetColorAt(int index) => colorArray[index % colorArray.Length];
    public int GetColorCount() => colorArray.Length;

    // New method to compare colors
    public bool AreColorsMatching(Color ballColor, Color paddleColor)
    {
        return Mathf.Approximately(ballColor.r, paddleColor.r) &&
               Mathf.Approximately(ballColor.g, paddleColor.g) &&
               Mathf.Approximately(ballColor.b, paddleColor.b);
    }

    private void Start()
    {
        score = 0;
        strikes = 0;
        isGameOver = false;
        gameStarted = false;

        UpdateScoreUI();
        UpdateStrikesUI();

        if (gameOverScreen != null)
            gameOverScreen.SetActive(false);

        Invoke("EnableStrikeChecking", 2f);
    }

    private void EnableStrikeChecking()
    {
        gameStarted = true;
        canTakeStrike = true;
        scoreCoroutine = StartCoroutine(IncrementScoreOverTime());
    }

    private IEnumerator IncrementScoreOverTime()
    {
        while (!isGameOver)
        {
            score++;
            UpdateScoreUI();
            yield return new WaitForSeconds(1f);
        }
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;

        score += amount;
        UpdateScoreUI();
    }

    public void AddStrike()
    {
        if (isGameOver || !gameStarted || !canTakeStrike) return;

        canTakeStrike = false;
        strikes++;
        Debug.Log("Strike Counted: " + strikes);
        UpdateStrikesUI();

        if (strikes >= maxStrikes)
        {
            GameOver();
        }
        else
        {
            Invoke("ResetStrikeCooldown", 1f);
        }
    }

    private void ResetStrikeCooldown()
    {
        canTakeStrike = true;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateStrikesUI()
    {
        if (strikesText != null)
            strikesText.text = "Strikes: " + strikes + "/" + maxStrikes;
    }

    private void GameOver()
    {
        isGameOver = true;
        if (scoreCoroutine != null)
            StopCoroutine(scoreCoroutine);

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);

        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
