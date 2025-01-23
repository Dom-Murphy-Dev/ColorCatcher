using UnityEngine;

public class BallController : MonoBehaviour
{
    public RectTransform ballRect; // The ball's RectTransform
    public RectTransform leftWall, rightWall, topWall; // RectTransforms for walls
    public float speed = 500f; // Speed of ball movement
    private Vector2 direction = Vector2.up; // Initial direction

    private GameManager gameManager;

    private void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();

        // Initialize a random direction
        direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
    }

    private void Update()
    {
        // Move the ball
        ballRect.anchoredPosition += direction * speed * Time.deltaTime;

        // Check collisions
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        Vector2 ballPos = ballRect.anchoredPosition;

        // Get ball size (half width and height for bounds)
        float ballHalfWidth = ballRect.rect.width / 2;
        float ballHalfHeight = ballRect.rect.height / 2;

        // Collision with left wall
        if (ballPos.x - ballHalfWidth <= leftWall.anchoredPosition.x + (leftWall.rect.width / 2))
        {
            direction.x = Mathf.Abs(direction.x); // Reflect to the right
            ChangeColor();
        }

        // Collision with right wall
        if (ballPos.x + ballHalfWidth >= rightWall.anchoredPosition.x - (rightWall.rect.width / 2))
        {
            direction.x = -Mathf.Abs(direction.x); // Reflect to the left
            ChangeColor();
        }

        // Collision with top wall
        if (ballPos.y + ballHalfHeight >= topWall.anchoredPosition.y - (topWall.rect.height / 2))
        {
            direction.y = -Mathf.Abs(direction.y); // Reflect downward
            ChangeColor();
        }

        // Falling below the screen (falling out of play area)
        if (ballPos.y < -100f) // Adjust based on your layout
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
        // Reset position to the center
        ballRect.anchoredPosition = Vector2.zero;

        // Assign a new random direction
        direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
    }
}
