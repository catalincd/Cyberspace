using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTiles : MonoBehaviour
{

	public float lastAdded = -10;
	public float size = 10;
	public float stride = 30;
    public float camBehindOffset = 15;
	public int tilesNum = 5;
	public GameObject cam;
    public GameObject tile;
    public GameObject vertical;
	public GameObject[] horizontalAssets;


    List<GameObject> tiles;
    List<GameObject> verticals;
	List<GameObject> horizontals;


    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<GameObject>(100);
        verticals = new List<GameObject>(100);
        horizontals = new List<GameObject>(100);
    }

    // Update is called once per frame
    void Update()
    {
        while(lastAdded - cam.transform.position.x < stride)
        {
        	add();
        }
    }

    void add()
    {
    	GameObject newTile = Instantiate(tile) as GameObject;
    	newTile.transform.SetParent(transform);
    	newTile.transform.position = new Vector3(lastAdded, 0, 0);
    	tiles.Add(newTile);

        GameObject newVertical = Instantiate(vertical) as GameObject;
        newVertical.transform.SetParent(transform);
        newVertical.transform.position = new Vector3(lastAdded, 0, 2.0f);
        verticals.Add(newVertical);

        GameObject newVertical2 = Instantiate(vertical) as GameObject;
        newVertical2.transform.SetParent(transform);
        newVertical2.transform.position = new Vector3(lastAdded, 0, -2.0f);
        verticals.Add(newVertical2);

        GameObject newHorizontal = Instantiate(horizontalAssets[Random.Range(0, horizontalAssets.Length)]) as GameObject;
        newHorizontal.transform.SetParent(transform);
        newHorizontal.transform.position = new Vector3(lastAdded - 5.0f, 0.65f + Random.Range(0, 3), 0.0f);
        horizontals.Add(newHorizontal);

    	lastAdded += size;
    	check();
    }

    void check()
    {
    	if(tiles[0].transform.position.x < cam.transform.position.x - camBehindOffset && tiles.Count > tilesNum)
    	{
    		Destroy(tiles[0]);
    		tiles.RemoveAt(0);

            Destroy(horizontals[0]);
            horizontals.RemoveAt(0);

            Destroy(verticals[0]);
            verticals.RemoveAt(0);
            Destroy(verticals[1]);
            verticals.RemoveAt(1);
    	}
    }
}
