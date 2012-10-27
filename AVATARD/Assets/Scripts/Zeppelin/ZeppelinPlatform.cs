using UnityEngine;
using System.Collections;

public class ZeppelinPlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "Player")
		{
			gameObject.SendMessageUpwards("ToggleTarget");
		}
	}
	
	void OnCollisionExit(Collision collision)
	{
		if(collision.transform.tag == "Player")
		{
			gameObject.SendMessageUpwards("ToggleTarget");
			collision.rigidbody.velocity = collision.rigidbody.velocity + rigidbody.velocity;
		}
	}
}
