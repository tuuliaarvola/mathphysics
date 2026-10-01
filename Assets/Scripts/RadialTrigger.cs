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

    public bool Triggered { }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

