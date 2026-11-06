using UnityEngine;

public class UniformMotion : MonoBehaviour
{

    public GameObject MovingObject;

    public Vector3 Pos0; // x0
    public Vector3 Vel; // Velocity = v
    //public float Time = 0f;

    // From physics: x = x0 + v*t

    private void OnDrawGizmos()
    {
        // Draw the x and y axes
        //x
        Drawing.DrawVector(10f * Vector3.right, Vector3.zero, Color.red, 2f);
        //y
        Drawing.DrawVector(10f * Vector3.up, Vector3.zero, Color.green, 2f);

        // Draw the starting position
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(Pos0, 0.25f);

        // Draw the velocity vector at the starting pos
        Drawing.DrawVector(Vel, Pos0, Color.cyan, 2f);

        Vector3 pos = Vector3.zero;
        for (int i = 1; i< 10; i++)
        {
            pos = Pos0 + i * Vel;
            Gizmos.color = Color.pink;
            Gizmos.DrawSphere(pos, 0.25f);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // x = x0 + v*t
        Vector3 pos = Pos0 + Time.time * Vel;

        MovingObject.transform.position = pos;
        
    }
}
