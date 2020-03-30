using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenManager : MonoBehaviour
{

	
	public float YPosDown = 0.75f;
	public float YPosUp = 1.75f;
	public GameObject cylinder;
	public GameObject cam;
	public float offset = 5.0f;
	public float offsetSeed = 2.0f;
	public float adjust = 20.0f;

	public float lastAdded = 10.0f;

	float thisOffset;
	private List<GameObject> spawned;

    // Start is called before the first frame update
    void Start()
    {
        thisOffset = offset;
        spawned = new List<GameObject>(300);
    }

    // Update is called once per frame
    void Update()
    {
    	if(lastAdded - cam.transform.position.x - adjust < thisOffset)
        {
        	add();
        }


        if(spawned.Count > 0)
            if(spawned[0].transform.position.x < cam.transform.position.x)
            {
            	if(spawned[0])
            		Destroy(spawned[0]);
        		spawned.RemoveAt(0);
            }
    }

    void add()
    {
    	GameObject tile;
	    tile = Instantiate(cylinder) as GameObject; 
	    tile.transform.SetParent(transform);  	

	    //float yPos = Mathf.Floor(Random.value * 3.0f) - 1;

	    tile.transform.position = (new Vector3(Mathf.Floor(lastAdded), YPosDown, 0));
	    spawned.Add(tile);
    	lastAdded += thisOffset;
    	thisOffset = offset + Random.Range(-offsetSeed, offsetSeed);
    }
}
