using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 200f;
    private Rigidbody2D rb;
    private GameManager gameManager;

    // Flag to ensure we process a paddle collision only once per contact.
    private bool paddleCollisionProcessed = false;

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
        if (collision.gameObject.CompareTag("TopWall"))
        {
            ChangeColor();
            Vector2 newVelocity = rb.linearVelocity;
            newVelocity.y = -Mathf.Abs(newVelocity.y);
            rb.linearVelocity = newVelocity.normalized * speed;
            return;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            ChangeColor();
            ContactPoint2D contact = collision.contacts[0];
            Vector2 reflectedVelocity = Vector2.Reflect(rb.linearVelocity, contact.normal).normalized * speed;
            if (Mathf.Abs(reflectedVelocity.y) < 0.2f)
            {
                reflectedVelocity.y = (reflectedVelocity.y >= 0 ? 1 : -1) * 0.2f;
                reflectedVelocity = reflectedVelocity.normalized * speed;
            }
            rb.linearVelocity = reflectedVelocity;
        }
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            if (paddleCollisionProcessed) return;
            paddleCollisionProcessed = true;

            PaddleController paddle = collision.gameObject.GetComponent<PaddleController>();
            if (paddle != null)
            {
                Color ballColor = GetComponent<SpriteRenderer>().color;
                Color paddleColor = paddle.GetCurrentColor();

                // Convert colors to full RGBA strings for exact comparison
                string ballColorHex = ColorUtility.ToHtmlStringRGBA(ballColor);
                string paddleColorHex = ColorUtility.ToHtmlStringRGBA(paddleColor);

                Debug.Log($"Ball Color: {ballColorHex} | Paddle Color: {paddleColorHex}");

                bool isMatch = ballColorHex == paddleColorHex;

                if (isMatch)
                {
                    Debug.Log("MATCH: Scoring 1 point.");
                    gameManager.AddScore(1);
                    ChangeColor();
                    speed *= 1.05f;

                    Vector2 newVelocity = rb.linearVelocity.normalized * speed;
                    if (newVelocity.y < 0)
                    {
                        newVelocity.y = Mathf.Abs(newVelocity.y);
                        newVelocity = newVelocity.normalized * speed;
                    }
                    rb.linearVelocity = newVelocity;
                }
                else
                {
                    Debug.LogWarning("MISMATCH: Adding strike and resetting ball.");
                    gameManager.AddStrike();
                    ResetBall();
                }
            }
        }

        else if (collision.gameObject.CompareTag("OutOfBounds"))
        {
            gameManager.AddStrike();
            ResetBall();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            paddleCollisionProcessed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("OutOfBounds"))
        {
            gameManager.AddStrike();
            ResetBall();
        }
    }

    private void ChangeColor()
    {
        if (gameManager != null && gameManager.colorArray.Length > 0)
        {
            int randomIndex = Random.Range(0, gameManager.colorArray.Length);
            GetComponent<SpriteRenderer>().color = gameManager.colorArray[randomIndex];
        }
    }
}
