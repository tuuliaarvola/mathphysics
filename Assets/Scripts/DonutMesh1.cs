using System.Collections.Generic;
using UnityEngine;

public class DonutMesh : MonoBehaviour
{
    [SerializeField, Range(0.1f, 20f)] private float _radius = 5f;
    [SerializeField, Range(0.1f, 10f)] private float _thickness = 1f;
    [SerializeField, Range(3, 120)] private int _segments = 20;

    private Mesh _donutMesh;

    private void GenerateMesh()
    {
        if (_donutMesh == null) _donutMesh = new Mesh();
        else _donutMesh.Clear();

        List<Vector3> verts = new List<Vector3>();
        List<int> tri_indices = new List<int>();

        List<Vector2> uvs = new List<Vector2>();

        float innerRadius = Mathf.Max(0.01f, _radius - _thickness);
        float delta_angle = 360f / _segments;

        for (int i = 0; i < _segments; i++)
        {
            float angle = Mathf.Deg2Rad * delta_angle * i;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);            

            // OUTER VERTICES //
            verts.Add(new Vector3(_radius * cos, _radius * sin, 0f));

            Vector2 uvcoord = new Vector2(cos, sin);

            uvs.Add(0.5f * uvcoord * (innerRadius / _radius) + new Vector2(0.5f, 0.5f));

            // INNER VERTICES //
            verts.Add(new Vector3(innerRadius * cos, innerRadius * sin, 0f));                      
            
            uvs.Add(0.5f * uvcoord + new Vector2(0.5f, 0.5f));

            // VERTEX INDICES //
            int outer = i * 2;
            int inner = outer + 1;
            int nextOuter = ((i + 1) % _segments) * 2;
            int nextInner = nextOuter + 1;

            // 1ST TRIANGLE //
            tri_indices.Add(outer);
            tri_indices.Add(inner);
            tri_indices.Add(nextOuter);

            // 2ND TRIANGLE //
            tri_indices.Add(inner);
            tri_indices.Add(nextInner);
            tri_indices.Add(nextOuter);
        }
        
        _donutMesh.SetVertices(verts);
        _donutMesh.SetUVs(0, uvs);
        _donutMesh.SetTriangles(tri_indices, 0);
        _donutMesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        GenerateMesh();
        GetComponent<MeshFilter>().sharedMesh = _donutMesh;
        _donutMesh.name = "Donut";

        Gizmos.matrix = transform.localToWorldMatrix;

        foreach (Vector3 vertex in _donutMesh.vertices)
        {
            Gizmos.DrawSphere(vertex, 0.1f);
        }
    }
}
