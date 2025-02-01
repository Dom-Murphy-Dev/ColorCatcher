using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float moveSpeed = 400f;
    private Rigidbody2D rb;
    private int currentColorIndex = 0;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = gameManager.GetColorAt(currentColorIndex);
    }

    private void Update()
    {
        HandleMovement();
        HandleColorCycle();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, 0);
    }

    private void HandleColorCycle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CycleColor();
        }
    }

    private void CycleColor()
    {
        currentColorIndex = (currentColorIndex + 1) % gameManager.GetColorCount();
        GetComponent<SpriteRenderer>().color = gameManager.GetColorAt(currentColorIndex);
    }

    public Color GetCurrentColor()
    {
        return GetComponent<SpriteRenderer>().color;
    }
}
