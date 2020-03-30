using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

	public GameObject[] tiles;
	public GameObject cam;
	public GameObject vertical;
	public float lastAdded = 0;
	public float size = 30;
	public float stride = 90;
	private float ratio;
	public bool debug = false;
	public int idx = 0;
    int lastId = 0;
	private List<GameObject> spawned;
	private List<GameObject> spawnedUps;
	private List<X> spawnedX;

	struct X
	{
		public int l,m,r;
		public int pos;

		public X(int a, int b, int c)
		{
			l = a;
			m = b;
			r = c;
			pos = 0;
		}
	}

	X last;
	X[] tX;

	bool usable(X a)
	{
		return ((a.l + a.m + a.r) > 1);
	}

	void initX()
	{
		tX = new X[10];

		X x0 = new X(1,1,1);
		tX[0] = x0;

		X x1 = new X(0,1,1);
		tX[1] = x1;	

		X x2 = new X(0,0,1);
		tX[2] = x2;	

		X x3 = new X(1,1,0);
		tX[3] = x3;	

		X x4 = new X(1,0,0);
		tX[4] = x4;	

        X x5 = new X(0,1,0);
        tX[5] = x5;

		last = x0;	
	}



	public bool validTarget(int target, float xPos)
	{
		X current;
		if(xPos < spawnedX[0].pos)
		{
			if(target == 1)return spawnedX[0].r > 0;
			if(target == 0)return spawnedX[0].m > 0;
			if(target == -1)return spawnedX[0].l > 0;
		} 
		else
		{
			if(target == 1)return spawnedX[1].r > 0;
			if(target == 0)return spawnedX[1].m > 0;
			if(target == -1)return spawnedX[1].l > 0;
		}

		
		return false;
	}

	bool Compatible(X a, X b)
	{
		return ((a.l == 1 && b.l == 1) || (a.m == 1 && b.m == 1) || (a.r == 1 && b.r == 1));
	}

    void Start()
    {
        spawned = new List<GameObject>(100);
        spawnedUps = new List<GameObject>(100);
        spawnedX = new List<X>(100);
        ratio = (stride - lastAdded) / size + 1;

        initX();

        addFixed();
        addFixed();
        addFixed();
        addFixed();
    }


    void Update()
    {
        if(lastAdded - cam.transform.position.x < stride)
        {
        	if(debug)
        		addFixed(idx);
        	else
        		add();
        }
        if(spawnedUps.Count > 0)
	        if(spawnedUps[0].transform.position.x < cam.transform.position.x)
	        {
	        	Destroy(spawnedUps[0]);
	    		spawnedUps.RemoveAt(0);
	        }
    }

    void addFixed(int _id = 0)
    {
    	GameObject tile;
    	int id = _id;
    	last = tX[id];
        lastId = _id;

    	tile = Instantiate(tiles[id]) as GameObject; 
    	tile.transform.SetParent(transform);
    	lastAdded += size;
    	tile.transform.position = (new Vector3(lastAdded, 0, 0));
    	last.pos = Mathf.RoundToInt(lastAdded + (size / 2));

    	spawned.Add(tile);
    	spawnedX.Add(last);
    	
    	if(spawned.Count > ratio)
    	{
    		Destroy(spawned[0]);
    		spawned.RemoveAt(0);
    		spawnedX.RemoveAt(0);
    	}
    }



    void add()
    {
    	GameObject tile;
    	int id;

    	do
    	{
    		id = (int) Mathf.Floor(Random.Range(0, tiles.Length-1));
    	}
    	while(!Compatible(last, tX[id]));
    	last = tX[id];
        lastId = id;


    	tile = Instantiate(tiles[id]) as GameObject; 
    	tile.transform.SetParent(transform);

    	lastAdded += size;
    	tile.transform.position = (new Vector3(lastAdded, 0, 0));
    	last.pos = Mathf.RoundToInt(lastAdded + (size / 2));

    	spawned.Add(tile);
    	spawnedX.Add(last);

    	if(Random.Range(0.0f, 3.0f) > 1.5f)
    		addVertical(lastId, lastAdded);

    	if(spawned.Count > ratio)
    	{
    		Destroy(spawned[0]);
    		spawned.RemoveAt(0);
    		spawnedX.RemoveAt(0);
    	}
    }

    void addVertical(int id, float x)
    {
    	if(id != 0 && id != 1 && id != 3) return;

    	int pos = 0;

    	if(id == 0) pos = Mathf.RoundToInt(Random.Range(0.51f, 3.49f)) - 2; 
    	else
    	if(id == 1) pos = Mathf.RoundToInt(Random.Range(1.51f, 3.49f)) - 2; 
    	else
    	if(id == 3) pos = Mathf.RoundToInt(Random.Range(0.51f, 2.49f)) - 2; 

    	pos *= 2;
    	GameObject verticalTile = Instantiate(vertical) as GameObject;
    	verticalTile.transform.SetParent(transform);
    	verticalTile.transform.position = new Vector3(x, 0, pos);
    	spawnedUps.Add(verticalTile);

    }
}
