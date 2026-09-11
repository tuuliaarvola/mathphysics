using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class lerp : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject Platform; // actual gameobject to be moved

    [Range(0f, 10f)]
    public float InterpTime = 10f;

    [Range(0f, 1f)]
    public float T = 0f;

    public void OnDrawGizmos()
    {
        // get position of A into vector Apos
        Vector3 Apos = A.transform.position;
        // get position of B into vector Bpos
        Vector3 Bpos = B.transform.position;

        // Draw vectors a and b
        Drawing.DrawVector(Apos, Vector3.zero, Color.white, 2f);
        Drawing.DrawVector(Bpos, Vector3.zero, Color.white, 2f);

        //compute interpolation (x = (1-t)*A + t*B)
        Vector3 part_of_a = (1f - T) * Apos;
        Drawing.DrawVector(part_of_a, Vector3.zero, Color.magenta, 1.5f);

        Vector3 part_of_b = T * Bpos;
        Drawing.DrawVector(part_of_b, Vector3.zero, Color.yellow, 1.5f);

        // Draw vector part_of_a + part_of_b
        Vector3 sum = part_of_a + part_of_b;
        Drawing.DrawVector(sum, Vector3.zero, Color.cyan, 1.5f);


        Drawing.DrawVector(part_of_b, part_of_a, Color.yellow, 1.5f);
        Drawing.DrawVector(part_of_a, part_of_b, Color.magenta, 1.5f);

        if (null != Platform)
        {
            Platform.transform.position = sum;
        }


        // Draw dotted line from A to B
        Handles.DrawDottedLine(Apos, Bpos, 4f);

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (null != Platform)
        {

            float curr_time = Time.time;
            float t = curr_time / InterpTime;
            if (t > 1f) { t = 1f; } //same as Mathf.clamp01(t)

            // compute interpolation (x = (1-t) * A + t*B)
            Vector3 interp_pos = (1 - t) * A.transform.position + t * B.transform.position;
            Platform.transform.position = interp_pos;
        }
    }
}
