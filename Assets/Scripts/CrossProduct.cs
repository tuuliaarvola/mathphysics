using UnityEditor;
using UnityEngine;

public class CrossProduct : MonoBehaviour
{
    public GameObject target;
    public void OnDrawGizmos()
    {
        Vector3 origin = transform.position; // position of the gameobject script is attached to
        Vector3 ray = transform.forward; // ray direction

        //Raycast
        RaycastHit hit;

        if (Physics.Raycast(origin, ray, out hit))
        {
            //Draw sphere to the hit point
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit.point, 0.2f);

            Handles.color = Color.yellow;
            Handles.DrawLine(origin, hit.point);

            Drawing.DrawVector(5 * hit.normal, hit.point, Color.cornflowerBlue, 1f);

            Drawing.DrawVector(5f * ray, hit.point, Color.green, 1f); // Look direction

            //Cross product (ristitulo)
            Vector3 cross = Vector3.Cross(hit.normal, ray);
            cross.Normalize();
            Drawing.DrawVector(5* cross, hit.point, Color.violetRed, 1f);

            Vector3 cross2 = Vector3.Cross(cross, hit.normal);
            cross2.Normalize();
            Drawing.DrawVector(5 * cross2, hit.point, Color.darkBlue, 1f);

            target.transform.position = hit.point;
            target.transform.rotation = Quaternion.LookRotation(cross2, hit.normal);
            

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
