using UnityEngine;
using System.Collections;

public class TurretTrack : MonoBehaviour {
	
	public GameObject target;
	private Vector3 adjustedTarget;
	
	public bool isActive = true;
	
	public bool isHorizontalPart;
	public bool isVerticalPart;
	
	public float rotationSpeed;
	
	private Quaternion targetHorizontalRotation;
	private Quaternion targetVerticalRotation;
	
	private int debugCounter;
	
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	 {
	
		
		if(isActive == true)
		{	
			if(isHorizontalPart == true)
			{
				adjustedTarget = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
			
				targetHorizontalRotation = Quaternion.LookRotation(transform.position - adjustedTarget);
				
				//horizontal rotation
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetHorizontalRotation, rotationSpeed * Time.smoothDeltaTime);
			}
			
			/*if(isVerticalPart == true)
			{
				
				adjustedTarget = new Vector3(target.transform.position.x, transform.transform.position.y, target.transform.position.z);

				
				targetVerticalRotation = Quaternion.LookRotation(transform.position - adjustedTarget);
				
				//vertical rotation
				//transform.rotation = Quaternion.RotateTowards(transform.rotation, targetVerticalRotation, rotationSpeed * Time.smoothDeltaTime);
				transform.rotation = Quaternion.FromToRotation (new Vector3(transform.rotation.x,0,0),new Vector3(targetVerticalRotation.x,0,0));
				
				
				
			}
			*/
			if(isVerticalPart == true)
			{
				adjustedTarget = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
				
				// Update aiming
				// Find the global direction towards the target and convert it into local space
				//Vector3 aimDirInLocalSpace = transform.InverseTransformDirection(target.transform.position);
				
				// Find a rotation that points at that target
				Quaternion aimRot = Quaternion.LookRotation(transform.position - target.transform.position);
				
				// Smoothly rotate the turret towards the target rotation
				float newEulerX = TurnTowards(transform.localEulerAngles.x, aimRot.eulerAngles.x, rotationSpeed * Time.deltaTime);
				//float newEulerY = TurnTowards(transform.localEulerAngles.y, aimRot.eulerAngles.y, rotationSpeed * Time.deltaTime);
				//float newEulerZ = TurnTowards(transform.localEulerAngles.z, aimRot.eulerAngles.z, rotationSpeed * Time.deltaTime);
				transform.localEulerAngles = new Vector3(newEulerX, 0, 0);
				
				//Fix a bug where it gets stuck strait vertical for some reason
				
				if((transform.localEulerAngles.x == 270) || (transform.localEulerAngles.x == 90))
				{
					debugCounter++;
				}
				
				if(debugCounter > 5)
				{
					transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + 10,0,0);
					debugCounter = 0;
				}
				
			}
		}
	}
	
	public static float TurnTowards (float current, float target, float maxDegreesDelta) {
		float angleDifference = Mathf.Repeat (target - current + 180, 360) - 180;
		float turnAngle = maxDegreesDelta * Mathf.Sign (angleDifference);
		if (Mathf.Abs (turnAngle) > Mathf.Abs (angleDifference)) {
			return target;
		}
		return current + turnAngle;
	}
}
