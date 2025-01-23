using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    public Color[] colors;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        LaunchBall();
        ChangeColor();
    }

    private void LaunchBall()
    {
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-0.5f, 1f)).normalized;
        rb.linearVelocity = randomDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ChangeColor();
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            PaddleController paddle = collision.gameObject.GetComponent<PaddleController>();
        }

        if (paddle  != null && sr.color == paddle.GetCurrentColor ())
        {
            Debug.Log("Match! Ball kept in play.");
        } else
        {
            Debug.Log("Game Over!");
            Object.FindFirstObjectByType<GameManager>().GameOver();

        }

    }

    private void ChangeColor()
    {
        if (colors.Length > 0)
        {
            sr.color = colors[Random.Range(0, colors.Length)];
        } else
        {
            Debug.LogWarning("No colors set in the BallController!");
        }
    }
}
