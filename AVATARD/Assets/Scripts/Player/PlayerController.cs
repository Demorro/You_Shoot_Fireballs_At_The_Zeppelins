using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float jetStrength = 100;
	public float acceleration = 100;
	private static float storedDecceleration;
	public float deceleration = 100;
	public float topSpeed = 100;
	private float currentTopSpeed;
	
	public float topAirSpeed = 200;
	public float airAcceleration = 150;
	public float permenantAirAcceleration;
	private float currentTopAirSpeed;
	
	public float Health = 100;
	
	public float jumpStrength = 100;
	
	public float firePower = 100;
	private float maxFirePower;
	public float jetDecrementFactor = 10;
	public float firePowerRechargeFactor = 10;
	
	public float rechargeWaitTime = 3.0f;
	public float rechargeTimer = 0.0f;
	
	private bool isGrounded = false;
	
	
	//to do with absolute velocity and finding the velocity of a non rigidbody
	private Vector3 lastPosition;
	public Vector3 absoluteVelocity;
	
	private Vector3 parentLastPosition;
	public Vector3 parentAbsoluteVelocity;
	private bool trackParentVelocity = false;
	private bool applyExitVelocity = false;
	
	public bool onAZeppelin = false;
	
	//falling stuff
	public float maxSafeFallSpeed = 15;
	public float fallDamageMultiplyer = 1;
	private float currentFallSpeed;
	
	public GameObject flyingJet;
	Component[] jetEmitters;
	
	public bool debugFlying = false;
	
	// Use this for initialization
	void Start () 
	{
		maxFirePower = firePower;
		
		jetStrength *= 10;
		acceleration *= 10;
		deceleration *= 10;
		storedDecceleration = deceleration;
		topSpeed *= 10;
		topAirSpeed *= 10;
	 	airAcceleration *= 10;
		permenantAirAcceleration = airAcceleration;
		
		
		
		jetEmitters = flyingJet.GetComponentsInChildren<ParticleEmitter>();
		foreach(ParticleEmitter emitter in jetEmitters)
		{
			emitter.emit = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{	
		
		if (debugFlying == true)
		{
			firePower = 100;
		}
		
		//forward
		if((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow)))
		{
			//transform.InverseTransformDirection(rigidbody.velocity).z is the local z velocity
			if((transform.InverseTransformDirection(rigidbody.velocity).z < currentTopSpeed * Time.deltaTime) && (isGrounded == true))
			{
				rigidbody.AddForce(transform.forward * acceleration * Time.deltaTime,ForceMode.Acceleration);
			}
			else if((isGrounded == false) && (transform.InverseTransformDirection(rigidbody.velocity).z < currentTopAirSpeed * Time.deltaTime))
			{
				rigidbody.AddForce(transform.forward * airAcceleration * Time.deltaTime,ForceMode.Acceleration);
			}
		}
		else
		{
			if((transform.InverseTransformDirection(rigidbody.velocity).z > 1) && (isGrounded == true))
			{
				rigidbody.AddForce(-transform.forward * deceleration * Time.deltaTime,ForceMode.Acceleration);
			}
		}
		//back
		if((Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.DownArrow)))
		{
			//transform.InverseTransformDirection(rigidbody.velocity).z is the local z velocity
			if((transform.InverseTransformDirection(rigidbody.velocity).z > -currentTopSpeed * Time.deltaTime) && (isGrounded == true))
			{
				rigidbody.AddForce(-transform.forward * acceleration * Time.deltaTime,ForceMode.Acceleration);
			}
			else if((isGrounded == false) && (transform.InverseTransformDirection(rigidbody.velocity).z > -currentTopAirSpeed * Time.deltaTime))
			{
				rigidbody.AddForce(-transform.forward * airAcceleration * Time.deltaTime,ForceMode.Acceleration);	
			}
		}
		else
		{
			if((transform.InverseTransformDirection(rigidbody.velocity).z < -1) && (isGrounded == true))
			{
				rigidbody.AddForce(transform.forward * deceleration * Time.deltaTime,ForceMode.Acceleration);
			}
		}
		//left
		if((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow)))
		{
			//transform.InverseTransformDirection(rigidbody.velocity).x is the local x velocity
			if((transform.InverseTransformDirection(rigidbody.velocity).x > -currentTopSpeed * Time.deltaTime) && (isGrounded == true))
			{
				rigidbody.AddForce(-transform.right * acceleration * Time.deltaTime,ForceMode.Acceleration);
			}
			else if((isGrounded == false) && (transform.InverseTransformDirection(rigidbody.velocity).x > -currentTopAirSpeed * Time.deltaTime))
			{
				rigidbody.AddForce(-transform.right * airAcceleration * Time.deltaTime,ForceMode.Acceleration);
			}
		}
		else
		{
			if((transform.InverseTransformDirection(rigidbody.velocity).x < -1) && (isGrounded == true))
			{
				rigidbody.AddForce(transform.right * deceleration * Time.deltaTime,ForceMode.Acceleration);
			}
		}
		//right
		if((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)))
		{
			//transform.InverseTransformDirection(rigidbody.velocity).x is the local x velocity
			if((transform.InverseTransformDirection(rigidbody.velocity).x < currentTopSpeed * Time.deltaTime) && (isGrounded == true))
			{
				rigidbody.AddForce(transform.right * acceleration * Time.deltaTime,ForceMode.Acceleration);
			}
			else if((isGrounded == false) && (transform.InverseTransformDirection(rigidbody.velocity).x < currentTopAirSpeed * Time.deltaTime))
			{
				rigidbody.AddForce(transform.right * airAcceleration * Time.deltaTime,ForceMode.Acceleration);
			}
		}
		else
		{
			if((transform.InverseTransformDirection(rigidbody.velocity).x > 1) && (isGrounded == true))
			{
				rigidbody.AddForce(-transform.right * deceleration * Time.deltaTime,ForceMode.Acceleration);
			}
		}
		
		//Calculate diagonal speed
		//upright
		if(((Input.GetKey(KeyCode.W)) || (Input.GetKeyDown(KeyCode.UpArrow))) && ((Input.GetKey(KeyCode.D)) || (Input.GetKeyDown(KeyCode.RightArrow))))
		{
			//use trigonomotry to get the speeds needed for equal diagonal movement. The bit inside the sin brackets is radian conversion
			currentTopSpeed = topSpeed * Mathf.Sin(45 * (Mathf.PI / 180));
			currentTopAirSpeed = topAirSpeed * Mathf.Sin(45 * (Mathf.PI / 180));
		}
		//Calculate diagonal speed
		//upleft
		else if(((Input.GetKey(KeyCode.W)) || (Input.GetKeyDown(KeyCode.UpArrow))) && ((Input.GetKey(KeyCode.A)) || (Input.GetKeyDown(KeyCode.LeftArrow))))
		{
			//use trigonomotry to get the speeds needed for equal diagonal movement. The bit inside the sin brackets is radian conversion
			currentTopSpeed = topSpeed * Mathf.Sin(45 * (Mathf.PI / 180));
			currentTopAirSpeed = topAirSpeed * Mathf.Sin(45 * (Mathf.PI / 180));
		}
		//Calculate diagonal speed
		//downright
		else if(((Input.GetKey(KeyCode.S)) || (Input.GetKeyDown(KeyCode.DownArrow))) && ((Input.GetKey(KeyCode.D)) || (Input.GetKeyDown(KeyCode.RightArrow))))
		{
			//use trigonomotry to get the speeds needed for equal diagonal movement. The bit inside the sin brackets is radian conversion
			currentTopSpeed = topSpeed * Mathf.Sin(45 * (Mathf.PI / 180));
			currentTopAirSpeed = topAirSpeed * Mathf.Sin(45 * (Mathf.PI / 180));
		}
		//Calculate diagonal speed
		//downleft
		else if(((Input.GetKey(KeyCode.S)) || (Input.GetKeyDown(KeyCode.DownArrow))) && ((Input.GetKey(KeyCode.A)) || (Input.GetKeyDown(KeyCode.LeftArrow))))
		{
			//use trigonomotry to get the speeds needed for equal diagonal movement. The bit inside the sin brackets is radian conversion
			currentTopSpeed = topSpeed * Mathf.Sin(45 * (Mathf.PI / 180));
			currentTopAirSpeed = topAirSpeed * Mathf.Sin(45 * (Mathf.PI / 180));
		}
		else
		{
			currentTopSpeed = topSpeed;
			currentTopAirSpeed = topAirSpeed;
		}
		//Jet
		if(Input.GetKey(KeyCode.Mouse1))
		{
			if(firePower > jetDecrementFactor * Time.deltaTime)
			{
				rigidbody.AddForce(Vector3.up * jetStrength * Time.deltaTime,ForceMode.Force);
				firePower = firePower - (jetDecrementFactor * Time.deltaTime)/2;
				rechargeTimer = 0;
				airAcceleration = permenantAirAcceleration * 2.5f;
				
				foreach(ParticleEmitter emitter in jetEmitters)
				{
					emitter.emit = true;
				}
				
			}
			else
			{
				airAcceleration = permenantAirAcceleration;
				
				foreach(ParticleEmitter emitter in jetEmitters)
				{
					emitter.emit = false;
				}
			}
		}
		else
		{
			airAcceleration = permenantAirAcceleration;
			
			foreach(ParticleEmitter emitter in jetEmitters)
			{
				emitter.emit = false;
			}
		}
		//jetsound
		if((Input.GetKeyDown(KeyCode.Mouse1)) && (firePower >= jetDecrementFactor * Time.deltaTime))
		{
			SendMessage("PlayJetSound");
		}
		else if((Input.GetKeyUp(KeyCode.Mouse1)) || (firePower < jetDecrementFactor * Time.deltaTime))
		{
			SendMessage("StopJetSound");
			//audio.PlayOneShot(jetPackFadeOut);	
		}
		
		//If we arnt jetting or shooting fireballs, and the recharge wait has expired, recharge.
		if(rechargeTimer > rechargeWaitTime)
		{
			if((!Input.GetKey(KeyCode.Mouse1))  && (!Input.GetKeyDown(KeyCode.Mouse0)))
			{
				if(firePower < maxFirePower)
				{
					firePower = firePower + (firePowerRechargeFactor * Time.deltaTime);
				}
				if (firePower > maxFirePower)
				{
					firePower = maxFirePower;
				}
			}
		}
		
		//increment recharge timer
		rechargeTimer += Time.deltaTime;
			
	}
	
	void FixedUpdate()
	{
		//To do with being the right side up after shooting down a zeppelin we are standing on, not strictly neccesary.
		Vector3 beRightSideUp;
		beRightSideUp.x = 0;
		beRightSideUp.y = transform.rotation.eulerAngles.y;
		beRightSideUp.z = 0;
		
		//This handy bit of code stores the velocity of a non rigidbody as a vector 3. Here it is used to add inheritence to the bullets, among other important things
		absoluteVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
		lastPosition = transform.position;
		
		//The whole deal with tracking the parents velocity is so that when you jump off a zeppelin, you inherit its velocity so it behaves more realistically
		//works just the same way as above does to add inheritence to bullets, just only tracking when there is a parent(zeppelin) as defined by a toggle
		if(trackParentVelocity == true)
		{
			if(transform.parent != null)
			{
				if(parentLastPosition != Vector3.zero) //This appears to fix the problem of the player zooming off into the distance when touching/leaving zeppelins. More testing is required
				{									   //However if this does work, it is logical as the parents transform - 0 / something like 0.001 would be very high.
					parentAbsoluteVelocity = (transform.parent.transform.position - parentLastPosition) / Time.deltaTime;
				}
				parentLastPosition = transform.parent.transform.position;
			}
			else if (transform.parent == null)
			{
				//should probably only happen when the parent is destroyed ... maybe.
				trackParentVelocity = false;
				onAZeppelin = false;
				parentAbsoluteVelocity = Vector3.zero;
				parentLastPosition = Vector3.zero;
				
				//set the rotation so we are facing the right way, not strictly neccesary anymore, but nice to have in case im not the perfect programmer(which i obviously am)
				transform.eulerAngles = beRightSideUp;
			}
		}
		if(applyExitVelocity == true)
		{
			//this bit applys the inheritence velocity from the zeppelin parents if the toggle is true, put here to deal with physics bugs, hope it works. The toggle is in OnCollisionExit
			applyExitVelocity = false;
			rigidbody.velocity = rigidbody.velocity + parentAbsoluteVelocity;
			parentAbsoluteVelocity = Vector3.zero;
			parentLastPosition = Vector3.zero;
		}
		
		//Debug.Log(absoluteVelocity.magnitude);
		//Make sure the player isnt going super duper fast
		if(absoluteVelocity.magnitude  > currentTopAirSpeed/25)
		{
			//Debug.Log("FIXING TOO MUCH SPEED");
			rigidbody.AddForce(-absoluteVelocity * deceleration/1000);
			deceleration = deceleration * 2;
		}
		else
		{
			//Debug.Log("TOO MUCH SPEED SHOULD BE FIXED, CHECK THE DECCELERATION, IT MIGHT BE FUCKED");
			if (deceleration != storedDecceleration)
			{
				deceleration = storedDecceleration;
			}
		}
		
		currentFallSpeed = Mathf.Abs(transform.rigidbody.velocity.y);
		
		if(currentFallSpeed > maxSafeFallSpeed)
		{
		}
		
		SendMessage("PlayFallingSound",transform.rigidbody.velocity.y);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if((currentFallSpeed > maxSafeFallSpeed) && (collision.transform.tag == "Ground"))
		{
			Health -= (currentFallSpeed - maxSafeFallSpeed) * fallDamageMultiplyer;
		}
		
		isGrounded = true;	
	}
	
	void OnCollisionExit(Collision collision)
	{
		
		isGrounded = false;
	}
	
	//These two functions get called from FootColliderController on the footcollider which is a child of player.
	void OnAZeppelin(Collider collider)
	{	
		gameObject.transform.parent = collider.gameObject.transform;
		trackParentVelocity = true;
		onAZeppelin = true;
	}
	
	void OffAZeppelin(Collider collider)
	{
		gameObject.transform.parent = null;
		gameObject.transform.localScale = new Vector3(1,1,1);
		applyExitVelocity = true;
		trackParentVelocity = false;
		onAZeppelin = false;
	}
	
	void OnCollisonStay()
	{

	}
			
}
