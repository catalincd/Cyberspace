using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuCube : MonoBehaviour
{

    public Text coins;
    public Text hertz;
    public Text highScore;
    public Text highText;
    public UISoundPlayer sound;

    public TextAsset DefaultAsset;

    public TextMeshProUGUI topText;
    public TextMeshProUGUI bottomText;
    public TextMeshProUGUI costText;
    public GameObject img;

    public Button StartButton;

	public GameObject cam;
	public GameObject cubeToInstatiate;
	public int maxId = 20;
    public Texture[] cubeTextures;
    public Material cubeMaterial;
    public float xOffset = 2.0f;

    public Button leftButton;
    public Button rightButton;

    public GameObject leftNew;
    public GameObject rightNew;

	List<GameObject> cubes;
    List<MCube> MCubes;

	public int selected = 0;

	public float transitionSpeed = 0.5f;
	
	float startCamPos = 0.0f;	
	float targetCamPos = 2.0f;	
	float camBias = 0.0f;
	bool transitioning = false;
	bool changed = false;
	int linesNum = 0;

	float yPos;
	float zPos;

    int minNew;
    int maxNew;
    bool isNew = false;


    List<int> newCubes;


    void refreshNews(bool force = false)
    {
        if(!isNew && !force)return;

        if(!force)
        {
            for(int i=0;i<newCubes.Count;i++)
            {
                if(newCubes[i] == selected)
                    newCubes.RemoveAt(i);
            }
        }

        if(newCubes.Count > 0)
        {
            isNew = true;
            minNew = maxNew = newCubes[0];
            for(int i=1;i<newCubes.Count;i++)
            {
                minNew = Mathf.Min(minNew, newCubes[i]);
                maxNew = Mathf.Max(minNew, newCubes[i]);
            }
        }
        else 
            isNew = false;

        if(isNew)
        {   
            leftNew.SetActive(minNew <= selected);
            rightNew.SetActive(maxNew >= selected);
        }
        else
        {
            leftNew.SetActive(false);
            rightNew.SetActive(false);
        }
    }



    void Start()
    {

        if(PlayerPrefs.GetInt("LAST_SCORE") > PlayerPrefs.GetInt("HIGH_SCORE"))
        {
            PlayerPrefs.SetInt("HIGH_SCORE", PlayerPrefs.GetInt("LAST_SCORE"));
            highText.gameObject.SetActive(true);
        }
        else
        {
            highText.gameObject.SetActive(false);
        }

        highScore.text = "" + PlayerPrefs.GetInt("HIGH_SCORE");
        

        linesNum = DefaultAsset.text.Split('\n').Length;

        newCubes = new List<int> (50);
        cubes = new List<GameObject> (50);
        changed = false;
        LoadCubes();
        CheckCubes();
        refreshNews(true);

        for(int i=0;i<maxId;i++)
        {
        	addCube(i);
        }

        yPos = cam.transform.position.y;
        zPos = cam.transform.position.z;

        coins.text = "" + PlayerPrefs.GetInt("bytes", 0);
        hertz.text = "" + PlayerPrefs.GetInt("hertz", 0);


        


        selected = PlayerPrefs.GetInt("selected", 0) + 1;

        for(int i=0;i<MCubes.Count;i++)
        {
            if(MCubes[i].textureId == selected)
            {
                selected = i;
                break;
            }
        }

        cam.transform.position = new Vector3(selected * xOffset, yPos, zPos);
        transitioning = false;
        refreshTexts();
        buttonState();

        AudioListener.volume = PlayerPrefs.GetInt("SOUND");
    }


    public void CheckCubes()
    {
        

        int respawns = Achievements.getRespawns();
        if(respawns > 0 && !MCubes[5].available){MCubes[5].available = true; newCubes.Add(5); changed = true;}
        if(respawns > 10 && !MCubes[6].available){MCubes[6].available = true; newCubes.Add(6); changed = true;}
        if(respawns > 100 && !MCubes[7].available){MCubes[7].available = true; newCubes.Add(7); changed = true;}

        int ups = Achievements.getUps();
        if(ups > 0 && !MCubes[8].available){MCubes[8].available = true; newCubes.Add(8); changed = true;}
        if(ups > 10 && !MCubes[9].available){MCubes[9].available = true; newCubes.Add(9); changed = true;}
        if(ups > 100 && !MCubes[10].available){MCubes[10].available = true; newCubes.Add(10); changed = true;}

        int deaths = Achievements.getDeaths();
        if(deaths > 10 && !MCubes[17].available){MCubes[17].available = true; newCubes.Add(17); changed = true;}
        if(deaths > 100 && !MCubes[18].available){MCubes[18].available = true; newCubes.Add(18); changed = true;}
        if(deaths > 1000 && !MCubes[19].available){MCubes[19].available = true; newCubes.Add(19); changed = true;}

        int bytes = Achievements.getBytes();
        if(bytes > 100 && !MCubes[11].available){MCubes[11].available = true; newCubes.Add(11); changed = true;}
        if(bytes > 1000 && !MCubes[12].available){MCubes[12].available = true; newCubes.Add(12); changed = true;}
        if(bytes > 10000 && !MCubes[13].available){MCubes[13].available = true; newCubes.Add(13); changed = true;}

        int score = PlayerPrefs.GetInt("HIGH_SCORE");
        if(score > 100000 && !MCubes[14].available){MCubes[14].available = true; newCubes.Add(14); changed = true;}
        if(score > 300000 && !MCubes[15].available){MCubes[15].available = true; newCubes.Add(15); changed = true;}
        if(score > 1000000 && !MCubes[16].available){MCubes[16].available = true; newCubes.Add(16); changed = true;}
        if(score > 1500000 && !MCubes[20].available){MCubes[20].available = true; newCubes.Add(20); changed = true;}

        if(changed)SaveCubes();
    }




    void refreshTexts()
    {

        topText.text = MCubes[selected].name;
        if(MCubes[selected].collectible && !MCubes[selected].available)
        {
            bottomText.text = MCubes[selected].requirements;
            costText.text = "";
            img.SetActive(false);
        }
        else if(!MCubes[selected].available)
        {
            costText.text = "" + MCubes[selected].cost;
            img.SetActive(true);
            if(PlayerPrefs.GetInt("bytes", 0) >= MCubes[selected].cost)
            {
                costText.text += "    BUY";
            }
            bottomText.text = "";
        }
        else
        {
            costText.text = "";
            bottomText.text = "";
            img.SetActive(false);
        }
        
    }

    public void buyCurrent()
    {
        Debug.Log("PRESSSS");
        int currentBytes = PlayerPrefs.GetInt("bytes", 0);
        if(currentBytes < MCubes[selected].cost)return;
        MCubes[selected].available = true;
        PlayerPrefs.SetInt("bytes", currentBytes - MCubes[selected].cost);
        coins.text = "" + PlayerPrefs.GetInt("bytes", 0);
        SaveCubes();
        buttonState();
        refreshTexts();
    }

    void addCube(int id)
    {
    	GameObject cube = Instantiate(cubeToInstatiate) as GameObject;
        cube.transform.position = new Vector3(id * xOffset, 0, 0); 
        cube.transform.localScale = new Vector3(25, 25, 25);
        cube.transform.SetParent(transform);
        SetTexture.setMaterial(cubeMaterial, cube);
        SetTexture.setTexture(cubeTextures[MCubes[id].textureId - 1], cube);
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
        sound.playClip();
    	selected--;
    	camBias = 0.0f;
    	transitioning = true;
    	startCamPos = cam.transform.position.x;
    	targetCamPos = selected * xOffset;
    	buttonState();
        refreshTexts();
        refreshNews();
    }

    public void goRight()
    {
        sound.playClip();
    	selected++;
    	camBias = 0.0f;
    	transitioning = true;
    	startCamPos = cam.transform.position.x;
    	targetCamPos = selected * xOffset;
    	buttonState();
        refreshTexts();
        refreshNews();
    }

    void buttonState()
    {
        bool av = MCubes[selected].available;
        StartButton.interactable = av;
        if(av)
    	{
    		PlayerPrefs.SetInt("selected", MCubes[selected].textureId - 1);
    		Data.playerTexture = cubeTextures[MCubes[selected].textureId - 1];
    	}
    	leftButton.interactable = (selected > 0);
    	rightButton.interactable = (selected < maxId - 1);
    }

    public static string getPath()
    {
        return Application.persistentDataPath + "/serialized.json";
    }

    public void LoadCubes()
    {
        string path = getPath();
        if(!File.Exists(path))
        {
            File.WriteAllText(path, DefaultAsset.text);
        }

        string[] lines = File.ReadAllLines(path);
        MCubes = new List<MCube>(lines.Length);
        

        if(lines.Length != linesNum)
        {
        	Debug.Log("linesNum: " + linesNum + " lines: "+lines.Length);
        	changed = true;
        	string[] newLines = DefaultAsset.text.Split('\n');

        	for(int i = 0;i < linesNum;i++)
        	{
        		MCubes.Add(JsonUtility.FromJson<MCube>(newLines[i]));
        	}

        	for(int i=0;i<lines.Length;i++)
	        {
	        	MCube current = JsonUtility.FromJson<MCube>(lines[i]);
                if(MCubes[i] != null && current != null)
	                MCubes[i].available = current.available;
                //Debug.Log("DONE:" + i);
	        }
        }
        else
	        for(int i=0;i<lines.Length;i++)
	        {
	            MCubes.Add(JsonUtility.FromJson<MCube>(lines[i]));
	        }
    }

    public void SaveCubes()
    {
        string[] lines = new string[MCubes.Count];
        for(int i=0;i<MCubes.Count;i++)
        {
            lines[i] = JsonUtility.ToJson(MCubes[i]);
        }
        File.WriteAllLines(getPath(), lines);
    }

    [Serializable]
    public class MCube
    {
        public string name;
        public bool available;
        public bool collectible;
        public int cost;
        public string requirements;
        public int textureId;
    }


}
