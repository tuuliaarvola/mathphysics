using UnityEditor;
using UnityEngine;

public class TerrainReflect : MonoBehaviour
{

    [Range(1,1000)]
    //int on tasaluku
    public int Iterations = 5;

    public void OnDrawGizmos()
    {
        Vector2 origin = transform.position; // position of the gameobject script is attached to
        Vector3 laser = transform.right; // laser direction

        //Raycast
        RaycastHit hit;

        //Repeat this code 5 times, i=0 and is incereased by 1 untill it's 5
        for (int i=0; i < Iterations; i++)
        {
            if (Physics.Raycast(origin, laser, out hit))
            {
                //we actually hit something

                //Draw line from gameobject position to hit point
                Handles.color = Color.darkRed;
                Handles.DrawLine(origin, hit.point);

                //Draw sphere at the hit point
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(hit.point, 0.2f);

                //Also draw the normal
                //Drawing.DrawVector(3f * hit.normal, hit.point, Color.rebeccaPurple, 2f);

                //Calculate reflection
                Vector3 reflection = Vector3.Reflect(laser, hit.normal);
                //Draw reflection
                //Drawing.DrawVector(3f * reflection, hit.point, Color.cyan, 2f);

                //Update origin and laser
                origin = hit.point;
                laser = reflection;
            }
            else
            {
                //we missed
                Debug.Log("Raycast missed");
            }
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
