using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTexture;
    public float cursorScale = 1f; // Scale factor for the cursor

    private void Start()
    {
        // Scale the cursor texture
        Texture2D scaledCursorTexture = ScaleTexture(cursorTexture, cursorScale);
        Vector2 hotSpot = new Vector2(scaledCursorTexture.width / 2, scaledCursorTexture.height / 2); // Adjust hotspot as needed
        Cursor.SetCursor(scaledCursorTexture, hotSpot, CursorMode.Auto);
    }

    private Texture2D ScaleTexture(Texture2D source, float scale)
    {
        int width = Mathf.RoundToInt(source.width * scale);
        int height = Mathf.RoundToInt(source.height * scale);

        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(source, rt);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D result = new Texture2D(width, height);
        result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        result.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        return result;
    }
}
