using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 200f;
    private Rigidbody2D rb;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    private void ResetBall()
    {
        transform.position = new Vector2(400, 185);
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * speed;
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
            if (paddle != null)
            {
                if (GetComponent<SpriteRenderer>().color == paddle.GetCurrentColor())
                {
                    gameManager.AddScore(1);
                    ChangeColor();
                    speed *= 1.05f;
                    rb.linearVelocity = rb.linearVelocity.normalized * speed;
                }
                else
                {
                    gameManager.AddStrike();
                    ResetBall();
                }
            }
        }

        if (collision.gameObject.CompareTag("OutOfBounds"))
        {
            gameManager.AddStrike();
            ResetBall();
        }
    }

    private void ChangeColor()
    {
        if (gameManager != null)
            GetComponent<SpriteRenderer>().color = gameManager.GetNextColor();
    }
}
