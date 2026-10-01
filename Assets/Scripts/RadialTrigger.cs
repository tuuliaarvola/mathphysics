using UnityEditor;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    [Range(1f, 10f)]
    public float Radius = 1f;

    [Range(1f, 180f)]
    public float FieldOfView = 1f;

    [Range(1f, 10f)]
    public float Height = 1f;

    public GameObject IntersectTarget;
    public GameObject LookAtTarget;
    // The code below makes the variable private but shows it in the inspector
    // Transform holds reference to gameobject transform and makes writing code shorter
    // So overall a better way to write the code
    // [SerializeField] private Transform _intersectTarget;

    public bool Triggered = false;

    private bool IsTriggered()
    {
        if (IntersectTarget == null) return false;
        if (LookAtTarget== null) return false;

        // Vertical distance trigger
       Vector3 difference = IntersectTarget.transform.position - transform.position;

       if (Mathf.Abs(difference.y) > Height / 2f) return false;

       // Horizontal distance trigger
       Vector3 horizontalTargetDirection = new Vector3(difference.x, 0f, difference.z);

        if (horizontalTargetDirection.magnitude > Radius) return false;

        //Look at trigger
        Vector3 lookDirection = LookAtTarget.transform.position - transform.position;

        lookDirection.y = 0f;

        lookDirection.Normalize();
        horizontalTargetDirection.Normalize();

        float dot = Vector3.Dot(lookDirection, horizontalTargetDirection);

        float treshhold = Mathf.Cos((FieldOfView / 2f) * Mathf.Deg2Rad);
        return dot >= treshhold;






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

