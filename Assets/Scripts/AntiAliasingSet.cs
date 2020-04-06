using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;

public class AntiAliasingSet : MonoBehaviour
{

	public TextMeshProUGUI aaText;
	public RenderPipelineAsset low;
	public RenderPipelineAsset medium;
	public RenderPipelineAsset high;

	int current = 1;
    // Start is called before the first frame update
    void Start()
    {
        //aaText = Component.GetComponent<TextMeshProUGUI>();
        current = PlayerPrefs.GetInt("MSAA", 1);
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
    	PlayerPrefs.SetInt("MSAA", current);
    	set();
    }

    void set()
    {
    	if(current == 0)GraphicsSettings.renderPipelineAsset = low;
    	if(current == 1)GraphicsSettings.renderPipelineAsset = medium;
    	if(current == 2)GraphicsSettings.renderPipelineAsset = high;

        /*
        if(current == 0)QualitySettings.renderPipeline = low;
        if(current == 1)QualitySettings.renderPipeline = medium;
        if(current == 2)QualitySettings.renderPipeline = high;
        */

        if(current == 0)aaText.text = "ANTI-ALIASING: DISABLED";
        if(current == 1)aaText.text = "ANTI-ALIASING: x2";
        if(current == 2)aaText.text = "ANTI-ALIASING: x4";

    }
}
