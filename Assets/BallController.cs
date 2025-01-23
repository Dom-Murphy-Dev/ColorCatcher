using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f; // Speed of the ball
    public Color[] colors; // Array of possible colors
    private Rigidbody2D rb; // Rigidbody2D for physics
    private SpriteRenderer sr; // SpriteRenderer to change color

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Launch the ball in a random direction
        LaunchBall();

        // Set the initial color of the ball
        ChangeColor();
    }

    private void LaunchBall()
    {
        // Choose a random direction and normalize the vector
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
        rb.linearVelocity = randomDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Change the ball's color when it hits a wall
            ChangeColor();
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Check if the paddle color matches the ball's color
            PaddleController paddle = collision.gameObject.GetComponent<PaddleController>();
            if (paddle != null && sr.color == paddle.GetCurrentColor())
            {
                // Matching color, continue gameplay
                Debug.Log("Match! Ball kept in play.");
            }
            else
            {
                // Non-matching color, end the game
                Debug.Log("Game Over! Ball passed through.");
                Object.FindFirstObjectByType<GameManager>().GameOver();
            }
        }
    }

    private void ChangeColor()
    {
        // Randomly select a new color from the color array
        if (colors.Length > 0)
        {
            sr.color = colors[Random.Range(0, colors.Length)];
        }
        else
        {
            Debug.LogWarning("No colors set in the BallController!");
        }
    }
}