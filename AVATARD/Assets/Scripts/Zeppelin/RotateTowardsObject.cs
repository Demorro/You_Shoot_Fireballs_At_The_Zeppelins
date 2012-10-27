using UnityEngine;
using System.Collections;

public class RotateTowardsObject : MonoBehaviour {
	
	public GameObject target;
	public GameObject permenantTarget;
	public GameObject alternateTarget;

	
	private Vector3 newLocation;
	
	public float rotationSpeed = 10;
	public float randomRotationSpeedFactor = 5;
	
	private float actualRotationSpeed;
	private float actualRotationSpeedToUseAfterModifications;
	public bool isActive = true;
	
	//to do with collision
	
	//
	private Quaternion targetRotation;
	
	// Use this for initialization
	void Start () {
		
		actualRotationSpeed = rotationSpeed + Random.Range(-randomRotationSpeedFactor,randomRotationSpeedFactor);
		actualRotationSpeedToUseAfterModifications = actualRotationSpeed;
		
		permenantTarget = target;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(isActive == true)
		{
			//if we arnt going to hit anything
			targetRotation = Quaternion.LookRotation(transform.position - target.transform.position);
			
			targetRotation.x = 0;
			//targetRotation.y = 0;
			targetRotation.z = 0;
			
			targetRotation *= Quaternion.Euler(0,180,0);		
			
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, actualRotationSpeedToUseAfterModifications * Time.smoothDeltaTime);;
		}
		
	}
	
}
