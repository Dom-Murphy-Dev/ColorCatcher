using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Color[] colorArray; // Shared color array
    private int currentColorIndex = 0; // Current index in the color array

    private Vector2 initialPosition = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Store the ball's starting position
        initialPosition = transform.position;

        LaunchBall();
        ChangeColor();
    }

    public void SetColorArray(Color[] colors)
    {
        colorArray = colors;
    }

    private void LaunchBall()
    {
        Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
        rb.linearVelocity = randomDirection * speed; // Updated to use velocity
    }

    private void ChangeColor()
    {
        if (colorArray.Length > 0)
        {
            sr.color = colorArray[currentColorIndex];
            currentColorIndex = (currentColorIndex + 1) % colorArray.Length; // Cycle to the next color
        }
        else
        {
            Debug.LogWarning("No colors set in the BallController!");
        }
    }

    public void StopBall()
    {
        rb.linearVelocity = Vector2.zero; // Updated to use velocity
    }

    public void ResetBall()
    {
        transform.position = initialPosition;
        StopBall();
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
                Object.FindFirstObjectByType<GameManager>().AddScore(1);
            }
            else
            {
                Object.FindFirstObjectByType<GameManager>().AddStrike();
            }
        }
    }

    private void Update()
    {
        if (transform.position.y < 99f)
        {
            Object.FindFirstObjectByType<GameManager>().AddStrike();
            ResetBall();
        }

        // Ensure the ball remains visible by resetting its Z-position
        if (transform.position.z != 0)
        {
            Vector3 pos = transform.position;
            pos.z = 0;
            transform.position = pos;
        }
    }
}
