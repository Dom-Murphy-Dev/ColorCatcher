using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private float horizontal;
    private float speed = 400f;
    private int currentColorIndex = 0;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = gameManager.GetColorAt(currentColorIndex);
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Z)) ChangeColor(-1);
        if (Input.GetKeyDown(KeyCode.X)) ChangeColor(1);
    }

    private void ChangeColor(int direction)
    {
        currentColorIndex = (currentColorIndex + direction + gameManager.GetColorCount()) % gameManager.GetColorCount();
        spriteRenderer.color = gameManager.GetColorAt(currentColorIndex);
    }

    public Color GetCurrentColor() => spriteRenderer.color;

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }
}
