
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [Header("Physics Object Properties")]
    [Range(0.001f,10000f)]
    public float Mass = 1f;  // In kilograms!!!
    public float GravityValue = 9.81f;  // Default gravity (acceleration downwards)
    public bool Gravity = true;


    [Header("Forces")]
    [SerializeReference]
    public List<IForce> Forces = new List<IForce>();
    //public IForce[] Forces = null;

    /*
    public Vector3 Force = Vector3.zero;
    public Vector3 ImpulseForce = Vector3.zero;
    public float ImpulseDuration = 0.01f; // In seconds (resolution of fixedUpdate()???)
    */

    [Header("Internal stuff")]
    public float Delay = 3f;
    // Internally, we use inverse mass!!! Needs to be computed "somewhere"...
    [SerializeField]
    private float InverseMass;
    // Just for seeing the computed values
    // -----------------------------------
    [SerializeField]
    private Vector3 Position = Vector3.zero;
    [SerializeField]
    private Vector3 Velocity = Vector3.zero;
    [SerializeField]
    private Vector3 Acceleration = Vector3.zero;
    [SerializeField]
    private float ImpulseTimer = 0f;

    // just for visualization in OnDrawGizmos
    private SpringForce _spring;

    public Vector3 GetVelocity() { return Velocity; }


    private void Awake()
    {
       _spring = GetComponent<SpringForce>();
    }
    private void UpdateInverseMass()
    {
        if (Mass > 0.00001f)  // For safety!
            InverseMass = 1.0f / Mass;
    }

    private void OnDrawGizmos()
    {
        if (_spring != null)
        {
            Handles.color = Color.black;
            Handles.DrawLine(_spring.AnchorPoint.position, Position, 3f);
            
        }
        // Acceleration vector
        //if (Acceleration.sqrMagnitude < 0.01f)
        //Drawing.DrawVector(Acceleration, Position, Color.yellow, 2f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find all the forces attached to this gameobject
        IForce[] all_forces = GetComponents<IForce>();
        Forces.AddRange(all_forces);

        UpdateInverseMass();

        Position = transform.position;  // Move the internal position to
                                        // whatever the gameobject's position is

        //ImpulseTimer = ImpulseDuration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.fixedTime < Delay) return;
        
        // Physics calculation:
        // - Newton: F = ma --> a = 1/m * F
        // -         v = v0 + a*t
        // -         x = x0 + v*t  (+ 0.5*a*t*t)

        // "Generic Force" - not anything specific
        /*
        Vector3 ResultantForce = Force;
        // Impulse Force
        if (ImpulseTimer > 0f)
        {   // Only add the impulse force if impulse timer hasn't gone to zero
            ResultantForce += ImpulseForce;
            ImpulseTimer -= Time.fixedDeltaTime;
        }
        */
        Vector3 ResultantForce = Vector3.zero;
        foreach (var f in Forces)
        {
            ResultantForce += f.CalculateForce(this);
        }

        // Compute the acceleration
        Acceleration = InverseMass * ResultantForce;
        if (Gravity)
            Acceleration.y -= GravityValue;

        // Compute the velocity
        Velocity = Velocity + Acceleration * Time.fixedDeltaTime;

        // Compute the position
        Position = Position + Velocity * Time.fixedDeltaTime;
        
        // Set the position of this gameobject
        transform.position = Position;
    }
}
