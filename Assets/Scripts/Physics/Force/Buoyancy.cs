using UnityEngine;

public class Buoyancy : MonoBehaviour

{
    public float WaterLevel = 0f;

    public float WaterDensity = 1000f;

    public float ObjectVolume = 1f;

    public float ObjectHeight = 2f;

    public float ObjectMass = 100f;

    public float Gravity = 9.81f;

    private float Velocity = 0f;

    void Update()
    {
        // Find the bottom of the object
        float ObjectBottom = transform.position.y;

        // Calculate how much of the object is underwater
        float SubmergedFraction =
            (WaterLevel - ObjectBottom) / ObjectHeight;

        // Clamp the fraction between 0 and 1
        SubmergedFraction = Mathf.Clamp01(SubmergedFraction);

        // Calculate buoyancy force
        float BuoyancyForce =
            WaterDensity * ObjectVolume * Gravity * SubmergedFraction;

        // Calculate gravity force (weight)
        float GravityForce = ObjectMass * Gravity;

        // Calculate the total force
        float NetForce = BuoyancyForce - GravityForce;

        // Calculate acceleration using Newton's second law
        float Acceleration = NetForce / ObjectMass;

        // Calculate velocity
        Velocity += Acceleration * Time.deltaTime;

        // Move the object
        transform.position += Vector3.up * Velocity * Time.deltaTime;
    }
}

