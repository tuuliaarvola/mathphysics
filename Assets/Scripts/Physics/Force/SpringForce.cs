using UnityEngine;

public class SpringForce : MonoBehaviour, IForce
{
    public Transform AnchorPoint; // anchor point for the spring
    public float NormalLength = 1.0f; // the "resting" lenght of the spring
    public float SpringConstant = 1.0f; // the spring constant
    public Vector3 CalculateForce(PhysicsObject pobj)
    {
        // hooke's law: F = -k * x (k = spring constant, x = deviation)
        Vector3 v = pobj.transform.position - AnchorPoint.position;
        float deviation = v.magnitude - NormalLength;

        return -SpringConstant * v.normalized * deviation;
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
