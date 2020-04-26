using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void feedbackURL()
    {
    	Application.OpenURL(@"https://docs.google.com/forms/d/e/1FAIpQLSdbbqgkWOjtgvGTQhUTbm935PNwzx04t3RLpf7LBgJJd3KD2w/viewform?usp=sf_link");
    }
}
