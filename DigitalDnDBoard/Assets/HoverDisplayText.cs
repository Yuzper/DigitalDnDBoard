using UnityEngine;
using TMPro;

public class HoverDisplayText : MonoBehaviour
{
    public TextMeshProUGUI hoverText;  // Reference to the attached TMP text field
    private TextMeshPro tmp; // Reference to the TextMeshPro component

    private void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        hoverText.gameObject.SetActive(false);
    }

    // This is called when the mouse enters the object's collider
    private void OnMouseEnter()
    {
        hoverText.gameObject.SetActive(true);
        hoverText.text = tmp.text;

    //    Vector3 mousePosition = Input.mousePosition;
    //    hoverText.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    // This is called when the mouse exits the object's collider
    private void OnMouseExit()
    {
        hoverText.gameObject.SetActive(false);

    }

    // Destroy pin point with right click
    private void OnMouseDown()
    {
        Debug.Log("MouseEvent");
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right click detected. Destroying object."); // Debug log
            Destroy(gameObject);
        }
    }

}
