using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShowHideGrid : MonoBehaviour
{
    public GameObject targetObject; // Assign the object you want to show/hide in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        // Optionally, you can initialize the target object's active state here
        // targetObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the spacebar was pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle the active state of the targetObject
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}
