using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
	public Material refMaterial;
	public float duration = 7.0f;
	public float animationDuration = 0.7f;


	float bias = 0.0f;

	bool active = false;
	bool ascending = false;
	bool descending = false;

	[ColorUsageAttribute(true,true)]
	public Color defaultColor;
	[ColorUsageAttribute(true,true)]
	public Color red;
	[ColorUsageAttribute(true,true)]
	public Color green;
	[ColorUsageAttribute(true,true)]
	public Color blue;

	Color targetColor;
    // Start is called before the first frame update
    void Start()
    {
        refMaterial.SetColor("_EmissionColor", defaultColor);
    }

    // Update is called once per frame
    void Update()
    {
    	if(active)
    	{
    		if(ascending)
	        {
	        	bias += Time.deltaTime / animationDuration;
	        	refMaterial.SetColor("_EmissionColor", Color.Lerp(defaultColor, targetColor, SmoothStep(bias)));

	        	if(bias >= 1.0f)
	        	{
	        		bias = 0.0f;
	        		ascending = false;
	        		refMaterial.SetColor("_EmissionColor", targetColor);
	        	}
	        }
	        else if(descending)
	        {
	        	bias += Time.deltaTime / animationDuration;
	        	refMaterial.SetColor("_EmissionColor", Color.Lerp(targetColor, defaultColor, SmoothStep(bias)));

	        	if(bias >= 1.0f)
	        	{
	        		bias = 0.0f;
	        		descending = false;
	        		active = false;
	        		refMaterial.SetColor("_EmissionColor", defaultColor);
	        	}
	        }
    	}
    }

    public void reset()
    {
    	bias = 0.0f;
    	ascending = false;
    	active = false;
    	descending = false;
    	refMaterial.SetColor("_EmissionColor", defaultColor);
    }

    void start()
    {
    	bias = 0.0f;
    	ascending = true;
    	active = true;
    	descending = false;
    	StartCoroutine(Descend());
    }

    public void startRed()
    {
    	targetColor = red;
    	start();
    }

    public void startGreen()
    {
        targetColor = green;
        start();
    }

    public void startBlue()
    {
        targetColor = blue;
        start();
    }

    IEnumerator Descend()
    {
    	yield return new WaitForSeconds(duration - animationDuration);
    	descending = true;
    }

    float SmoothStep(float t)
    {
  		return t * t * (3.0f - 2.0f * t);
    }
}
