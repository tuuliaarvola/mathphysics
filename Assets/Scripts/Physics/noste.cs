using UnityEngine;

public class noste : MonoBehaviour
    
{
    public float ObjectHeight = 2.0f; // adjustable object height
    public float ObjectVolume = 1.0f; // adjustable objest volume
    public float WaterDensity = 1000f;
    public float Gravity = 9.81f;
    public float WaterLevel = 0f; // y is 0
    public float ObjectWeight = 50f; // adjustable object weight
    public float Vel = 0f; // velocity


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // the gameobject's y
        float ObjectBottom = transform.position.y - ObjectHeight;

        // how submerged the object is in percents
        float SubmergePercent = (WaterLevel - ObjectBottom) / ObjectHeight;

        // we don't want negative numbers!!!
        SubmergePercent = Mathf.Clamp01(SubmergePercent);

        // calculate buoyancy
        // F = mass * submerged volume * gravity * submergepercet
                                                // -> buoyancy is different depending how submerged the object is
        float BuoyancyForce = WaterDensity * ObjectVolume * Gravity * SubmergePercent;

        // Calculate gravity
        // Force = mass * gravity
        float GravityForce = ObjectWeight * Gravity;

        // forces affecting the object
        float Force = BuoyancyForce - GravityForce;

        // acceleration = force / mass
        float Acceleration = Force / ObjectWeight;

        // how vel changes overtime
        Vel += Acceleration * Time.deltaTime;

        // calculate position of the object
        transform.position += Vector3.up * Vel * Time.deltaTime;


        
    }
}
