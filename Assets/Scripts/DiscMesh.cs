using UnityEngine;

public class DiscMesh : MonoBehaviour
{
    [Range(0.1f,20f)]
    public float Radius = 5.0f;

    [Range(3,120)]
    public int Segments = 10;

    private void OnDrawGizmos()
    {
        float delta_angle = 360f / Segments;
        Vector3 point = Vector3.zero;
        for (int i=0; i<=Segments; i++)
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
