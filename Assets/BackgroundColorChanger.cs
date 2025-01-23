using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    public Camera mainCamera; // Reference to the Main Camera
    public float colorSpeed = 0.5f; // Speed of the color transition
    public Color startColor = Color.blue; // First color in the gradient
    public Color endColor = Color.green; // Second color in the gradient

    void Start()
    {
        // Ensure the Main Camera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically find the Main Camera
        }

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned or cannot be found!");
        }
    }

    void Update()
    {
        if (mainCamera == null) return; // Avoid null errors

        // Calculate a value that oscillates between 0 and 1 over time
        float t = Mathf.PingPong(Time.time * colorSpeed, 1f);

        // Interpolate the background color of the camera
        mainCamera.backgroundColor = Color.Lerp(startColor, endColor, t);
    }
}
