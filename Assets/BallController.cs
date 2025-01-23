using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f; // Ball speed
    public Color[] colors; // Ball colors
    private Rigidbody2D rb; // Reference to Rigidbody2D
    private SpriteRenderer sr; // Reference to SpriteRenderer

    private Vector2 initialPosition = Vector2.zero; // Default spawn position

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Store the ball's starting position
        initialPosition = transform.position;

        LaunchBall();
        ChangeColor();
    }

    private void LaunchBall()
    {
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
        rb.linearVelocity = randomDirection * speed;
    }

    private void ChangeColor()
    {
        if (colors.Length > 0)
        {
            sr.color = colors[Random.Range(0, colors.Length)];
        }
        else
        {
            Debug.LogWarning("No colors set in the BallController!");
        }
    }

    public void StopBall()
    {
        // Stop the ball's movement
        rb.linearVelocity = Vector2.zero;
    }

    public void ResetBall()
    {
        // Reset the ball's position to the initial position
        transform.position = initialPosition;

        // Stop the ball's movement
        StopBall();

        // Relaunch the ball and assign a new color
        LaunchBall();
        ChangeColor();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            PaddleController paddle = collision.gameObject.GetComponent<PaddleController>();
            if (paddle != null && sr.color == paddle.GetCurrentColor())
            {
                // Matching color: Add score
                Object.FindFirstObjectByType<GameManager>().AddScore(1);
            }
            else
            {
                // Mismatched color: Add a strike
                Object.FindFirstObjectByType<GameManager>().AddStrike();
            }
        }
    }

    private void Update()
    {
        // Trigger strike if the ball goes below the Y-axis threshold
        if (transform.position.y < 99f) // Updated condition
        {
            Debug.Log("Ball went below Y = 99"); // Debugging message
            Object.FindFirstObjectByType<GameManager>().AddStrike();
            ResetBall(); // Reset the ball after it goes below the threshold
        }
    }
}
