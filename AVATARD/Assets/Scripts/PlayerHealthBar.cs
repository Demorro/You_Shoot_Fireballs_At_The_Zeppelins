using UnityEngine;
using System.Collections;

public class PlayerHealthBar: MonoBehaviour {
	
	private Rect barRectangle;
	public Texture2D barTexture;
	
	public float xOffset = 0;
	public float yOffset = 0;
	//public float width = 0;
	public float height = 0;
	
	public float lengthMultiplyer = 1; //a multiplication factor of 2 will mean the healthbar is twice as long
	
	public float Health = 100;
	
	public bool fromTop = true;
	public bool fromLeft = true;
	
	// Use this for initialization
	void Start ()
	{
		if((fromTop == true) && (fromLeft == true))
		{
			barRectangle = new Rect(xOffset,yOffset,Health * lengthMultiplyer,height);
		}
		if((fromTop == false) && (fromLeft == true))
		{
			barRectangle = new Rect(xOffset,Screen.height - yOffset -height,(Health * lengthMultiplyer),height);
		}
		if((fromTop == true) && (fromLeft == false))
		{
			barRectangle = new Rect(Screen.width - (Health * lengthMultiplyer) - xOffset,yOffset,Health * lengthMultiplyer,height);
		}
		if((fromTop == false) && (fromLeft == false))
		{
			barRectangle = new Rect(Screen.width - (Health * lengthMultiplyer) - xOffset,Screen.height - yOffset -height,Health * lengthMultiplyer,height);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Health = GetComponent<PlayerController>().Health;
			
		barRectangle.width = Health * lengthMultiplyer;
		
		if(barRectangle.width < 0)
		{
			barRectangle.width = 0;
		}
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(barRectangle,barTexture);
	}
	
}