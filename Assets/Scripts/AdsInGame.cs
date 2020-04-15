using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdsInGame : MonoBehaviour
{ 
	bool isEnabled = true;
	public TextMeshProUGUI text;

    void Start()
    {
        isEnabled = !(PlayerPrefs.GetInt("ADS_INGAME", 1) > 0);
        Switch();
    }

    void Update()
    {
        
    }


    public void Switch()
    {
    	isEnabled = !isEnabled;
    	PlayerPrefs.SetInt("ADS_INGAME", isEnabled? 1:0);
    	text.text = "IN-GAME ADS: " + (isEnabled? "ENABLED":"DISABLED");
    }
}
