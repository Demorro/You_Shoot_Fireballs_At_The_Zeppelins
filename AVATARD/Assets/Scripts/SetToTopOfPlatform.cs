using UnityEngine;
using System.Collections;

public class SetToTopOfPlatform : MonoBehaviour {
	
	
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
			Debug.Log("Got Ere");
			//collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + (collision.transform.lossyScale.y * 0.5f), collision.transform.position.z);
		}
	}
}
