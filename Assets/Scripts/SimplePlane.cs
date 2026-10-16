using System.Collections.Generic;
using UnityEngine;



public class SimplePlane : MonoBehaviour
{

    [Range(1, 100)]
    public int Segments = 10;

    [Range(0.1f, 1000f)]
    public float Size = 10f;

    public Mesh mymesh;

    public float NoiseAmplitude = 10f;
    [Range(0.01f, 2.0f)]
    public float NoiseFrequency = 1.0f;
    [Range(0f, 20f)]
    public float NoiseOffSetX = 0.0f;
    [Range(0f, 20f)]
    public float NoiseOffSetY = 0.0f;

    private Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.name = "SimplePlane";

        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> tri_indices = new List<int>();

        // the delta size between segments
        float delta = Size / (float)Segments;

        Vector3 point = Vector3.zero;
        for (int i=0; i<= Segments; i++)
        {
            for (int j=0; j <= Segments; j++)
            {
                point.x = j *delta;
                point.y = Mathf.PerlinNoise(NoiseFrequency * point.x + NoiseOffSetX,
                                            NoiseFrequency * point.z + NoiseOffSetY) * NoiseAmplitude;
                point.z = i * delta;
                verts.Add(point);
                uvs.Add(new Vector2 (j / (float)Segments, i / (float)Segments));
            }
        }

        for (int i = 0; i < Segments; i++)
        {
            for (int j = 0; j < Segments; j++)
            {

                int lower_left = i * (Segments + 1) + j;
                int lower_right = lower_left + 1;

                int upper_left = (i + 1) * (Segments + 1) + j;
                int upper_right = upper_left + 1;
                //current square
                tri_indices.Add(lower_left);
                tri_indices.Add(upper_right);
                tri_indices.Add(lower_right);

                tri_indices.Add(lower_left);
                tri_indices.Add(upper_left);
                tri_indices.Add(upper_right);
            }

        }

        mesh.SetVertices(verts);
        mesh.SetTriangles(tri_indices, 0);
        mesh.SetUVs(0, uvs);
        mesh.RecalculateNormals();
        return mesh;

    }

    private void OnDrawGizmos()
    {
        mymesh = GenerateMesh();
        GetComponent<MeshFilter>().sharedMesh = mymesh;
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
