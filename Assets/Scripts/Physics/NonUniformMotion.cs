using UnityEngine;

public class NonUniformMotion : MonoBehaviour
{
 public GameObject MovingObject;

public Vector3 Pos0; // x0
public Vector3 Vel0; // v0
    public Vector3 Acc; //a
                    //public float Time = 0f;

// From physics: x = x0 + v*t
// v = v0 + a*t
//(x = x0 + v0*t + 0.5*a*t*t)

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
    Drawing.DrawVector(Vel0, Pos0, Color.cyan, 2f);
    
    Gizmos.color = Color.pink;
    float delta_t = 0.010f; // 10 ms

    Vector3 pos = Pos0; // initial position
    Vector3 vel = Vel0; // initial velocity
        for (int i = 1; i < 250; i++)
    {
        // x= x0 + v*t
        pos = pos + vel*delta_t;

        // v = v0 + a*t
        vel = vel + Acc* delta_t;
        
        Gizmos.DrawSphere(pos, 0.10f);
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
    Vector3 pos = Pos0 + Time.time * Vel0;

    MovingObject.transform.position = pos;

}
}