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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            speed *= 1.05f;
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
        else if (collision.gameObject.CompareTag("OutOfBounds"))
        {
            gameManager.AddStrike();
            ResetBall();
        }
    }
}