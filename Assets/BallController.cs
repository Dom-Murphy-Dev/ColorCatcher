using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 200f;
    private Rigidbody2D rb;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    private void ResetBall()
    {
        transform.position = new Vector2(400, 185);
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * speed;
        ChangeColor();
    }

    private void ChangeColor()
    {
        int randomIndex = Random.Range(0, gameManager.GetColorCount());
        GetComponent<SpriteRenderer>().color = gameManager.GetColorAt(randomIndex);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            PaddleController paddle = collision.gameObject.GetComponent<PaddleController>();
            if (paddle != null)
            {
                Color ballColor = GetComponent<SpriteRenderer>().color;
                Color paddleColor = paddle.GetCurrentColor();

                if (gameManager.AreColorsMatching(ballColor, paddleColor))
                {
                    gameManager.AddScore(1);
                    speed *= 1.05f;
                    rb.linearVelocity = rb.linearVelocity.normalized * speed;
                }
                else
                {
                    gameManager.AddStrike();
                }
            }
        }
        else if (collision.gameObject.CompareTag("OutOfBounds"))
        {
            gameManager.AddStrike();
            ResetBall();
        }
    }
}
