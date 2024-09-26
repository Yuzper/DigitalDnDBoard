using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;
using System.Collections.Generic;

public class GenerateMesh : MonoBehaviour
{
    public Grid grid;  // Reference to the grid component
    private bool isPolygonClosed = false;  // Flag to check if the polygon is closed

    public float m_RadiusMin = 1.5f;
    public float m_RadiusMax = 2f;
    public float m_Height = 1f;
    public bool m_FlipNormals = false;
    ProBuilderMesh m_Mesh;
    public List<Vector3> points = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        var go = new GameObject();

        // Add a ProBuilderMesh component (ProBuilder mesh data is stored here)
        m_Mesh = go.gameObject.AddComponent<ProBuilderMesh>();
        go.gameObject.AddComponent<FadeOnClick>();

    //    Invoke("Rebuild", 0f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Detect left mouse click
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Convert the world position to the closest grid cell position
            Vector3 gridPosition = grid.CellToWorld(grid.WorldToCell(mouseWorldPos));
            gridPosition.z = 0.1f;
            Debug.Log(gridPosition);

            // If the polygon is not closed, add this grid position to the list of selected nodes
            if (!isPolygonClosed)
            {
                // Check if the clicked node is the starting node to close the polygon
                if (points.Count > 0 && gridPosition == points[0])
                {
                    ClosePolygon();
                }
                else
                {
                    AddNodeToGrid(gridPosition);
                }
            }
            if (isPolygonClosed)
            {
                Rebuild();
            }
            
        }
    }

    void ClosePolygon()
    {
        Debug.Log("Polygon closed!");
        isPolygonClosed = true;
    }

    public void AddNodeToGrid(Vector3 newPoint)
    {
        newPoint.z = 0.1f;
        points.Add(newPoint);
    }

    public void Rebuild()
    {
        // CreateShapeFromPolygon is an extension method that sets the pb_Object mesh data with vertices and faces
        // generated from a polygon path.
        m_Mesh.CreateShapeFromPolygon(points.ToArray(), m_Height, m_FlipNormals);
    }

}
