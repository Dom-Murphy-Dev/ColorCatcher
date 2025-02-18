using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float score = 0f;
    public int strikes = 0;
    public int maxStrikes = 3;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI strikesText;
    public GameObject gameOverScreen;

    private bool isGameOver = false;

    private void Start()
    {
        score = 0f;
        strikes = 0;
        isGameOver = false;
        UpdateUI();
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    public void UpdateContinuousScore(float deltaTime)
    {
        if (isGameOver) return;
        score += deltaTime;
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
        if (scoreText != null) scoreText.text = "Score: " + Mathf.FloorToInt(score);
        if (strikesText != null) strikesText.text = "Strikes: " + strikes + "/" + maxStrikes;
    }

    private void GameOver()
    {
        isGameOver = true;
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }
}