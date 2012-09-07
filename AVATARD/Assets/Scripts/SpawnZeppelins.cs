using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnZeppelins : MonoBehaviour {
	
	public GameObject[] spawners;
	public GameObject zeppelin;
	public List<GameObject> allZeppelins;
	public GameObject target;
	public GameObject alternateTarget;
	private int spawnIndex = 0;
	private int currentSpawnerIndex = 0;
	
	public float spawnSpeed = 10.0f;
	public int maxZeppelins = 15;
	
	private float[] zeppelinHeights;
	
	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("SpawnZeppelin",0.0f,spawnSpeed);
		
		zeppelinHeights = new float[maxZeppelins];
		
		for(int i = 0; i < maxZeppelins; i++)
		{
			zeppelinHeights[i] = (i*25) + 15;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(currentSpawnerIndex >= spawners.Length)
		{
			currentSpawnerIndex = 0;
		}
		
	}
	
	void SpawnZeppelin()
	{
		
		if(allZeppelins.Count < maxZeppelins)
		{
			allZeppelins.Add(Instantiate(zeppelin,spawners[currentSpawnerIndex].transform.position + (transform.up * 15),spawners[currentSpawnerIndex].transform.rotation) as GameObject);
			allZeppelins[allZeppelins.Count - 1].GetComponent<RotateTowardsObject>().target = target;
			allZeppelins[allZeppelins.Count - 1].GetComponent<RotateTowardsObject>().alternateTarget = alternateTarget;
			//allZeppelins[allZeppelins.Count - 1].GetComponent<ZeppelinController>().target = target;
			allZeppelins[allZeppelins.Count - 1].GetComponent<ZeppelinController>().spawnZeppelins = this.GetComponent<SpawnZeppelins>();
			
			
			//This bit is for setting the heights so they dont collide, i just havnt figured out how to do it yet, so its still random
			/*foreach(float zeppelinHeight in zeppelinHeights)
			{
				foreach(GameObject currentZeppelin in allZeppelins)
				{
					
				}
			}
			*/
			
			spawnIndex++;
			currentSpawnerIndex++;
			
		}
		
		
	}
}
