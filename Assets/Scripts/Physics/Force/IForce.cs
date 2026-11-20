using UnityEngine;

public interface IForce
{
    // Each force needs to be able to 
    // Calculate itself, and the physics object
    // is responsible for adding it to the resultant force
    public Vector3 CalculateForce(PhysicsObject pobj);
}
