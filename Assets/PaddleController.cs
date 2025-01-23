using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public RectTransform paddleRect; // Paddle RectTransform
    public float moveSpeed = 1000f; // Speed of paddle movement
    private int currentColorIndex = 0; // Current color index
    private GameManager gameManager;

    private void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();

        // Initialize paddle color
        if (gameManager != null)
        {
            GetComponent<UnityEngine.UI.Image>().color = gameManager.GetColorAt(currentColorIndex);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleColorCycle();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        paddleRect.anchoredPosition += new Vector2(horizontalInput * moveSpeed * Time.deltaTime, 0);

        // Clamp within screen bounds
        float clampedX = Mathf.Clamp(paddleRect.anchoredPosition.x, -284f, 276f); // Adjust based on canvas size
        paddleRect.anchoredPosition = new Vector2(clampedX, paddleRect.anchoredPosition.y);
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
        GetComponent<UnityEngine.UI.Image>().color = gameManager.GetColorAt(currentColorIndex);
    }

    public Color GetCurrentColor()
    {
        return GetComponent<UnityEngine.UI.Image>().color;
    }
}
