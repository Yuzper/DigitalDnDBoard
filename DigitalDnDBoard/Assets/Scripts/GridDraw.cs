using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDraw : MonoBehaviour
{
    public Grid grid;                 // Reference to the Grid component
    public LayerMask layerMask;       // Layer mask for raycasting
    public GameObject gridIndicator;   // The object to move to the intersection point
    public List<Vector3> polygon = new List<Vector3>();
    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits an object in the specified layer mask
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 hitPoint = hit.point;                            // The point where the ray hits
            Vector3Int cellPos = grid.WorldToCell(hitPoint);        // Get the grid cell position
            Vector3 worldPos = grid.CellToWorld(cellPos);            // Get the world position of the cell's lower-left corner

            // Calculate the middle of the cell
            Vector3 cellSize = grid.cellSize; // Get the size of the grid cell
            Vector3 middlePos = new Vector3(
                worldPos.x + cellSize.x / 2,  // Middle X
                worldPos.y + cellSize.y / 2,                    // Y remains the same
                worldPos.z// + cellSize.z / 2   // Middle Z
            );

            // Move the grid indicator to the middle position of the cell
            gridIndicator.transform.position = middlePos;
        }
    }
}
