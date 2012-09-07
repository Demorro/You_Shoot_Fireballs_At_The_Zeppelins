using UnityEngine;
using System.Collections;

public class TriggerTrigger : MonoBehaviour {
	
	public bool hasBeenTriggered = false;
	public string[] tagsToTrigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other)
	{
		foreach(string tag in tagsToTrigger)
		{
			if(other.transform.tag == tag)
			{
				hasBeenTriggered = true;
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		foreach(string tag in tagsToTrigger)
		{
			if(other.transform.tag == tag)
			{
				hasBeenTriggered = false;
			}
		}
	}
}
