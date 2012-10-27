using UnityEngine;
using System.Collections;


public class SpinPropellor : MonoBehaviour {
	
	public float speed = 10;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.RotateAround(transform.up,speed);
	}
}
