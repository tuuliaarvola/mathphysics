using UnityEngine;

public class ImpulseForce : MonoBehaviour, IForce
{
    public Vector3 Impulse;
    public float Duration = 1.0f;

    // We inherit IForce, so we must implement CalculateForce()
    
    public Vector3 CalculateForce(PhysicsObject pobj)
    {
        //if current time is less than duration
        if (Time.fixedTime < Duration + pobj.Delay)
        {
            return Impulse; // return the impulse vector
        }
        //more time has passed than the duration, so return zero-vector
        return Vector3.zero;
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
