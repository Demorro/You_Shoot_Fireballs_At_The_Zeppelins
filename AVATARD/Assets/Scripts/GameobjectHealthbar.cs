using UnityEngine;
using System.Collections;

public class GameobjectHealthbar : MonoBehaviour {
	
	public float defaultScaleDistance = 100;
	
	public float timesBiggerAllowed = 3;
	public float timesSmallerAllowed = 3;
	
	public float fadeDistance = 15;
	public float fadeSpeed = 1;
	
	public float healthScale = 1;
	
	private float lastHealth;
	private float healthChange;
	public float barChangeSpeed = 2f;
	
	Color theColor;
	
	private Vector3 healthbarCurrentScale;
	private Vector3 startingScale;
	
	// Use this for initialization
	void Start () {
		startingScale.x = transform.localScale.x;
		startingScale.y = transform.localScale.y;
		startingScale.z = transform.localScale.z;	
		theColor = gameObject.renderer.material.color;
		
		healthbarCurrentScale = Vector3.zero;
		healthbarCurrentScale.x = startingScale.x * transform.parent.GetComponent<ZeppelinController>().currentHealth/100;
		healthbarCurrentScale.y = startingScale.y;
		healthbarCurrentScale.z = startingScale.z;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log(Vector3.Distance(Camera.mainCamera.transform.position, transform.position));
		
		if(healthbarCurrentScale.x > (transform.parent.GetComponent<ZeppelinController>().currentHealth/10 * Vector3.Distance(Camera.mainCamera.transform.position, transform.position)/defaultScaleDistance))
		{
			healthbarCurrentScale.x -= barChangeSpeed * Time.deltaTime;
		}
		else if(healthbarCurrentScale.x < (transform.parent.GetComponent<ZeppelinController>().currentHealth/10 * Vector3.Distance(Camera.mainCamera.transform.position, transform.position)/defaultScaleDistance))
		{
			healthbarCurrentScale.x += barChangeSpeed * Time.deltaTime;
		}
		healthbarCurrentScale.y = startingScale.y * Vector3.Distance(Camera.mainCamera.transform.position, transform.position)/defaultScaleDistance;
		healthbarCurrentScale.z = startingScale.z * Vector3.Distance(Camera.mainCamera.transform.position, transform.position)/defaultScaleDistance;
		
		//makes sure it cant get ludicrously big
		if(healthbarCurrentScale.x > (transform.parent.GetComponent<ZeppelinController>().currentHealth/10 * timesBiggerAllowed))
		{
			healthbarCurrentScale.x = (transform.parent.GetComponent<ZeppelinController>().currentHealth/10 * timesBiggerAllowed);
		}
		if(healthbarCurrentScale.y > startingScale.y * timesBiggerAllowed)
		{
			healthbarCurrentScale.y = startingScale.y * timesBiggerAllowed;
		}
		if(healthbarCurrentScale.z > startingScale.z * timesBiggerAllowed)
		{
			healthbarCurrentScale.z = startingScale.z * timesBiggerAllowed;
		}
		
		//makes sure it cant get ludicrously small
		if(healthbarCurrentScale.x < (transform.parent.GetComponent<ZeppelinController>().currentHealth/10 / timesSmallerAllowed))
		{
			//healthbarCurrentScale.x = (transform.parent.GetComponent<ZeppelinController>().currentHealth/10 / timesSmallerAllowed);
		}
		if(healthbarCurrentScale.y < startingScale.y / timesSmallerAllowed)
		{
			healthbarCurrentScale.y = startingScale.y / timesSmallerAllowed;
		}
		if(healthbarCurrentScale.z < startingScale.z / timesSmallerAllowed)
		{
			healthbarCurrentScale.z = startingScale.z / timesSmallerAllowed;
		}
		
		if(healthbarCurrentScale.x <= 0.001)
		{
			Destroy(this.gameObject);
		}
		
		transform.localScale = healthbarCurrentScale;
		transform.renderer.material.color = theColor;
		
		
		
		//Debug.Log(Vector3.Distance(Camera.mainCamera.transform.position, transform.position));
		
		if((Vector3.Distance(Camera.mainCamera.transform.position, transform.position) < fadeDistance) && (theColor.a > 0))
		{
			theColor.a = theColor.a - 0.005f * fadeSpeed;
			Debug.Log("Fading");
		}
		if((Vector3.Distance(Camera.mainCamera.transform.position, transform.position) > fadeDistance) && (theColor.a < 1))
		{
			theColor.a = theColor.a + 0.005f * fadeSpeed;
			Debug.Log("UnFading");
		}
		
			
		}
}
