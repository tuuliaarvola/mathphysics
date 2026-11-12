using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class NonUniformMotion : MonoBehaviour
{
 public GameObject MovingObject;
[Header("Motion Variables")]
public Vector3 Pos0; // x0
public Vector3 Vel0; // v0
public Vector3 Acc; //a
public float angle = 10f;

 [Header("Internal variables")]
 [SerializeField]
 private Vector3 vel;
    //public float Time = 0f;

    // From physics: x = x0 + v*t
    // v = v0 + a*t
    //(x = x0 + v0*t + 0.5*a*t*t)


    // Weekly task:
    // - we want to hit a target that's 2 kilometers
    // away (Sunila factory) from the campus
    // - our weapon of choice is a 1600th century black powder cannon
    // and the initial velocity is 250 m/s
    // - simulate this using our codebase from class
    // and find out what is elevation angle needed for the cannon?
    // - are we able to hit it? 
    // - if not, what is the maximum range?
    // (or what is the minimum v0 to hit the factory?)

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
    
    Gizmos.color = Color.magenta;
    float delta_t = 0.010f; // 10 ms

    Vector3 pos = Pos0; // initial position
    Vector3 vel = Vel0; // initial velocity
        for (int i = 0; i < 1000; i++)
    {
        // x= x0 + v*t
        pos = pos + vel*delta_t;

        // v = v0 + a*t
        vel = vel + Acc* delta_t;
        
        Gizmos.DrawSphere(pos, 0.10f);

            // Draw velocity arrows every 100 steps
            if (i % 100 == 0)
            {
                Drawing.DrawVector(vel, pos, Color.white, 2f);
            }

            // Stop drawing when the object hits the ground
            if (pos.y < 0)
            {
                break;
            }
        }
}
// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
{

        // set initial position for the gameobject
        //MovingObject.transform.position = Pos0;

        // set initial velocity (internal variable)
        //vel = Vel0;

        // the target is able to hit the goal of 2 km with initial velocity of 250 m/s
        // angle is calculated:
        // target distance = initial velocity^2 * sin(20) 7 gravity)
        // angle should be something between ~10-80 degrees)

        Vel0 = new Vector3
    (250f * Mathf.Cos(angle * Mathf.Deg2Rad),
     250f * Mathf.Sin(angle * Mathf.Deg2Rad),0f);

        // Set initial position of GameObject
        MovingObject.transform.position = Pos0;

        // Set initial velocity
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

        if (Mathf.Abs(MovingObject.transform.position.x - 2000f) < 0.1f)
        {
            Debug.Log("X is over 2000!");
        }

     

    }
}