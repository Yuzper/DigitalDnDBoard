using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

public class GenerateMesh : MonoBehaviour
{
    public float m_RadiusMin = 1.5f;
    public float m_RadiusMax = 2f;
    public float m_Height = 1f;
    public bool m_FlipNormals = false;
    ProBuilderMesh m_Mesh;
    // Start is called before the first frame update
    void Start()
    {
        var go = new GameObject();

        // Add a ProBuilderMesh component (ProBuilder mesh data is stored here)
        m_Mesh = go.gameObject.AddComponent<ProBuilderMesh>();

        Invoke("Rebuild", 0f);
    }

    void Rebuild()
    {
        // Create a circle of points with randomized distance from origin.
        Vector3[] points = {
            new Vector3(0, 0, 0.1f),
            new Vector3(1, 5, 0.1f),
            new Vector3(-3, 7, 0.1f),
            new Vector3(5, 6, 0.1f),
        };

        // CreateShapeFromPolygon is an extension method that sets the pb_Object mesh data with vertices and faces
        // generated from a polygon path.
        m_Mesh.CreateShapeFromPolygon(points, m_Height, m_FlipNormals);
    }
}
