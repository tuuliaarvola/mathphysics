using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Particle : MonoBehaviour
{

    [Header("Particle")]
    // Model for our physics particle
    [Range(0.01f, 1000)]
    public float Mass = 1.0f; // default mass: 1 kg
    public Vector3 Position = Vector3.zero;
    public Vector3 Velocity = Vector3.zero;

    // Forces:
    // -------------------
    // Earth-like gravity
    [Header("Gravity")]
    public Vector3 Gravity = new Vector3(0f, -9.81f, 0f);
    public bool UseGravity = false;

    [Header("Wind")]
    public float SurfaceArea = 1.0f;
    public Vector3 WindForce = Vector3.zero;
    public bool UseWind = false;

    [Header("Spring Force")]
    [Range(0.1f, 100f)]
    public float SpringConstant = 5.0f;
    public GameObject SpringLocationObject;
    public float NormalLength = 3f;
    private Vector3 SpringLocation;
    public bool UseSpring = false;
    bool ApplySpring = false;

    [Header("Buoyancy")]
    public float WaterHeight = 0.0f;
    public float rho = 1000.0f; // 1000 kilograms per cubic meter
    public bool UseBuoyancy = false;

    [Header("Misc")]
    public bool AirResistance = false;

    private void OnDrawGizmos()
    {
        if (ApplySpring)
            Handles.color = Color.black;
        else 
            Handles.color = Color.white;
        if (UseSpring)
            Handles.DrawLine(Position, SpringLocation, 3f);

        // Water level
        if (UseBuoyancy)
        {
            Handles.color = Color.blue;
            Handles.DrawLine(new Vector3(-100.0f, WaterHeight, 0.0f),
                             new Vector3(100.0f, WaterHeight, 0.0f), 3f);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
        SpringLocation = SpringLocationObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Zero out the resultant force
        Vector3 ResultantForce = Vector3.zero;

        // Add gravity:
        if (UseGravity)
            ResultantForce += Gravity * Mass; // G = mg
        // ... add other forces to the resultant!

        // Add windforce:
        if (UseWind)
            ResultantForce += WindForce * SurfaceArea;

        // Spring force
        // ------------
        // Compute vector from particle to spring location
        Vector3 spring_vector = SpringLocation - Position;
        float delta_l = spring_vector.magnitude - NormalLength;

        ApplySpring = false;
        // Bungee?
        if (delta_l > 0)
        {
            ApplySpring = true;
        }

        // F = -k * delta_l (* spring_vector.normalized)
        Vector3 SpringForce = SpringConstant * delta_l * spring_vector.normalized;
        // Add springforce:
        if (ApplySpring && UseSpring)
        {
            ResultantForce += SpringForce;
        }

        // Lift / Buoyancy
        // ----------------
        Vector3 LiftForce = Vector3.zero;

        // Volume of the _cube_ !!!
        float V = transform.localScale.x * transform.localScale.y * transform.localScale.z;

        float height = transform.localScale.y;
        float BottomLevel = Position.y - height / 2.0f;
        float TopLevel = Position.y + height / 2.0f;
        // 1. case: totally airborne, not under water
        if (BottomLevel >= WaterHeight)
        {
            // No lift/buoyancy!
            LiftForce = Vector3.zero;
        }
        // 2. case: totally under water
        else if (TopLevel - WaterHeight < -0.01f)
        {
            // F     =   rho   *    V   *   g
            // Force = density x Volume x gravity (opposite the gravity)
            LiftForce =  rho * V * (-Gravity);
        }
        // 3. case: Partially under water
        else
        {
            // How many percent is the object under water???
            float percentage = 0.95f*(WaterHeight - BottomLevel) / height;
            //Debug.Log("%:" + percentage);

            // Force = percentage*Volume * density * gravity
            LiftForce = rho * V * percentage * (-Gravity);
            //Debug.Log(LiftForce.y);
        }

        // Add the lift force
        if (UseBuoyancy)
            ResultantForce += LiftForce;
        // end lift / Buoyancy
        // -------------------------------------------------------



        // 1. Solve acceleration from resultant force
        Vector3 Acceleration = ResultantForce / Mass;

        // 2. Update velocity 
        Velocity += Acceleration * Time.fixedDeltaTime;

        // (Fake air resistance)
        if (AirResistance)
            Velocity *= (1f - 0.1f*Time.fixedDeltaTime);

        // Under water?
        if (BottomLevel < WaterHeight)
            Velocity *= (1f - Time.fixedDeltaTime);  // Do a really heavy drag!

        // 3. Update position
        Position += Velocity * Time.fixedDeltaTime;

        /*
        if (Position.y < 0)
        {
            Position.y = 0f;
            // Should we stop the particle
            Time.timeScale = 0.0f;
        }
        */


        // Finally, set the gameobject to our newly computed
        // Position
        transform.position = Position;

    }
}
