using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private float horizontal;
    private float speed = 400f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform ballCheck;
    [SerializeField] private LayerMask foreground;

    // New fields for color logic:
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private int currentColorIndex = 0;

    private void Start()
    {
        // Get the SpriteRenderer on this GameObject.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Find the GameManager (make sure one exists in your scene).
        gameManager = FindFirstObjectByType<GameManager>();

        // Set the paddle�s starting color from the GameManager�s color array.
        if (gameManager != null)
        {
            spriteRenderer.color = gameManager.GetColorAt(currentColorIndex);
        }
    }

    private void Update()
    {
        // Handle horizontal movement input.
        horizontal = Input.GetAxisRaw("Horizontal");

        // --- Color Changing Logic ---
        // Press Z to cycle to the previous color.
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentColorIndex--;
            if (currentColorIndex < 0)
            {
                currentColorIndex = gameManager.GetColorCount() - 1;
            }
            spriteRenderer.color = gameManager.GetColorAt(currentColorIndex);
        }

        // Press X to cycle to the next color.
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentColorIndex = (currentColorIndex + 1) % gameManager.GetColorCount();
            spriteRenderer.color = gameManager.GetColorAt(currentColorIndex);
        }
    }

    private void FixedUpdate()
    {
        // Move the paddle. (Using rb.velocity instead of linearVelocity for Rigidbody2D.)
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    // This method is called by BallController to determine the paddle�s current color.
    public Color GetCurrentColor()
    {
        return spriteRenderer.color;
    }
}
