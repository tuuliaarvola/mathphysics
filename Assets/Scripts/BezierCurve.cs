using UnityEditor;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public GameObject  A, B, C, D;

    [Range(0f, 1f)]
    public float T = 0.0f;

    private void OnDrawGizmos()
    {
        Vector3 a = A.transform.position;
        Vector3 b = B.transform.position;
        Vector3 c = C.transform.position;
        Vector3 d = D.transform.position;

        Handles.DrawLine(a, b, 2f);
        Handles.DrawLine(b, c, 2f);
        Handles.DrawLine(c, d, 2f);

        //1st interpolation between a and b
        Vector3 x = Vector3.Lerp(a, b, T);
        Gizmos.DrawSphere(x, 0.2f);
        Handles.Label(x + Vector3.up * 0.3f + Vector3.right*(-0.3f), "X");

        //2nd interpolation between b and c
        Vector3 y = Vector3.Lerp(b, c, T);
        Gizmos.DrawSphere(y, 0.2f);
        Handles.Label(y + Vector3.up * 0.3f + Vector3.right * (-0.3f), "Y");

        //3rd interpolation between c and d
        Vector3 z = Vector3.Lerp(c, d, T);
        Gizmos.DrawSphere(z, 0.2f);
        Handles.Label(z + Vector3.up * 0.3f + Vector3.right * (-0.3f), "Z");

        // Lines between x and y, and y and z
        Handles.DrawLine(x, y, 2f);
        Handles.DrawLine(y, z, 2f);

        // 4th interpolation between x and y
        Vector3 r = Vector3.Lerp(x, y, T);
        Gizmos.color = Color.plum;
        Gizmos.DrawSphere(r, 0.2f);
        Handles.Label(r + Vector3.up * 0.3f + Vector3.right * (-0.3f), "R");

        //5th interpolation between x and y
        Vector3 s = Vector3.Lerp(y, z, T);
        Gizmos.DrawSphere(s, 0.2f);
        Handles.Label(s + Vector3.up * 0.3f + Vector3.right * (-0.3f), "S");

        // Line between r and s
        Handles.color = Color.plum;
        Handles.DrawLine(r, s, 2f);

        // 6th interpolation between r and 2
        Vector3 o = Vector3.Lerp(r, s, T);
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(o, 0.2f);

        // Finally draw the bezier curve
        Handles.DrawBezier(a, d, b, c, Color.black, null, 2f);


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
