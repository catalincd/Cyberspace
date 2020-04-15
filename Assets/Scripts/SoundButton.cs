using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    bool soundON = true;

    void Start()
    {
        soundON = (PlayerPrefs.GetInt("SOUND", 1) > 0);
    	text.text = "SOUND: " + (soundON? "ENABLE":"DISABLE");
    }

    public void Switch()
    {
    	soundON = !soundON;
    	text.text = "SOUND: " + (soundON? "ENABLE":"DISABLE");
    	PlayerPrefs.SetInt("SOUND", soundON? 1:0);
    	AudioListener.volume = soundON? 1.0f : 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
