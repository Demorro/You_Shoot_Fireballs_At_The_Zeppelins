using UnityEngine;
using System.Collections;

public class FirePowerBar: MonoBehaviour {
	
	private Rect barRectangle;
	public Texture2D barTexture;
	
	public float xOffset = 0;
	public float yOffset = 0;
	//public float width = 0;
	public float height = 0;
	
	public float lengthMultiplyer = 1; //a multiplication factor of 2 will mean the healthbar is twice as long
	
	public float firePower = 100;
	
	public bool fromTop = true;
	public bool fromLeft = true;
	
	// Use this for initialization
	void Start ()
	{
		if((fromTop == true) && (fromLeft == true))
		{
			barRectangle = new Rect(xOffset,yOffset,firePower * lengthMultiplyer,height);
		}
		if((fromTop == false) && (fromLeft == true))
		{
			barRectangle = new Rect(xOffset,Screen.height - yOffset -height,(firePower * lengthMultiplyer),height);
		}
		if((fromTop == true) && (fromLeft == false))
		{
			barRectangle = new Rect(Screen.width - (firePower * lengthMultiplyer) - xOffset,yOffset,firePower * lengthMultiplyer,height);
		}
		if((fromTop == false) && (fromLeft == false))
		{
			barRectangle = new Rect(Screen.width - (firePower * lengthMultiplyer) - xOffset,Screen.height - yOffset -height,firePower * lengthMultiplyer,height);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		firePower = GetComponent<PlayerController>().firePower;
			
		barRectangle.width = firePower * lengthMultiplyer;
		
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


/*
var myRect : Rect;

var myText : Texture;

 

function Start()

{

  myRect = Rect(*insert starting rect variable here*);

}

 

function Update()

{

  // Example way to change bar length

  myRect.width -= Time.deltaTime;

  if (myRect.width < 0) {

    myRect.width = 0;

  }

}

 

function OnGUI()

{

  GUI.DrawTexture(myRect, myText);

}
*/