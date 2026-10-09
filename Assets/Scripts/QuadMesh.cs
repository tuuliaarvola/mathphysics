using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]

public class QuadMesh : MonoBehaviour
{

    public Mesh quadmesh;

    // X, Y, Z
    Vector3 v0 = Vector3.zero;          // (0, 0, 0)
    Vector3 v1 = Vector3.right;         // (1, 0, 0)
    Vector3 v2 = Vector3.up;            // (0, 1, 0)
    Vector3 v3 = new Vector3(1, 1, 0);  // (1, 1, 0)

    private void OnDrawGizmos()
    {
        GenerateMesh();
        GetComponent<MeshFilter>().sharedMesh = quadmesh;
        Gizmos.DrawSphere(transform.position+v0, 0.05f);
        Gizmos.DrawSphere(transform.position + v1, 0.05f);
        Gizmos.DrawSphere(transform.position + v2, 0.05f);
        Gizmos.DrawSphere(transform.position + v3, 0.05f);

    }

    private void GenerateMesh()
    {
        if (quadmesh == null)
            quadmesh = new Mesh();

        else
            quadmesh.Clear();
        
      

        List<Vector3> verts = new List<Vector3>();
        verts.Add(v0);  //index 0
        verts.Add(v1);  // index 1
        verts.Add(v2);  // index 2
        verts.Add(v3);  //index 3

                                         // referoi aiempiin indexeihin
                                        //  2 kolimioo, 3 indices each
        int[] tri_indices = new int[6]; // 2x3 = 6 indices
        // 1st triangle
        tri_indices[0] = 0;
        tri_indices[1] = 2;
        tri_indices[2] = 3;
        // 2nd triangle
        tri_indices[3] = 0;
        tri_indices[4] = 3;
        tri_indices[5] = 1;


        quadmesh.SetVertices(verts);
        quadmesh.SetTriangles(tri_indices, 0);
        quadmesh.RecalculateNormals(); // automatically defines own normals
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
