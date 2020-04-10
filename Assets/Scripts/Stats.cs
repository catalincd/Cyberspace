using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
	public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        text.text += "HIGH SCORE: " + PlayerPrefs.GetInt("HIGH_SCORE");
        text.text += "\n\nTOTAL BYTES: " + Achievements.getBytes();
        text.text += "\n\nTOTAL DEATHS: " + Achievements.getDeaths();
        text.text += "\n\nTOTAL RESPAWNS: " + Achievements.getRespawns();
        text.text += "\n\nTOTAL TRIGS: " + Achievements.getUps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
