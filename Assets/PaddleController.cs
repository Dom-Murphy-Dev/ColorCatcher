using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float moveSpeed = 10f; // Speed of the paddle's movement
    public Color[] colors; // Array of paddle colors
    private int currentColorIndex = 0; // Current color index
    private SpriteRenderer sr; // Reference to the SpriteRenderer
    private float screenWidthInUnits = 16f; // Adjust based on the screen width in Unity units

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Set the initial paddle color
        if (colors.Length > 0)
        {
            sr.color = colors[currentColorIndex];
        }
        else
        {
            Debug.LogError("No colors assigned to PaddleController!");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleColorSwitch();
    }

    // Handles left-right paddle movement
    void HandleMovement()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // Use keyboard controls for testing in the editor
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Clamp the paddle position within screen bounds
        float clampedX = Mathf.Clamp(transform.position.x, -screenWidthInUnits / 2 + 1, screenWidthInUnits / 2 - 1);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
#endif

#if UNITY_IOS || UNITY_ANDROID
        // Touch-based movement for mobile devices
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
            transform.position = new Vector3(touchPosition.x, transform.position.y, transform.position.z);

            // Clamp the paddle position within screen bounds
            float clampedX = Mathf.Clamp(transform.position.x, -screenWidthInUnits / 2 + 1, screenWidthInUnits / 2 - 1);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
#endif
    }

    // Handles color switching (keyboard or screen tap)
    void HandleColorSwitch()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Space)) // Keyboard input for testing
        {
            CycleColor();
        }
#endif

#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // Tap for mobile
        {
            CycleColor();
        }
#endif
    }

    // Cycles through the available colors
    void CycleColor()
    {
        if (colors.Length > 0)
        {
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            sr.color = colors[currentColorIndex];
        }
    }

    // Returns the current paddle color (for collision checks)
    public Color GetCurrentColor()
    {
        return sr.color;
    }
}