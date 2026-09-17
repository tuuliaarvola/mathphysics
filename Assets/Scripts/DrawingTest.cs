using UnityEditor;
using UnityEngine;

public class DrawingTest : MonoBehaviour
{

    public GameObject VectorTo;

    [Header("Screen Attributes")]
    [Range(640, 1920)]
    public int ScreenWidth = 1920;
    [Range(480, 1080)]
    public int ScreenHeight = 1080;

    [Range(0, 1920)]
    public int ScreenPositionX = 0;
    [Range(0, 1080)]
    public int ScreenPositionY = 0;

    [Header("Popup Attributes")]
    [Range(0, 100)]
    public int PopupPercentageWidth = 75;
    [Range(0, 100)]
    public int PopupPercentageHeight = 75;

    [Header("Healthbar Attributes")]
    [Range(-50, 50)]
    public int HealthBarOffsetX = 10;
    [Range(-50, 50)]
    public int HealthBarOffsetY = 4;

    [Range(0, 50)]
    public int HealthBarWidth = 12;
    [Range(0, 50)]
    public int HealthBarHeight = 2;

    private void OnDrawGizmos()
    {
        Vector3 v = VectorTo.transform.position;
        // Call the static method from Drawing-class
        Drawing.DrawVector(v, Vector3.zero, Color.magenta, 3f);
        // Call the static method for drawing the rectangle
        Vector2 screenpos = new Vector2(ScreenPositionX, ScreenPositionY);
        Vector2 screensize = new Vector2(ScreenWidth, ScreenHeight);
        Drawing.DrawXYRectangle(screenpos,
                                screensize,
                                Color.white, 3f);

        // Popup
        Vector2 popupsize = new Vector2(ScreenWidth * PopupPercentageWidth / 100,
                                        ScreenHeight * PopupPercentageHeight / 100);

        Vector2 popupoffset = new Vector2(ScreenWidth * (100 - PopupPercentageWidth) / 200,
                                          ScreenHeight * (100 - PopupPercentageHeight) / 200);

        Drawing.DrawXYRectangle(screenpos + popupoffset, popupsize, Color.white, 3f);

        // Healthbar
        Vector2 hb_offset = new Vector2(ScreenWidth * HealthBarOffsetX / 100,
                                        ScreenHeight * HealthBarOffsetY / 100);
        Vector2 hb_size = new Vector2(ScreenWidth * HealthBarWidth / 100,
                                        ScreenHeight * HealthBarHeight / 100);

        Drawing.DrawXYRectangle(screenpos + screensize - hb_offset, hb_size, Color.darkGreen, 3f);


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

