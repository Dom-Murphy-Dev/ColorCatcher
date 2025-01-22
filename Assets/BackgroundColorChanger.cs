using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class BackgroundColorChanger : MonoBehaviour
{
    public Image panelImage; // Reference to the Panel's Image component
    private float colorSpeed = 0.5f; // Speed of the color transition

    void Start()
    {
        // Ensure the panelImage is assigned
        if (panelImage == null)
        {
            Debug.LogError("Panel Image is not assigned! Please assign it in the Inspector.");
        }
    }

    void Update()
    {
        if (panelImage == null) return; // Avoid null errors

        // Calculate a value that oscillates between 0 and 1 over time
        float t = Mathf.PingPong(Time.time * colorSpeed, 1f);

        // Lerp between two colors (e.g., Blue and Green)
        panelImage.color = Color.Lerp(Color.blue, Color.green, t);
    }
}
