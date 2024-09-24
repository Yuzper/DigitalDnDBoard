using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float minZoom = 1f;
    public float maxZoom = 30f;

    private Camera cam;
    public float dragSpeed = 0.5f;  // Adjust this to control sensitivity; higher values make dragging faster
    private Vector3 dragOrigin;     // Point where mouse is first clicked

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        HandleZoom();
        HandleCameraDrag();
    }

    // Handles zooming
    void HandleZoom()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        if (cam.orthographic)
        {
            cam.orthographicSize -= scrollData * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
        else
        {
            cam.fieldOfView -= scrollData * zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minZoom, maxZoom);
        }
    }

    // Handles dragging
    void HandleCameraDrag()
    {
        // Detect the start of the drag
        if (Input.GetMouseButtonDown(1))
        {
            // Store the initial point where the mouse is clicked, in world coordinates
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return;
        }

        // Only perform dragging if the right mouse button is held down
        if (!Input.GetMouseButton(1)) return;

        // Get the current mouse position in world coordinates
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the difference (delta) between the original mouse position and the current one
        Vector3 difference = dragOrigin - currentMousePosition;

        // Move the camera in the opposite direction to the mouse drag
        transform.position += difference * dragSpeed;

        // Update the drag origin to the new current position for smooth dragging
        dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
