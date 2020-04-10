using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
   
	public GameObject target;
	public float stride;
	public float lower = 0.8f;
	public float upper = 1.2f;
	float bias = 0.0f;
	bool up = true;

    void Start()
    {
        
    }

    void Update()
    {
    	float fScale = lower;
    	bias += Time.deltaTime / stride;
    	if(bias >= 1.0f)
    	{
    		up = !up;
    		bias = 0.0f;
    	}
    	
		if(up)
			fScale = Mathf.SmoothStep(lower, upper, bias);
		else
			fScale = Mathf.SmoothStep(upper, lower, bias);
		
		
		Vector3 currentScale = Vector3.one * fScale;
		target.transform.localScale = currentScale;       
    }
}
