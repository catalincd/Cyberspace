using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdsEnable : MonoBehaviour
{

	public TextMeshProUGUI text;
	bool isEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        isEnabled = !(PlayerPrefs.GetInt("ADS", 1) > 0);
        Switch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch()
    {
    	isEnabled = !isEnabled;

    	if(isEnabled)
    	{
    		text.text = "ADS: ENABLED";
    		PlayerPrefs.SetInt("ADS", 1);
    	}
    	else
    	{
    		text.text = "ADS: DISABLED";
    		PlayerPrefs.SetInt("ADS", 0);
    	}
    	
    }
}
