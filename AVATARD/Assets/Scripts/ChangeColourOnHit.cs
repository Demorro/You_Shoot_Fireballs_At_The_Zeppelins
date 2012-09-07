using UnityEngine;
using System.Collections;

public class ChangeColourOnHit : MonoBehaviour {
	
	public Material newColour;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		renderer.material = newColour;
	}
}
