using UnityEngine;

public class Reflect : MonoBehaviour
{

    public GameObject Source;

    private void OnDrawGizmos()
    {
        //Compute the ray from source to (0, 0, 0)
        // You can leave the Vector3.zero out 
        Vector3 ray = Vector3.zero -Source.transform.position;

        Drawing.DrawVector(ray, Source.transform.position, Color.darkRed, 2f);

        // Normal vector
        Vector3 normal = 5f * Vector3.up;
        Drawing.DrawVector(normal, Vector3.zero, Color.lawnGreen, 2f);

        //Compute reclection (using Unity's implementation)
        //NOTE: Vector3.Reclect() needs the normal vector to 
        Vector3 reflection = Vector3.Reflect(ray, normal.normalized);
        Drawing.DrawVector(reflection, Vector3.zero, Color.violetRed, 2f);
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
