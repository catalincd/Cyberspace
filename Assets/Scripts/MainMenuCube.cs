using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCube : MonoBehaviour
{

    public Text coins;
    public Text hertz;
	public GameObject cam;
	public GameObject cubeToInstatiate;
    public Texture[] cubeTextures;
    public Material cubeMaterial;
    public float xOffset = 2.0f;

    public Button leftButton;
    public Button rightButton;
	List<GameObject> cubes;

	public int selected = 0;

	public float transitionSpeed = 0.5f;
	
	float startCamPos = 0.0f;	
	float targetCamPos = 2.0f;	
	float camBias = 0.0f;
	bool transitioning = false;

	float yPos;
	float zPos;


    uint[] available;
    uint[] newCubes;

    public void loadCubes()
    {
        ///BytesManagerCheck
        available = stringToArray(PlayerPrefs.GetString("Available", ""));
        newCubes = stringToArray(PlayerPrefs.GetString("New", ""));

        if(available == null || newCubes == null)
        {
            available = new uint[cubeTextures.Length];
            available[0] = 1;

            newCubes = new uint[cubeTextures.Length];
            saveCubes();
        }
    }

    public void saveCubes()
    {
        PlayerPrefs.SetString("Available", arrayToString(available));
        PlayerPrefs.SetString("New", arrayToString(newCubes));
    }


    void Start()
    {
        cubes = new List<GameObject> (30);
        for(int i=0;i<cubeTextures.Length;i++)
        {
        	addCube(i);
        }

        yPos = cam.transform.position.y;
        zPos = cam.transform.position.z;

        coins.text = "" + PlayerPrefs.GetInt("bytes", 0);
        hertz.text = "" + PlayerPrefs.GetInt("hertz", 0);

        selected = PlayerPrefs.GetInt("selected", 0);
        cam.transform.position = new Vector3(selected * xOffset, yPos, zPos);
        transitioning = false;
        buttonState();
    }

    void addCube(int id)
    {
    	GameObject cube = Instantiate(cubeToInstatiate) as GameObject;
        cube.transform.position = new Vector3(id * xOffset, 0, 0); 
        cube.transform.localScale = new Vector3(25, 25, 25);
        cube.transform.SetParent(transform);
        SetTexture.setMaterial(cubeMaterial, cube);
        SetTexture.setTexture(cubeTextures[id], cube);

        cubes.Add(cube);
    }

    // Update is called once per frame
    void Update()
    {
        if(transitioning)
        {
        	camBias += Time.deltaTime / transitionSpeed;
        	cam.transform.position = new Vector3(Mathf.SmoothStep(startCamPos, targetCamPos, camBias), yPos, zPos);

        	if(camBias >= 1.0f)
        	{
        		transitioning = false;
        		camBias = 0.0f;
        		cam.transform.position = new Vector3(targetCamPos, yPos, zPos);
        	}
        }
    }

    public void goLeft()
    {
    	selected--;
    	camBias = 0.0f;
    	transitioning = true;
    	startCamPos = cam.transform.position.x;
    	targetCamPos = selected * xOffset;
    	buttonState();
    }

    public void goRight()
    {
    	selected++;
    	camBias = 0.0f;
    	transitioning = true;
    	startCamPos = cam.transform.position.x;
    	targetCamPos = selected * xOffset;
    	buttonState();
    }

    void buttonState()
    {
    	PlayerPrefs.SetInt("selected", selected);
    	leftButton.interactable = (selected > 0);
    	rightButton.interactable = (selected < cubeTextures.Length - 1);
    }


    public static string arrayToString(uint[] arr)
    {
        string q = "" + arr[0];
        for(int i=1;i<arr.Length;i++)
        {
            q += "," + arr[i];
        }
        return q;
    }

    public static uint[] stringToArray(string q)
    {
        if(q == "")return null;

        string[] qrr = q.Split(',');
        uint[] arr = new uint[qrr.Length];
        for(int i=0;i<qrr.Length;i++)
        {
            arr[i] = UInt32.Parse(qrr[i]);
        }
        return arr;
    }
}
