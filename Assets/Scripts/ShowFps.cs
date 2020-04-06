using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowFps : MonoBehaviour
{
	public TextMeshProUGUI text;
	bool on = true;
    // Start is called before the first frame update
    void Start()
    {
        on = intToBool(PlayerPrefs.GetInt("FPS", 0));
        set();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void set()
    {
    	PlayerPrefs.SetInt("FPS", boolToInt(on));
    	text.text = "SHOW FPS: " + (on? "ENABLED":"DISABLED");
    }

    public void Switch()
    {
    	on = !on;
    	set();
    }

    bool intToBool(int a)
    {
    	return a > 0;
    }

    int boolToInt(bool a)
    {
    	return a? 1:0;
    }
}
