using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

	public Text display;
    public int powerupCoins = 20;

    int initBytes;
    int currentScore = 0;
    int lastAdded = 0;
	int currentHertz = 0;
	int multiplier = 30;
    public bool stopped = true;

    // Start is called before the first frame update
    void Start()
    {
        lastAdded = 0;
        currentScore = 0;
        initBytes = Achievements.getBytes();
        currentHertz = PlayerPrefs.GetInt("hertz", 0);
        if(!stopped)
            display.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increment()
    {
    	if(!stopped)
        {
            currentScore++;
            display.text = "" + currentScore;
        }
    }

    public void incrementMult()
    {
        if(!stopped)
        {
            currentScore += powerupCoins;
            display.text = "" + currentScore;
        }
    }

    public void incrementHertz()
    {
        if(!stopped)
        {
            PlayerPrefs.SetInt("hertz", PlayerPrefs.GetInt("hertz", 0) + 1);
        }
    }

    public int GetScore()
    {
    	return currentScore;
    }

    public void publishScore(int thisScore)
    {
        PlayerPrefs.SetInt("LAST_SCORE", thisScore);
    }

    public void addCoins()
    {
        PlayerPrefs.SetInt("bytes", PlayerPrefs.GetInt("bytes", 0) + currentScore - lastAdded);
        PlayerPrefs.SetInt("A_BYTES", initBytes + currentScore - lastAdded);
        lastAdded = currentScore;
    }

    
}
