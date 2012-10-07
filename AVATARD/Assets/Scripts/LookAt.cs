using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	
	public GameObject target;
	
	public bool limitX = false;
	public bool limitY = false;
	public bool limitZ = false;
	
	private bool isActive = true;
	
	public int topAngleRotationLimit = 45;
	
	public float rotationSpeed = 10;
	
	private Quaternion targetRotation;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		
		
		
		
		
		
		if(isActive == true)
		{
			//if we arnt going to hit anything
			targetRotation = Quaternion.LookRotation(transform.position - target.transform.position);
			
			if(limitX == true)
			{
				targetRotation.x = 0;
			}
			if(limitY == true)
			{
				targetRotation.y = 0;
			}
			if(limitZ == true)
			{
				targetRotation.z = 0;
			}
			
			//targetRotation *= Quaternion.Euler(0,180,0);		
			
			//THIS IF STATEMENT DISABLES THE LOOKAT IF WE ARE DIRECTLY ABOVE OR BELOW THE THING.
			if((Vector3.Angle(Vector3.up, target.transform.position - transform.position) > topAngleRotationLimit) && (Vector3.Angle(Vector3.up, target.transform.position - transform.position) < 180 - topAngleRotationLimit))
			{	
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.smoothDeltaTime);
			}
			
			Vector3 toPlayer = new Vector3(target.transform.position.x - transform.position.x, 0, target.transform.position.z - transform.position.z);
			
			//This bit here rotates 180 after the player goes up and over or vice versa, so it dosent look like the object is spinning around after the vertical lock is untriggered.
			if(Vector3.Angle(transform.forward, -toPlayer) > 90)
			{
				transform.Rotate(transform.up, 180);
			}
				
				
			//Debug.DrawRay(transform.position, -transform.forward * 5);
			//Debug.DrawLine(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
			
			
			//Debug.DrawRay(transform.position, toPlayer);
			
			//Debug.Log(Vector3.Angle(transform.forward, -toPlayer));
			
			
		}
	}
}
