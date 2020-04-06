using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;

public class AnisotropicSet : MonoBehaviour
{

	public TextMeshProUGUI aaText;

	int current = 1;
    // Start is called before the first frame update
    void Start()
    {
        //aaText = Component.GetComponent<TextMeshProUGUI>();
        current = PlayerPrefs.GetInt("AF", 1);
        set();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextSetting()
    {
    	current++;
    	current %= 3;
    	PlayerPrefs.SetInt("AF", current);
    	set();
    }

    void set()
    {
    	if(current == 0)aaText.text = "ANISOTROPIC: DISABLED";
    	if(current == 1)aaText.text = "ANISOTROPIC: NORMAL";
    	if(current == 2)aaText.text = "ANISOTROPIC: HIGH";

    	if(current == 0)QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
    	if(current == 1)QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
    	if(current == 2)QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
    }
}
