using UnityEngine;
using System.Collections;

public class ZeppelinController : MonoBehaviour {
	
	//public GameObject target;
	public GameObject zeppelinModel;
	public GameObject zeppelinPlatform;
	public GameObject zeppelinCrashSound;
	
	//Spawner Script
	public SpawnZeppelins spawnZeppelins;
	
	public float verticalHeight = 50; //This means that the object will be orbiting 10(units?) above the player.
	public float randomVerticalHeightFactor = 50;
	public float speed = 5; //The speed of the object.
	public float randomSpeedFactor = 2;
	public float upwardsSpeed = 5;
	public float randomUpwardsSpeedFactor = 5;
	
	//collision explosion
	public GameObject zeppelinCollisionExplosion;
	public GameObject zeppelinCollisionExplosionSound;
	
	//final explosion
	public GameObject deathExplosion;
	public GameObject deathExplosionSound;
	
	//Once randomness is applied, the actual variables for the script object
	private float actualUpwardsSpeed;
	public float actualSpeed;
	private float actualVerticalHeight;
	
	//if is true then zeppelin is going down
	private bool goingDown = false;
	private bool goingForward = true;
	
	private Quaternion targetRotation;
	

	// Use this for initialization
	void Start () 
	{
		actualSpeed = speed + Random.Range(-randomSpeedFactor,randomSpeedFactor);
		actualUpwardsSpeed = upwardsSpeed + Random.Range(-randomUpwardsSpeedFactor, randomUpwardsSpeedFactor);
		actualVerticalHeight = verticalHeight + Random.Range(-randomVerticalHeightFactor, randomVerticalHeightFactor);
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
		
		
	}
	
	void FixedUpdate()
	{
		Vector3 newPosition;
		newPosition = transform.position;
		
		if(goingDown == false)
		{	
			if(transform.position.y < actualVerticalHeight)
			{
				newPosition.y = transform.position.y + actualUpwardsSpeed * Time.fixedDeltaTime;
			}
			
		}
		else if (goingDown == true)
		{
			rigidbody.isKinematic = false;
		}
		
		if(goingForward == true)
		{
			newPosition = newPosition + ((transform.forward * actualSpeed) * Time.fixedDeltaTime);
		}
			
		transform.position = newPosition;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts[0];
		
		if((collision.transform.tag == "Projectile") || (collision.transform.tag == "Zeppelin") || (collision.transform.tag == "Collideable"))
		{
			//initiate crashing
			goingDown = true;
			gameObject.layer = 0;
			rigidbody.useGravity = true;
			GetComponent<RotateTowardsObject>().isActive = false;
			Destroy(zeppelinPlatform);
			zeppelinModel.transform.DetachChildren();
			zeppelinModel.animation.Play();
			//if we hit anything but a fireball, we should stop trying to go forward, as the thing is in the way
			if((collision.transform.tag == "Zeppelin") || (collision.transform.tag == "Collideable"))
			{
				goingForward = false;
			}
		}
		
		//if we hit into another Zeppelin or something, play the crash sounnd
		if((collision.transform.tag == "Zeppelin") || (collision.transform.tag == "Collideable"))
		{
			Instantiate(zeppelinCrashSound,transform.position,transform.rotation);
			Instantiate(zeppelinCollisionExplosion,contact.point,transform.rotation);
			Instantiate(zeppelinCollisionExplosionSound,contact.point,transform.rotation);
		}
		
		//Explode on the ground
		if(collision.transform.tag == "Ground")
		{
			Instantiate(deathExplosion,contact.point,transform.rotation);
			Instantiate(deathExplosionSound,transform.position,transform.rotation);
			
			Destroy(zeppelinModel.gameObject);
			Destroy(gameObject);
			spawnZeppelins.allZeppelins.Remove(this.gameObject);
		}
		
	}
	
	
	void OnCollisionExit(Collision collision)
	{

	}
	
	void ToggleTarget()
	{
		if(GetComponent<RotateTowardsObject>().target == GetComponent<RotateTowardsObject>().permenantTarget)
		{
			GetComponent<RotateTowardsObject>().target = GetComponent<RotateTowardsObject>().alternateTarget;
		}
		else if(GetComponent<RotateTowardsObject>().target == GetComponent<RotateTowardsObject>().alternateTarget)
		{
			GetComponent<RotateTowardsObject>().target = GetComponent<RotateTowardsObject>().permenantTarget;
		}
	}
	
}
