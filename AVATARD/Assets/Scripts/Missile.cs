using UnityEngine;
using System.Collections;

public class Missile: MonoBehaviour {
	
	public float speed = 10.0f;
	public bool useGravity = false;
	
	public GameObject explosion;
	public GameObject explosionSound;
	public GameObject travelSound;
	
	private GameObject theTravellingSound;
	
	public float damage = 20;

	// Use this for initialization
	void Start ()
	{
		gameObject.rigidbody.useGravity = useGravity;
		theTravellingSound = Instantiate(travelSound,transform.position,transform.rotation) as GameObject;
		theTravellingSound.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 newPosition = transform.localPosition;
		newPosition += speed * transform.forward * Time.deltaTime;
		transform.localPosition = newPosition;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		Instantiate(explosion,transform.position,transform.rotation);
		Instantiate(explosionSound,transform.position,transform.rotation);
		
		Destroy(gameObject);
	}
}
