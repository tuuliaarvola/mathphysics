using UnityEngine;

public class DragForce : MonoBehaviour, IForce
{
    [Range(0.01f, 0.99f)]
    public float DragCoeffiencent = 0.3f;
    public Vector3 CalculateForce(PhysicsObject pobj)
    {
        // we need access to the physics object's velocity
        Vector3 vel = pobj.GetVelocity();
        // from physics: F = 0.5*rho'v^2*C*A -> F = (-)C*v^2
        // opposite direction w.r.t. velocity
        Vector3 f = DragCoeffiencent * vel.sqrMagnitude * (-vel.normalized);

        return f;
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
