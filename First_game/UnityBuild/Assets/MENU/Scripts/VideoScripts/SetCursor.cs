using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetCursor : MonoBehaviour
{
    public Texture2D mouseCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector3 hotSpot = Vector3.zero;

    private void Start()
    {
        Cursor.SetCursor(mouseCursor, hotSpot, cursorMode);
    }
}