using UnityEngine;
using UnityEngine.InputSystem;

public class NonUniformMotion : MonoBehaviour
{
 public GameObject MovingObject;
[Header("Motion Variables")]
public Vector3 Pos0; // x0
public Vector3 Vel0; // v0
public Vector3 Acc; //a

 [Header("Internal variables")]
 [SerializeField]
 private Vector3 vel;
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

        // set initial position for the gameobject
    MovingObject.transform.position = Pos0;

        // set initial velocity (internal variable)
    vel = Vel0;

}

// Update is called once per frame
void Update()
{

        // Get the current pos
    Vector3 pos = MovingObject.transform.position;
        // update pos
    pos = pos + vel * Time.deltaTime;
        // update vel
    vel = vel + Acc* Time.deltaTime;

    // Try to get WASD and modify velocity
    if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            vel += Vector3.up * 5f;
        }

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            vel += Vector3.left * 5f;
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            vel += Vector3.right * 5f;
        }

        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            vel += Vector3.down * 5f;
        }

        // set the position of the gameobject
        MovingObject.transform.position = pos;

}
}