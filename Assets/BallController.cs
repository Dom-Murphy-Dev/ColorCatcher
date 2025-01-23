using UnityEngine;

public class BallController : MonoBehaviour
{
    public RectTransform ballRect; // The RectTransform of the ball
    public RectTransform leftWall, rightWall, topWall; // References to wall RectTransforms
    public float speed = 500f; // Ball speed
    private Vector2 direction = Vector2.up; // Initial ball direction

    private GameManager gameManager;

    private void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();

        // Randomize initial direction
        direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
    }

    private void Update()
    {
        // Move the ball
        ballRect.anchoredPosition += direction * speed * Time.deltaTime;

        // Check collisions with the walls
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        Vector2 ballPos = ballRect.anchoredPosition;

        // Collision with left wall
        if (ballPos.x <= leftWall.anchoredPosition.x + (leftWall.rect.width / 2))
        {
            direction.x = Mathf.Abs(direction.x); // Bounce right
            ChangeColor();
        }

        // Collision with right wall
        if (ballPos.x >= rightWall.anchoredPosition.x - (rightWall.rect.width / 2))
        {
            direction.x = -Mathf.Abs(direction.x); // Bounce left
            ChangeColor();
        }

        // Collision with top wall
        if (ballPos.y >= topWall.anchoredPosition.y - (topWall.rect.height / 2))
        {
            direction.y = -Mathf.Abs(direction.y); // Bounce down
            ChangeColor();
        }

        // Falling below the screen (e.g., game over condition)
        if (ballPos.y < -100f) // Adjust threshold as needed
        {
            gameManager.AddStrike();
            ResetBall();
        }
    }

    private void ChangeColor()
    {
        if (gameManager != null)
        {
            GetComponent<UnityEngine.UI.Image>().color = gameManager.GetNextColor();
        }
    }

    public void ResetBall()
    {
        ballRect.anchoredPosition = Vector2.zero; // Reset to center
        direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized; // New random direction
    }
}
