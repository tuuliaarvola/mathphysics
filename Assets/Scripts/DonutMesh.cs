using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatDonut : MonoBehaviour
{

    public Mesh donutmesh;

    [Range(3, 250)]
    public int Segments = 6;

    [Range(0.01f, 100f)]
    public float InnerRadius = 1.0f;

    [Range(0.01f, 100f)]
    public float OuterRadius = 1.0f;

    void GenerateMesh()
    {
        if (null == donutmesh)
        {
            donutmesh = new Mesh();
            donutmesh.name = "Donut";
        }
        else
            donutmesh.Clear();

        // List of vertices
        List<Vector3> vertices = new List<Vector3>();

        // Calculate the angle covered by each segment
        float delta_angle = 360.0f / Segments;
        // Go trough segments and create cordinates
        for (int i = 0; i < Segments; i++)
        {
            // calculate segment's angle
            float angle = delta_angle * i;
            // calculate x position of inner vertex. cos for horizontal position
            float x_inner = InnerRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
            // calculate x position of outer vertex. InnerRadius + OuterRadius gives the outer radius
            float x_outer = (InnerRadius + OuterRadius) * Mathf.Cos(Mathf.Deg2Rad * angle);
            // calculate z position of inner vertex. sin for depth position
            float z_inner = InnerRadius * Mathf.Sin(Mathf.Deg2Rad * angle);
            // calculate z position of outer vertex
            float z_outer = (InnerRadius + OuterRadius) * Mathf.Sin(Mathf.Deg2Rad * angle);

            vertices.Add(new Vector3(x_inner, 0, z_inner));
            vertices.Add(new Vector3(x_outer, 0, z_outer));
        }

        // gives vertex positions to the mesh
        donutmesh.SetVertices(vertices);

        // List for forming triangles
        List<int> tri_indices = new List<int>();
        // Loop through segments and create triangle indices
        for (int i = 0; i < Segments - 1; i++)
        {
            int inner_current = i * 2;
            int outer_current = inner_current + 1;

            int inner_next = outer_current + 1;
            int outer_next = inner_next + 1;

            tri_indices.Add(inner_current);
            tri_indices.Add(outer_next);
            tri_indices.Add(outer_current);

            tri_indices.Add(inner_current);
            tri_indices.Add(inner_next);
            tri_indices.Add(outer_next);
        }
        // Connect last and first segment
        tri_indices.Add((Segments - 1) * 2);
        tri_indices.Add(1);
        tri_indices.Add((Segments - 1) * 2 + 1);

        tri_indices.Add((Segments - 1) * 2);
        tri_indices.Add(0);
        tri_indices.Add(1);

        donutmesh.SetTriangles(tri_indices, 0);
        // Compute normals
        donutmesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        GenerateMesh();
        GetComponent<MeshFilter>().sharedMesh = donutmesh;

    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateMesh();
        GetComponent<MeshFilter>().sharedMesh = donutmesh;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
