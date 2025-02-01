using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 200f;
    private Rigidbody2D rb;
    private GameManager gameManager;

    private void Start()
    {
        // Find the GameManager and Rigidbody2D.
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    private void ResetBall()
    {
        // Reset the ball to its starting position and launch it upward.
        transform.position = new Vector2(400, 185);
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // *** Handle collisions with the TopWall separately ***
        if (collision.gameObject.CompareTag("TopWall"))
        {
            ChangeColor();
            // Force the ball's vertical velocity to be downward.
            Vector2 newVelocity = rb.linearVelocity;
            newVelocity.y = -Mathf.Abs(newVelocity.y);  // Ensures a negative (downward) value.
            newVelocity = newVelocity.normalized * speed;
            rb.linearVelocity = newVelocity;
            return;
        }
        // *** Handle generic wall collisions (sides, bottom, etc.) ***
        else if (collision.gameObject.CompareTag("Wall"))
        {
            ChangeColor();

            // Reflect the ball's velocity using the collision's contact normal.
            ContactPoint2D contact = collision.contacts[0];
            Vector2 reflectedVelocity = Vector2.Reflect(rb.linearVelocity, contact.normal);
            reflectedVelocity = reflectedVelocity.normalized * speed;

            // If the vertical component is too small, force a minimum vertical value.
            if (Mathf.Abs(reflectedVelocity.y) < 0.2f)
            {
                reflectedVelocity.y = (reflectedVelocity.y >= 0 ? 1 : -1) * 0.2f;
                reflectedVelocity = reflectedVelocity.normalized * speed;
            }

            rb.linearVelocity = reflectedVelocity;
        }
        // *** Handle paddle collisions ***
        else if (collision.gameObject.CompareTag("Paddle"))
        {
            PaddleController paddle = collision.gameObject.GetComponent<PaddleController>();
            if (paddle != null)
            {
                // Retrieve both colors and log them for debugging.
                Color ballColor = GetComponent<SpriteRenderer>().color;
                Color paddleColor = paddle.GetCurrentColor();
                Debug.Log("Ball color: " + ballColor + " | Paddle color: " + paddleColor);

                // Compare using our ColorsMatch helper.
                if (ColorsMatch(ballColor, paddleColor))
                {
                    // If colors match: score, change color, increase speed, and bounce upward.
                    gameManager.AddScore(1);
                    ChangeColor();
                    speed *= 1.05f;  // Increase speed by 5%

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
                    // If colors do not match: log, add a strike, and reset the ball.
                    Debug.Log("Color mismatch detected! Adding strike and resetting ball.");
                    gameManager.AddStrike();
                    ResetBall();
                }
            }
        }
        // *** Handle out-of-bounds collisions ***
        else if (collision.gameObject.CompareTag("OutOfBounds"))
        {
            gameManager.AddStrike();
            ResetBall();
        }
    }

    /// <summary>
    /// Changes the ball's color to a random color from the GameManager's color array.
    /// </summary>
    private void ChangeColor()
    {
        if (gameManager != null && gameManager.colorArray.Length > 0)
        {
            int randomIndex = Random.Range(0, gameManager.colorArray.Length);
            GetComponent<SpriteRenderer>().color = gameManager.colorArray[randomIndex];
        }
    }

    /// <summary>
    /// Compares two colors within a specified tolerance to account for floating point imprecision.
    /// </summary>
    /// <param name="a">First color.</param>
    /// <param name="b">Second color.</param>
    /// <param name="tolerance">Allowed difference per channel (default is 0.01).</param>
    /// <returns>True if colors are approximately equal; otherwise, false.</returns>
    private bool ColorsMatch(Color a, Color b, float tolerance = 0.01f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance &&
               Mathf.Abs(a.a - b.a) < tolerance;
    }
}
