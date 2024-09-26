using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float minZoom = 1f;
    public float maxZoom = 30f;

    private Camera cam;
    public float dragSpeed = 0.5f;  // Adjust this to control sensitivity; higher values make dragging faster
    [SerializeField]
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
        if (Input.GetMouseButtonDown(2))
        {
            // Store the initial point where the mouse is clicked, in world coordinates
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // Change camera position based on mouse movementl
        if(Input.GetMouseButton(2))
        {
            // Get the current mouse position in world coordinates
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate the difference (delta) between the original mouse position and the current one
            Vector3 difference = dragOrigin - currentMousePosition;

            // Move the camera in the opposite direction to the mouse drag
            transform.position += difference * dragSpeed;
        }
    }
}
