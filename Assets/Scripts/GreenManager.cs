using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenManager : MonoBehaviour
{

	
	public float YPosDown = 0.75f;
    public float YPosUp = 1.75f;
	public float YPosTop = 1.75f;
    public GameObject cylinder;
	public GameObject cylinderP;
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
	    

        bool up = Random.value < 0.50f;
        bool top = Random.value < 0.50f;   
        bool down = Random.value < 0.50f;

        if(!down && !up)
            down = true;

        if(down)
        {
            GameObject tile = Instantiate(cylinder) as GameObject; 
            tile.transform.SetParent(transform);    
            tile.transform.position = (new Vector3(Mathf.Floor(lastAdded), YPosDown, 0));
            spawned.Add(tile);
        } 

        if(up)
        {
            GameObject tile2 = Instantiate(cylinderP) as GameObject; 
            tile2.transform.SetParent(transform);    
            tile2.transform.position = (new Vector3(Mathf.Floor(lastAdded), YPosUp, 0));
            spawned.Add(tile2);
        }

        if(top)
        {
            GameObject tile3 = Instantiate(cylinder) as GameObject; 
            tile3.transform.SetParent(transform);    
            tile3.transform.position = (new Vector3(Mathf.Floor(lastAdded), YPosTop, 0));
            spawned.Add(tile3);
        }

    	lastAdded += thisOffset;
    	thisOffset = offset + Random.Range(-offsetSeed, offsetSeed);
    }

    public void respawn()
    {
        for(int i=0;i<spawned.Count;i++)
            Destroy(spawned[i]);
        spawned.Clear();
    }

}
