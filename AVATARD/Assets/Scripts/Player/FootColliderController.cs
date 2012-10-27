using UnityEngine;
using System.Collections;

public class FootColliderController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.transform.tag == "ZeppelinPlatform")
		{
			SendMessageUpwards("OnAZeppelin",collider);
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		if(collider.transform.tag == "ZeppelinPlatform")
		{
			SendMessageUpwards("OffAZeppelin",collider);
		}
	}
}
