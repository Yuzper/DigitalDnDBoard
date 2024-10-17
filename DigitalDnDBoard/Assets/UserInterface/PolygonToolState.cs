using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class PolygonToolState : ToolState
{
    public Grid grid;  // Reference to the grid component
    public List<Vector3> points = new List<Vector3>();
    public GameObject gridIndicator; //Shows nearest point in the grid.
    
    public override void EnterState()
    {
        base.EnterState();
        grid = GameObject.FindFirstObjectByType<Grid>();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0))  // Detect left mouse click
        {
            //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector3 gridPosition = grid.CellToWorld(grid.WorldToCell(mouseWorldPos));

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cellCornerPos = grid.CellToWorld(grid.WorldToCell(mouseWorldPos)); // Bottom-left corner of the cell
            Vector3 cellSize = grid.cellSize;  // Size of each cell

            // To find the center, add half the size of the cell to the corner position
            Vector3 gridPosition = new Vector3(
                cellCornerPos.x + cellSize.x / 2,
                cellCornerPos.y + cellSize.y / 2,
                0.1f // Optional for 3D grids, ignore for 2D
            );

            Debug.Log("w");
            //We dont want to add multiple of the same point in a row and we dont want to create a polygon with only one point since thats just a dot.
            if(points.Count > 0 && gridPosition == points[^1]) return; //Might need to change to break.

            if( points.Count > 2 &&                         //At least 3 points else its a line or a dot.
                points.Contains(gridPosition))              //is already in the list and therefore closes the polygon and we can create it.
            {
                CreatePolygonFromMesh();
            }else{
                AddPointToPolygon(gridPosition);
            }
            
        }

    }

    public void AddPointToPolygon(Vector3 point){
        Debug.Log($"Point added to polygon: {point}");
        points.Add(point);
    }

    //QUESTION: Should the polygon be created when you click the first point or just any point in the list as long as the list is at least 3 points long and the one you clicked isnt the immediately previous point?
    public void CreatePolygonFromMesh(){
        //float m_RadiusMin = 1.5f;
        //float m_RadiusMax = 2f;
        //float m_Height = 1f;
        //bool m_FlipNormals = false;

        Debug.Log("Polygon Created");
        var go = new GameObject("Polygon");
        var m_mesh = go.AddComponent<ProBuilderMesh>();
        m_mesh.CreateShapeFromPolygon(points.ToArray(), 1f, false);
        Reset();
    }

    public void Reset(){
        points.Clear();
    }

    //Temporarily draw the current polygon. reset when it is complete.
    public void DrawTempPolygon(){

    }
}
