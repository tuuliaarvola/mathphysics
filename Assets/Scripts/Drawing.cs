using UnityEditor;
using UnityEngine;

// Helper class for drawing stuff
public class Drawing
{
    public static void DrawVector(Vector3 vec, Vector3 pos, Color col, float th)
    {
        Color orig = Handles.color;  // Backup the original Handles.color
        Handles.color = col;
        // Start from pos, end at pos+vec
        Handles.DrawLine(pos, pos + vec, th);
        // Draw the cone at the end
        float h_size = HandleUtility.GetHandleSize(pos + vec); // get a scaler for the handle

        Handles.ConeHandleCap(0, pos + vec - vec.normalized * 0.23f * h_size,
            Quaternion.LookRotation(vec), 0.35f * h_size, EventType.Repaint);
        Handles.color = orig; // Restore original color
    }


    public static void DrawXYRectangle(Vector2 pos, Vector2 size, Color col, float th)
    {

        Color orig = Handles.color;  // Backup the original Handles.color
        Handles.color = col;
        // Vertical line (#1)        x  y  z               x    y   z
        Handles.DrawLine(new Vector3(pos.x, pos.y, 0), new Vector3(pos.x, pos.y + size.y, 0), th);
        // Horizontal line (#1)
        Handles.DrawLine(new Vector3(pos.x, pos.y, 0), new Vector3(pos.x + size.x, pos.y, 0), th);
        // Vertical line (#2)
        Handles.DrawLine(new Vector3(pos.x + size.x, pos.y, 0), new Vector3(pos.x + size.x, pos.y + size.y, 0), th);
        // Horizontal line (#2)
        Handles.DrawLine(new Vector3(pos.x, pos.y + size.y, 0), new Vector3(pos.x + size.x, pos.y + size.y, 0), th);
        Handles.color = orig; // Restore original color
    }

}
