using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousManager : MonoBehaviour
{
    public Texture2D regular;
	public Texture2D signal;
	public Texture2D walk;
	
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpotCursor = Vector2.zero;
	public Vector2 hotSpotWalk = Vector2.zero;
	
	public void setRegular() {
		Cursor.SetCursor(regular, hotSpotCursor, cursorMode);
	}
	
	public void setSignal() {
		Cursor.SetCursor(signal, hotSpotCursor, cursorMode);
	}
	
	public void setWalk() {
		Cursor.SetCursor(walk, hotSpotWalk, cursorMode);
	}
}
