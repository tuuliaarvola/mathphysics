using UnityEngine;

public class EasingTest : MonoBehaviour
{


    public GameObject[] MyGameObjects;

    public EasingFunction.Ease ease; // public "easetype" which the user can select
    private EasingFunction.Function easeFunction; // delegate function variable

    public Color[] MyColorsStart;
    public Color[] MyColorsEnd;

    [Range(1f, 10f)]
    public float EasingTime = 5f;
    [Range(0f, 5f)]
    public float StartTime = 2f;

    public float MoveAmount = 5f;

    public bool Loop = true;

    private Vector3[] OriginalPositions;



     private float Ease(float t, int index)
    {

        if (index == 0)
            return Mathf.Clamp01(t); //Linear
        else if (index == 1)
            return Mathf.Clamp01(t * t); // Ease-in
        else if (index == 2)
        {
            return Mathf.Clamp01(1f - (1f - t) * (1f - t)); // Ease-out
        }
        else if (index == 3)
        {
            if (t < 0.5f)
                return Mathf.Clamp01(2f * t * t);
            else
            {
                return Mathf.Clamp01(1f - (2f - 2f * t) * (2f - 2f * t) / 2f);
            }
        }

        return 0.0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //Get the correct easing function
        easeFunction = EasingFunction.GetEasingFunction(ease);

        OriginalPositions = new Vector3[MyGameObjects.Length];
        for (int i = 0; i < MyGameObjects.Length; i++)
        {
            OriginalPositions[i] = MyGameObjects[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < StartTime)
            return;
        else
        {
            float t = 0.0f;
            if (!Loop)
                t = Mathf.Clamp01((Time.time - StartTime) / EasingTime);
            else
                t = Mathf.Clamp01(((Time.time - StartTime) % EasingTime) / EasingTime);

            for (int i = 0; i < MyGameObjects.Length; i++)
            {
                // Position
                MyGameObjects[i].transform.position =
                    OriginalPositions[i] + MoveAmount * easeFunction(0f, 1f, t) * Vector3.right;
                // Material color
                MyGameObjects[i].GetComponent<MeshRenderer>().material.color = Color.Lerp(MyColorsStart[i], MyColorsEnd[i], easeFunction(0f, 1f, t));

            }

        }

    }
}

