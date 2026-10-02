using UnityEngine;

public class Reflect : MonoBehaviour
{

    public GameObject Source;

    private void OnDrawGizmos()
    {
        //Compute the ray from source to (0, 0, 0)
        // You can leave the Vector3.zero out 
        Vector3 ray = Vector3.zero - Source.transform.position;
        Drawing.DrawVector(ray, Source.transform.position, Color.darkRed, 2f);

        // Normal vector
        Vector3 normal = Vector3.up; // y-direction, magnitude is 1
        Drawing.DrawVector(5f * normal, Vector3.zero, Color.lawnGreen, 2f);

        //Compute reclection (using Unity's implementation)
        //NOTE: Vector3.Reclect() needs the normal vector to 
        Vector3 reflection = Vector3.Reflect(ray, normal); //vector to be reflected (incoming vector) = ray, normal vector = normal  
        Drawing.DrawVector(reflection, Vector3.zero, Color.violetRed, 2f);

        //Compute the "projected" vector
        Vector3 projected = Vector3.Dot(ray, normal) * normal;
        Drawing.DrawVector(projected, Vector3.zero, Color.white, 2f);

        //Draw the ray again, starting from the origin
        Drawing.DrawVector(ray, Vector3.zero, Color.darkRed, 2f);

        //Draw the projected vector but in the opposite direction
        //starting at the end of the ray
        Drawing.DrawVector(-projected, ray, Color.white, 2f);

        //And apped one more projected vector but in the opposite direction
        Drawing.DrawVector(-projected, ray-projected, Color.white, 2f);


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
