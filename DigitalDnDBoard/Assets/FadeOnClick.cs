using UnityEngine;

public class FadeOnClick : MonoBehaviour
{
    // Variables to control fade speed
    public float fadeDuration = 2f;
    private SpriteRenderer spriteRenderer;
    private Color spriteColor;
    private bool isFading = false;
    private float fadeStartTime;

    void Start()
    {
        // Get the SpriteRenderer component of the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 position = transform.position;
        position.z = -0.15f;
        transform.position = position;

        if (spriteRenderer != null)
        {
            // Get the object's initial color
            spriteColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on this GameObject.");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("TSETSET");
        // Start the fading process on mouse click
        if (!isFading)
        {
            isFading = true;
            fadeStartTime = Time.time;
        }
    }

    void Update()
    {
        // If the fading process has started
        if (isFading)
        {
            // Calculate the fading progress based on time
            float elapsed = Time.time - fadeStartTime;
            float progress = elapsed / fadeDuration;

            // Gradually reduce the alpha value of the object's color
            spriteColor.a = Mathf.Lerp(1f, 0f, progress);
            spriteRenderer.color = spriteColor;

            // Stop fading when the object is fully transparent
            if (progress >= 1f)
            {
                isFading = false;
                // Optionally disable or destroy the GameObject when it becomes invisible
                gameObject.SetActive(false);
                // Destroy(gameObject); // Uncomment if you want to completely destroy the object
            }
        }
    }
}
