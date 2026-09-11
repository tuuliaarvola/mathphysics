using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawingTest : MonoBehaviour
{
    public GameObject VectorTo;


    private void OnDrawGizmos()
    {
        Vector3 v = VectorTo.transform.position;
        Handles.color = Color.red;
        // start from the origin, end at VectorTo.transform.position
        Handles.DrawLine(Vector3.zero, v, 3f);

        Handles.ConeHandleCap(0, v-v.normalized*0.35f, Quaternion.LookRotation(v), 0.5f, EventType.Repaint);
        Handles.color = Color.white;
        Handles.DrawLine(new Vector3(0, 0, 0), new Vector3(0, 1080, 0), 3f);

        Handles.DrawLine(new Vector3(0, 0, 0), new Vector3(1920, 0, 0), 3f);

        Handles.DrawLine(new Vector3(1920, 0, 0), new Vector3(1920, 1080, 0), 3f);

        Handles.DrawLine(new Vector3(1920, 1080, 0), new Vector3(0, 1080, 0), 3f);
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
