using UnityEngine;
using UnityEngine.InputSystem;

public class NonUniformMotion2 : MonoBehaviour
{

    public GameObject MovingObject;

    [Header("Motion variables")]
    public Vector3 Pos0;  // x0    
    [Range(0.1f, 1000f)]
    public float InitialVelocity = 250.0f;
    public float Angle = 5.0f;  // Degrees!
    public Vector3 Acc;   // a
    [Range(0, 9000)]
    public int Steps = 1000;
    public float DragCoefficient = 0.999f;

    [SerializeField]
    private Vector3 Vel0;   // v0

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



    [Header("Internal variables")]
    [SerializeField]
    private Vector3 vel;

    //public float Time = 0f;

    // From physics: x = x0 + v*t
    //               v = v0 + a*t
    //              (x = x0 + v0*t + 0.5*a*t*t)

    private void ComputeInitialVelocity()
    {
        float Vx = InitialVelocity * Mathf.Cos(Mathf.Deg2Rad * Angle);
        float Vy = InitialVelocity * Mathf.Sin(Mathf.Deg2Rad * Angle);

        Vel0.x = Vx;
        Vel0.y = Vy;
        Vel0.z = 0f;
    }

    private void OnDrawGizmos()
    {
        // Make sure we have computed the velocity vector correctly
        ComputeInitialVelocity();

        // Draw the x- and y-axes
        Drawing.DrawVector(10f * Vector3.right, Vector3.zero,
                           Color.red, 2f);

        Drawing.DrawVector(10f * Vector3.up, Vector3.zero,
                    Color.green, 2f);

        // Draw the starting position
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(Pos0, 0.25f);

        // Draw the velocity vector at the starting position
        Drawing.DrawVector(Vel0, Pos0, Color.cyan, 2f);

        Gizmos.color = Color.red;
        float delta_t = 0.010f;  // 10 ms

        Vector3 pos = Pos0;  // Initial position
        Vector3 vel = Vel0;  // Initial velocity
        for (int i = 1; i <= Steps; i++)
        {
            // x = x0 + v*t
            pos = pos + vel * delta_t;

            // v = v0 + a*t
            vel = vel + Acc * delta_t;

            // Multiply velocity by the drag coefficient
            vel *= DragCoefficient;

            Gizmos.DrawSphere(pos, 0.50f);
        }

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ComputeInitialVelocity();
        // Set initial position for the gameobject
        MovingObject.transform.position = Pos0;
        // Set initial velocity (internal variable)
        vel = Vel0; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < 6f)
            return;

        // Get the current position
        Vector3 pos = MovingObject.transform.position;
        // Update position
        pos = pos + vel * Time.deltaTime;
        // Update velocity
        vel = vel + Acc * Time.deltaTime;
        // Multiply velocity by the drag coefficient
        vel *= DragCoefficient;

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