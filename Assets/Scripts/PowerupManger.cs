using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManger : MonoBehaviour
{

	public GameObject cam;
    public TileManager tiles;
	
	private List<GameObject> spawned;

	public float lastAdded = 0;
	public float offset = 30;
	public float size = 2;
	public float stride = 2;
	private float ratio;
	public float spawnProbability = 0.7f;
	public float lateralOffset = 2.0f;
	public float yPos = 0.5f;

    public GameObject[] ups;
    public float[] probabilities;

    void Start()
    {
          spawned = new List<GameObject>(300);
          ratio = (stride - lastAdded) / size + 1;

         // add(true);
    }


    void Update()
    {
        if(lastAdded - cam.transform.position.x - offset < stride)
        {
        	add();
        }

        if(spawned.Count > 0)
            if(spawned[0])
            {
                if(spawned[0].transform.position.x < cam.transform.position.x)
                {
                    if(spawned[0])
                    Destroy(spawned[0]);
                }
            }
            else spawned.RemoveAt(0);
    }

    void add(bool force = false)
    {
        float rnd = Random.value;
    	if(rnd < spawnProbability || force)
    	{
            int id = 0;
            while(rnd > probabilities[id])id++;

    		GameObject tile;
	    	tile = Instantiate(ups[id]) as GameObject; 
	    	tile.transform.SetParent(transform);  	

            //if(id == 1) Debug.Log("YES");   

	    	int zPos = 0;
            while(!tiles.validTarget(zPos, lastAdded))
            {
                zPos = (int) Mathf.Floor(Random.value * 3.0f) - 1;
            }

	    	tile.transform.position = (new Vector3(lastAdded, yPos, zPos * lateralOffset));
	    	spawned.Add(tile);
    	}
    	lastAdded += size;
    }
}
