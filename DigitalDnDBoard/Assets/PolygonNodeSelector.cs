using UnityEngine;
using System.Collections.Generic;

public class PolygonNodeSelector : MonoBehaviour
{
    public Grid grid;  // Reference to the grid component
    public GameObject squarePrefab;  // Reference to the square prefab
    public List<Vector3Int> selectedNodes = new List<Vector3Int>();  // Store selected grid positions
    private bool isPolygonClosed = false;  // Flag to check if the polygon is closed

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Detect left mouse click
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;  // Ensure the Z axis is zero if you're working in 2D

            // Convert the world position to the closest grid cell position
            Vector3Int gridPosition = grid.WorldToCell(mouseWorldPos);

            // If the polygon is not closed, add this grid position to the list of selected nodes
            if (!isPolygonClosed)
            {
                // Check if the clicked node is the starting node to close the polygon
                if (selectedNodes.Count > 0 && gridPosition == selectedNodes[0])
                {
                    ClosePolygon();
                }
                else
                {
                    selectedNodes.Add(gridPosition);
                    // Optionally, instantiate a visual indicator at the selected grid cell
                    Instantiate(squarePrefab, grid.GetCellCenterWorld(gridPosition), Quaternion.identity);
                }
            }
        }
    }

    void ClosePolygon()
    {
        Debug.Log("Polygon closed!");
        isPolygonClosed = true;

        // Check and instantiate squares on the polygon's edges and inside the polygon
        List<Vector3Int> allGridNodes = GetAllGridNodes();
        HashSet<Vector3Int> nodesToInstantiate = new HashSet<Vector3Int>();

        // Add the nodes that lie on the edges of the polygon
        for (int i = 0; i < selectedNodes.Count; i++)
        {
            Vector3Int start = selectedNodes[i];
            Vector3Int end = selectedNodes[(i + 1) % selectedNodes.Count];
            foreach (var node in GetNodesOnLine(start, end))
            {
                nodesToInstantiate.Add(node);  // Add edge nodes
            }
        }

        // Add the nodes inside the polygon using the winding number algorithm
        foreach (var node in allGridNodes)
        {
            if (IsPointInPolygon(grid.CellToWorld(node), selectedNodes))
            {
                nodesToInstantiate.Add(node);  // Add inner nodes
            }
        }

        // Instantiate squares on all the selected nodes
        foreach (var node in nodesToInstantiate)
        {
            Instantiate(squarePrefab, grid.GetCellCenterWorld(node), Quaternion.identity);
        }
    }

    // Bresenham's line algorithm to find all nodes between two points
    List<Vector3Int> GetNodesOnLine(Vector3Int start, Vector3Int end)
    {
        List<Vector3Int> lineNodes = new List<Vector3Int>();
        int x0 = start.x;
        int y0 = start.y;
        int x1 = end.x;
        int y1 = end.y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = (x0 < x1) ? 1 : -1;
        int sy = (y0 < y1) ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            lineNodes.Add(new Vector3Int(x0, y0, 0));

            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }

        return lineNodes;
    }

    // Function to get all the grid nodes (this can be customized to your grid size)
    List<Vector3Int> GetAllGridNodes()
    {
        List<Vector3Int> nodes = new List<Vector3Int>();
        // For example, assuming a 20x20 grid size for simplicity
        for (int x = -10; x < 10; x++)
        {
            for (int y = -10; y < 10; y++)
            {
                nodes.Add(new Vector3Int(x, y, 0));
            }
        }
        return nodes;
    }

    // Winding number algorithm to check if a point is inside the polygon
    public bool IsPointInPolygon(Vector3 point, List<Vector3Int> polygonVertices)
    {
        int windingNumber = 0;

        // Loop through all edges of the polygon
        for (int i = 0; i < polygonVertices.Count; i++)
        {
            Vector3Int v1 = polygonVertices[i];
            Vector3Int v2 = polygonVertices[(i + 1) % polygonVertices.Count];

            // Check if the point is on an upward or downward crossing
            if (v1.y <= point.y)
            {
                if (v2.y > point.y && IsLeft(v1, v2, point) > 0)
                {
                    windingNumber++;
                }
            }
            else
            {
                if (v2.y <= point.y && IsLeft(v1, v2, point) < 0)
                {
                    windingNumber--;
                }
            }
        }

        // Point is inside if winding number is non-zero
        return windingNumber != 0;
    }

    // Helper function to check if a point is left of an edge (v1 to v2)
    private float IsLeft(Vector3Int v1, Vector3Int v2, Vector3 p)
    {
        return (v2.x - v1.x) * (p.y - v1.y) - (p.x - v1.x) * (v2.y - v1.y);
    }
}
