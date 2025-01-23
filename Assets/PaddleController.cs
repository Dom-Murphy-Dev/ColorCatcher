using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float moveSpeed = 10f;

    private SpriteRenderer sr;
    private Color[] colorArray; // Shared color array
    private int currentColorIndex = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (colorArray.Length > 0)
        {
            sr.color = colorArray[currentColorIndex];
        }
    }

    public void SetColorArray(Color[] colors)
    {
        colorArray = colors;
    }

    void Update()
    {
        HandleMovement();
        HandleColorCycle();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Clamp the paddle within screen bounds
        float clampedX = Mathf.Clamp(transform.position.x, -8f, 8f);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
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
        if (colorArray.Length > 0)
        {
            currentColorIndex = (currentColorIndex + 1) % colorArray.Length;
            sr.color = colorArray[currentColorIndex];
        }
    }

    public Color GetCurrentColor()
    {
        return sr.color;
    }
}
