using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {
	
	public float range;
	private Ray ray;
	private RaycastHit hitInfo;
	
	public bool canShoot = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		ray.origin = transform.position;
		ray.direction = transform.up;
		
		if(Physics.Raycast(ray,out hitInfo,range))
		{
			if(hitInfo.collider.tag == "Player")
			{
				//gets here if the player is in range and the gun is pointing at him
				canShoot = true;
			}
		}
		Debug.DrawRay(transform.position,transform.up * range,Color.red);
	}
}


//QUICKIE ALGORITHM

/*
 * up above
 * if((Physics.Raycast(ray,out hitInfo,range)) && (isShooting == false) && (timeSinceLastShot > reloadTime)))
 * {
 * 	shoot()
 * }
 * 
 * 
 * void shoot(RaycastHit hitInfo)
 * {
 * 	timeSinceLastShot = 0;
 * 	isShooting = true;
 *  displayMuzzleFlash();
 * 	shootTracer();
 * 	playerPos = hitInfo.transform.position;
 * 	
 *  There will be a problem here, as shoot is only called once, and if you do a while loop it will hang, FIGURE IT OUT
 * 	wait for waitTime * Distance
 * 
 * 	instatiate explosion at playerPos
 * 	if player is near explosion
 * 	{
 * 		hurtTheBitch();
 * 	}
 * 	isShooting = false;
 * 	playerPos = null;
 * }
 */
