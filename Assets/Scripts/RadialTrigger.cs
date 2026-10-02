using UnityEditor;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{

    [Range(0.1f, 20f)]
    public float Radius = 5f;

    public GameObject Target;

    public bool Triggered = false;

    private bool IsTriggered()
    {
        // Local vector variables
        Vector3 trigger = transform.position;
        trigger.y = 0f;  // FIX from last week
        Vector3 target = Target.transform.position;
        target.y = 0f;   // FIX from last week
        Vector3 trigger_to_target = target - trigger;  // vector math!!!

        return trigger_to_target.magnitude <= Radius;
    }

    private void OnDrawGizmos()
    {
        // Are we triggered???
        Triggered = IsTriggered();
        if (Triggered)
            Handles.color = Color.red;
        else 
            Handles.color = Color.green;

        Handles.DrawWireDisc(transform.position, Vector3.up, Radius);

        // Local vector variables
        Vector3 trigger = transform.position;
        Vector3 target = Target.transform.position;
        Vector3 trigger_to_target = target - trigger;  // vector math!!!

        // Draw vectors from origin to trigger & target
        Drawing.DrawVector(trigger, Vector3.zero, Color.paleGreen, 2f);
        Drawing.DrawVector(target, Vector3.zero, Color.paleGreen, 2f);

        // Draw vector from trigger to target
        Drawing.DrawVector(trigger_to_target, trigger, Color.darkMagenta, 2f);

        // Draw the "projected" vector
        trigger_to_target.y = 0f;
        Drawing.DrawVector(trigger_to_target, trigger, Color.blue, 2f);

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
