using UnityEngine;
using System.Collections;

public class ShootMissiles : MonoBehaviour {
	
	public GameObject missile;
	public GameObject ignitionSound;
	public GameObject playerRigidBody;
	
	public int shootCost = 10;
	
	private GameObject missileReference;
	private GameObject ignitionSoundReference;
	
	public float forwardOffset = 1;
	
	
	// Use this for initialization
	void Start () 
	{
		//this only works if the players actual rigidbody is on the parent. This script was designed to be put on the camera of a first person controller
		playerRigidBody = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(transform.parent.GetComponent<PlayerController>().firePower > shootCost)
			{
				
				missileReference = Instantiate(missile,(transform.position + transform.forward * forwardOffset), transform.rotation) as GameObject;
				missileReference.rigidbody.velocity = missileReference.rigidbody.velocity + transform.parent.GetComponent<PlayerController>().absoluteVelocity; //the absoluteVelocity bit adds inheritence

				
				ignitionSoundReference = Instantiate(ignitionSound,transform.position,transform.rotation) as GameObject;
				ignitionSoundReference.gameObject.transform.parent = gameObject.transform;
				
				transform.parent.GetComponent<PlayerController>().firePower -= shootCost;
				transform.parent.GetComponent<PlayerController>().rechargeTimer = 0;
				
			}
		}
	
	}
	
	
}