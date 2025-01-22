using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Color[] colors;
    private SpriteRenderer spriteRenderer;
    private int currentColorIndex;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColorIndex = 0;
        spriteRenderer.color = colors[currentColorIndex];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Tap to change color
        {
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            spriteRenderer.color = colors[currentColorIndex];
        }
    }
}
