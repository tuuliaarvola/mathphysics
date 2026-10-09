using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class DiscMesh : MonoBehaviour
{
    [Range(0.1f,20f)]
    public float Radius = 5.0f;

    [Range(3,120)]
    public int Segments = 10;

    public Mesh discmesh;


    private void GenerateMesh()
    {
        if (discmesh == null)
            discmesh = new Mesh();

        else
            discmesh.Clear();

        List<Vector3> verts = new List<Vector3>();  
        verts.Add(Vector3.zero);

        float delta_angle = 360f / Segments;
        Vector3 point = Vector3.zero;
        for ( int i = 0; i < Segments; i++ )
        {
            point.x = Radius * Mathf.Cos(Mathf.Deg2Rad * (delta_angle * i));
            point.y = Radius * Mathf.Sin(Mathf.Deg2Rad * (delta_angle * i));
            verts.Add(point);
        }

        discmesh.SetVertices(verts);

        List<int> tri_indices = new List<int>();
        for (int i = 0;i < Segments-1;i++ )
        {
            tri_indices.Add(0);
            tri_indices.Add(i + 2);
            tri_indices.Add(i + 1);

        }

        tri_indices.Add(0);
        tri_indices.Add(1);
        tri_indices.Add(verts.Count-1);


        discmesh.SetTriangles(tri_indices, 0);
        discmesh.RecalculateNormals();


    }

    private void OnDrawGizmos()
    {

        GenerateMesh();
        GetComponent<MeshFilter>().mesh = discmesh;
        float delta_angle = 360f / Segments;
        Vector3 point = Vector3.zero;
        for (int i=0; i<Segments; i++)
        {
            point.x = Radius * Mathf.Cos(Mathf.Deg2Rad*(delta_angle * i));
            point.y = Radius * Mathf.Sin(Mathf.Deg2Rad * (delta_angle * i));
            Gizmos.DrawSphere(point, 0.1f);

        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
