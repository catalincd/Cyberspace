using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAntiAliasing(int level)
    {
    	int power = Mathf.RoundToInt(Mathf.Pow(2, level));
    	Debug.Log("" + power);
    	QualitySettings.antiAliasing = power;
    }

    public void setQuality(int level)
    {
    	Debug.Log("" + level);
    	QualitySettings.SetQualityLevel(level, true);
    }
}
