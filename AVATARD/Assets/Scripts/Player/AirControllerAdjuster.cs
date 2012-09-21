using UnityEngine;
using System.Collections;

//This adjusts the air controller so you cant just pin yourself to flat colliders
public class AirControllerAdjuster : MonoBehaviour {
	
	public bool shouldAdjustAirControl = false;
	private Vector3 contactVector = new Vector3(0,0,0);
	
	public bool goForwardAllowed = false;
	public bool goBackAllowed = false;
	public bool goLeftAllowed = false;
	public bool goRightAllowed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate()
	{
		if(shouldAdjustAirControl == true)
		{
			//rigidbody.AddForce(-(contactVector) * GetComponent<PlayerController>().airAcceleration * (Time.fixedDeltaTime * 1f),ForceMode.VelocityChange);
			
			//disabling relevent air movement in playercontroller
			//left
			if(contactVector.x < 0)
			{
				goLeftAllowed = false;
			}
			else if(contactVector.x > 0)
			{
				goLeftAllowed = true;
			}
			//right
			if(contactVector.x > 0)
			{
				goRightAllowed = false;
			}
			else if(contactVector.x < 0)
			{
				goRightAllowed = true;
			}
			//back
			if(contactVector.z < 0)
			{
				goBackAllowed = false;
			}
			else if(contactVector.z > 0)
			{
				goBackAllowed = true;
			}
			//forward
			if(contactVector.z > 0)
			{
				goForwardAllowed = false;
			}
			else if(contactVector.z < 0)
			{
				goForwardAllowed = true;
			}
			
			//Debug.Log(goForwardAllowed);
		}
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "Collideable") //Very very temporary code
		{		
			ContactPoint contact = collision.contacts[0];
			
			//get the vector between the middle of the transform and the point of contact
			contactVector = contact.point - transform.position;
			
			//flatten the vector along the y axis(up) so this can be ignored
			contactVector.y = 0;
					
			
			//normallize the vector(may not actually be neccesary, but makes it neat
			contactVector.Normalize();
			
			shouldAdjustAirControl = true;
			
		}

	}
	
	void OnCollisionExit(Collision collision)
	{
		shouldAdjustAirControl = false;
	}
}
