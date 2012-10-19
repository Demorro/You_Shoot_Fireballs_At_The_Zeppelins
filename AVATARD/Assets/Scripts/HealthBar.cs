using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	//This all just works, in order to change the health at runtime just call ChangeHealth()
	
	
	public float startHealth;
	private float maxHealth;
	public float currentHealth;
	
	//to do with scaling, both in the x dimension and to do with camera distance
	private Vector3 startScale;
	private Vector3 tempScale;
	public float scalingFactor = 1; //This is the number that controls how drastically the thing gets scaled, 2 is the reccommended value
	public float closestScaleDistance; //The closest distance that the thing will start to scale
	public float furthestScaleDistance;//The furthest distance that the thing will scale from
	private float currentScalingFactor; //This stores the factor of how much to scale by, as worked out by camera distance and closest and furthest scale distance
	private float multiplicationFactor; //this stores how much to multiply in order to stretch the sequence of numbers from something to something to 1-something, as in 2-4 becomes 1-4 at the same ratio
	private float cameraDistance;
	
	//changing the x value
	private float xScaler;
	private float xChangeHolder;
	public float healthChangeSpeed;
	private bool healthHasChanged;
	
	//To do with the fade in/out effect
	Color theColor;
	public float fadeDistance = 15;
	public float fadeSpeed = 1;
	
	// Use this for initialization
	void Start () 
	{
		startHealth = transform.parent.GetComponent<ZeppelinController>().startHealth;
		currentHealth = startHealth;
		maxHealth = startHealth;
		
		startScale = transform.localScale;
		tempScale = startScale;
		xScaler = startScale.x;
		
		theColor = gameObject.renderer.material.color;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//store the camera distance in the cameraDistance Variable
		cameraDistance = Vector3.Distance(Camera.mainCamera.transform.position, transform.position);
		
		if((cameraDistance > closestScaleDistance) && (cameraDistance < furthestScaleDistance))
		{
			//Calculate the currentScalingFactor as a float of 1-0 between the closest and furthest scale distance, taking into account player pos
			currentScalingFactor = 1/((furthestScaleDistance - closestScaleDistance)/(furthestScaleDistance - cameraDistance));
			
			//invert that number so it is from 0-1, and also add 1(have to -1 and take abs due to the way it works)
			currentScalingFactor = Mathf.Abs(currentScalingFactor - 1);
			
			
			//add 1 so that it is between 1-2
			currentScalingFactor++;
			//currentScalingFactor++;
			
			//multiply currentScalingFactor by scalingFactor in the case that you want the healthbars to get bigger
			currentScalingFactor = currentScalingFactor * scalingFactor;
			
			//then stretch whatever the range is so it starts at 1, as in 2-4 will become 1-4, so it works.
			//currentscalingfactor = currentscalingfactor *  1 - (max - currentPos / max - min) * (1-(1 / min));
			//I worked this algorithm out myself, it works and makes sense, at least at the time, but god knows what sense that it? Don't touch it, it works.
			//Scalingfactor is min, scalingfactor * 2 is max
			multiplicationFactor = 1 - (((scalingFactor * 2) - (currentScalingFactor)) / ((scalingFactor * 2) - (scalingFactor))) * (1 - (1 / scalingFactor));
			currentScalingFactor = currentScalingFactor * multiplicationFactor;
			
			//then assign this scaling factor to the scale via tempscale
			tempScale = startScale * (currentScalingFactor);
			transform.localScale = tempScale;	
			xScaler = tempScale.x;

		}
		else if(cameraDistance < closestScaleDistance)	//if we are way close to the heathBar
		{
			tempScale = startScale;
			xScaler = tempScale.x;
		}
		else if(cameraDistance > furthestScaleDistance) //if we are way far from the healthBar
		{
			tempScale = startScale * (scalingFactor * 2);
			xScaler = tempScale.x;
		}
		//make sure the healthbar dosent overflow off the bottom
		if(currentHealth < 0)
		{
			xScaler = 0;
		}
		//do the x scale distance
		tempScale.x = xScaler * (1/(maxHealth/currentHealth));
		transform.localScale = tempScale;
		
		//change the health nicely
		AdjustHealth();
		
		//Deals with fading
		if((Vector3.Distance(Camera.mainCamera.transform.position, transform.position) < fadeDistance) && (theColor.a > 0))
		{
			theColor.a = theColor.a - 0.005f * fadeSpeed;
		}
		if((Vector3.Distance(Camera.mainCamera.transform.position, transform.position) > fadeDistance) && (theColor.a < 1))
		{
			theColor.a = theColor.a + 0.005f * fadeSpeed;
		}
		
		transform.renderer.material.color = theColor;
	}
	
	public void ChangeHealth(float healthChange)
	{
		xChangeHolder += healthChange;
	}
	
	private void AdjustHealth()
	{
		if(currentHealth < currentHealth + xChangeHolder)
		{
			currentHealth = currentHealth + (healthChangeSpeed);
			xChangeHolder = currentHealth - (healthChangeSpeed);
		}
		else if(currentHealth > currentHealth + xChangeHolder)
		{
			currentHealth = currentHealth - (healthChangeSpeed);
			xChangeHolder = xChangeHolder + (healthChangeSpeed);
		}

		currentHealth = Mathf.Round(currentHealth);
		xChangeHolder = Mathf.Round(xChangeHolder);
	}
}
