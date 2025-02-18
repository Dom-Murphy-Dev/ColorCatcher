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

    private bool isGameOver = false;

    private void Start()
    {
        score = 0;
        strikes = 0;
        isGameOver = false;
        UpdateUI();
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        UpdateUI();
    }

    public void AddStrike()
    {
        if (isGameOver) return;
        strikes++;
        UpdateUI();
        if (strikes >= maxStrikes) GameOver();
    }

    private void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (strikesText != null) strikesText.text = "Strikes: " + strikes + "/" + maxStrikes;
    }

    private void GameOver()
    {
        isGameOver = true;
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}