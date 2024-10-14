using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextOnPin : MonoBehaviour
{
    public Camera mainCamera;              // Reference to the main camera
    public GameObject textPrefab;          // Prefab with TextMeshPro component
    public GameObject popupPanel;          // The panel that contains the input field and button
    public TMP_InputField inputField;      // The input field for user text
    public Button submitButton;            // Button to submit the text
    private Vector3 clickPosition;         // Store the click position in world space

    private void Start()
    {
        // Hide the popup panel on start
        popupPanel.SetActive(false);

        // Add listener for the submit button
        submitButton.onClick.AddListener(OnSubmit);
    }

    void Update()
    {
        // Ignore clicks if over UI elements
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))  // Left mouse click
        {
            // Perform a 2D raycast from the mouse position in world space
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseWorldPos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos2D, Vector2.zero);

            if (hit.collider != null)
            {
                // Get the layer of the hit 2D object
                int hitLayer = hit.collider.gameObject.layer;

                // Check if the object is on the "Maps" layer
                if (hitLayer == LayerMask.NameToLayer("Maps"))
                {
                // Store the world point from the hit
                clickPosition = hit.point;

                // Move the popup panel to the mouse position
                popupPanel.GetComponent<RectTransform>().position = Input.mousePosition;

                // Show the popup
                popupPanel.SetActive(true);
                }
            }
        }
    }

    private void OnSubmit()
    {
        // Instantiate text object at the click position
        GameObject textObj = Instantiate(textPrefab, clickPosition, Quaternion.identity);

        // Get the TextMeshPro component and set the text
        TextMeshPro textMesh = textObj.GetComponentInChildren<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.text = inputField.text;  // Set the text from the input field

            if (textMesh.text.Length == 0)
            {
                textMesh.text = "Null";
            }
        }

        // Reset the input field and hide the popup
        inputField.text = "";
        popupPanel.SetActive(false);
    }
}
