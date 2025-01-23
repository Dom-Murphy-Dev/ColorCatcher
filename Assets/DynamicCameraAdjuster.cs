using UnityEngine;

public class DynamicCameraAdjuster : MonoBehaviour
{
    public float defaultWidth = 9f; // Default width of the game world
    public float defaultHeight = 16f; // Default height of the game world (aspect ratio)

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        // Calculate the desired orthographic size based on screen aspect ratio
        float targetAspect = defaultWidth / defaultHeight;
        float screenAspect = (float)Screen.width / (float)Screen.height;

        if (screenAspect >= targetAspect)
        {
            cam.orthographicSize = defaultHeight / 2;
        }
        else
        {
            float differenceInSize = targetAspect / screenAspect;
            cam.orthographicSize = (defaultHeight / 2) * differenceInSize;
        }
    }
}
