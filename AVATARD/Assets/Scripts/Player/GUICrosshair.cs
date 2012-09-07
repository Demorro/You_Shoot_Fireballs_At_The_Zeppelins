using UnityEngine;
using System.Collections;

public class GUICrosshair : MonoBehaviour {
	
	public Texture2D crosshairTexture;
	
	private Rect position;

	// Use this for initialization
	void Start () 
	{
		position.x = (Screen.width / 2) - (crosshairTexture.width / 2);
		position.y = (Screen.height / 2) - (crosshairTexture.height /2);
		position.height = crosshairTexture.height;
		position.width = crosshairTexture.width;
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(position,crosshairTexture);
	}
}
