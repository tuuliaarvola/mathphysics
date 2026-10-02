using System;
using UnityEditor;
using UnityEngine;

public class WedgeTrigger : MonoBehaviour
{

    [Range(0.1f, 20f)]
    public float Radius = 5f;

    [Range(0.1f, 20f)]
    public float Height = 2f;


    //[Range(-1f, 1f)]
    //public float Threshold = 0.86f;
    [Range(0f, 360f)]
    public float FOVDegrees = 90f;
    [SerializeField]
    private float Threshold = Mathf.Cos(Mathf.Deg2Rad * 45f);

    public GameObject Target;
    public GameObject LookingAt;

    public bool Triggered = false;

    private bool IsTriggered()
    {
        if (Target == null)
            return false;

        if (Target.transform.position.y < transform.position.y)
            return false;

        if (Target.transform.position.y > transform.position.y + Height)
            return false;


        Threshold = Mathf.Cos(Mathf.Deg2Rad * FOVDegrees / 2f);

        // Local vector variables
        Vector3 trigger = transform.position;
        Vector3 target = Target.transform.position;
        Vector3 looking = LookingAt.transform.position;

        Vector3 trigger_to_target = target - trigger;  // vector math!!!
        Vector3 trigger_to_lookat = looking - trigger;

        trigger_to_target.y = 0f;

        if (trigger_to_target.sqrMagnitude > Radius * Radius)
            return false;

        //return trigger_to_target.magnitude <= Radius;
        return Vector3.Dot(trigger_to_lookat.normalized,
                           trigger_to_target.normalized) > Threshold;
    }

    private void OnDrawGizmos()
    {
        // Are we triggered???
        Triggered = IsTriggered();
        if (Triggered)
            Handles.color = Color.red;
        else
            Handles.color = Color.green;

        //Handles.DrawWireDisc(transform.position, Vector3.up, Radius);

        // Local vector variables
        Vector3 trigger = transform.position;
        Vector3 target = Target.transform.position;
        Vector3 looking = LookingAt.transform.position;

        Vector3 trigger_to_target = target - trigger;  // vector math!!!
        Vector3 trigger_to_lookat = looking - trigger;

        // Draw vectors from origin to trigger & target
        //Drawing.DrawVector(trigger, Vector3.zero, Color.paleGreen, 2f);
        //Drawing.DrawVector(target, Vector3.zero, Color.paleGreen, 2f);

        // Draw vector from trigger to target
        Drawing.DrawVector(trigger_to_target, trigger, Color.darkMagenta, 2f);
        // Draw vector from trigger to looking at
        Drawing.DrawVector(trigger_to_lookat, trigger, Color.darkMagenta, 2f);

        // Draw NORMALIZED vector from trigger to target
        //Drawing.DrawVector(trigger_to_target.normalized, trigger, Color.white, 2f);
        // Draw NORMALIZED vector from trigger to looking at
        //Drawing.DrawVector(trigger_to_lookat.normalized, trigger, Color.white, 2f);

        //Drawing.DrawVector(Radius*trigger_to_lookat.normalized, trigger, Color.white, 2f);
        // Create a quaternion that rotates around the y-axis
        Quaternion rot = Quaternion.AngleAxis(FOVDegrees / 2f, Vector3.up);
        // Take the original vector and rotate it
        Vector3 direction = Radius * trigger_to_lookat.normalized;
        Vector3 direction_rot = rot * direction;

        // Draw the rotated vector
        Color col = Color.green;
        if (Triggered)
            col = Color.red;

        // Trigger level edge
        Drawing.DrawVector(direction_rot, trigger, col, 2f);
        // Upper level edge
        Drawing.DrawVector(direction_rot, trigger + Vector3.up * Height, col, 2f);
        // Vertical line on the edge
        Handles.DrawLine(trigger + direction_rot, trigger + direction_rot + Vector3.up * Height, 2f);


        rot = Quaternion.AngleAxis(-FOVDegrees / 2f, Vector3.up);
        direction_rot = rot * direction;
        // Trigger level other edge
        Drawing.DrawVector(direction_rot, trigger, col, 2f);
        // Upper level other edge
        Drawing.DrawVector(direction_rot, trigger + Vector3.up * Height, col, 2f);
        // Vertical line on the other edge
        Handles.DrawLine(trigger + direction_rot, trigger + direction_rot + Vector3.up * Height, 2f);

        // Trigger level arc
        Handles.DrawWireArc(trigger, Vector3.up, direction_rot,
                            FOVDegrees, Radius, 2f);
        // Upper arc
        Handles.DrawWireArc(trigger+Vector3.up*Height, Vector3.up, direction_rot,
                     FOVDegrees, Radius, 2f);
        // Vertical line on the base
        Handles.DrawLine(trigger, trigger + Vector3.up * Height, 2f);


        //float dotp = Vector3.Dot(trigger_to_target.normalized, trigger_to_lookat.normalized);
        //Debug.Log("Dot product: " + dotp);
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
