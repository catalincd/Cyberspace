using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
  public UnityEvent onInEnd;
  public UnityEvent onOutEnd;
	public float duration = 0.7f;
	public CanvasGroup group;
	bool fadingIn = false;
	bool fadingOut = false;
	float bias = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        bias = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadingIn)
        {
        	bias += Time.deltaTime / duration;
        	group.alpha = bias;

        	if(bias >= 1.0f)
        	{
        		group.alpha = 1.0f;
        		bias = 0.0f;
        		fadingIn = false;
            onInEnd.Invoke();
        	}
        }
        else
        if(fadingOut)
        {
        	bias += Time.deltaTime / duration;
        	group.alpha = 1.0f - bias;

        	if(bias >= 1.0f)
        	{
        		group.alpha = 0.0f;
        		bias = 0.0f;
        		fadingOut = false;
            onOutEnd.Invoke();
        	}
        }
    }

   	public void start()
   	{
   		bias = 0.0f;
   		fadingIn = true;
   	}

   	public void startOut()
   	{
   		bias = 0.0f;
   		fadingOut = true;
   	}
}
