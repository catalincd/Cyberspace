using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour
{

	public Image img;
	public Color black;
	public Color cyan;

	float bias = 1.0f;
	float animationDuration = 0.72f;

	bool up = false;
	bool down = true;

	Color blackTransparent;
	Color cyanTransparent;

    // Start is called before the first frame update
    void Start()
    {
        img.color = black;
        down = true;
        bias = 0.0f;
        blackTransparent = black;
        blackTransparent.a = 0.0f;
        cyanTransparent = cyan;
        cyanTransparent.a = 0.0f;
    }

    public void start()
    {
    	up = true;
    	down = false;
    }

    // Update is called once per frame
    void Update()
    {	
        if(up)
        {
        	img.color = Color.Lerp(cyanTransparent, cyan, bias);
        	bias += Time.deltaTime / animationDuration;
        	if(bias >= 1.0f)
        	{
        		up = false;
        		bias = 0.0f;
        		img.color = cyan;
        	}
        }
        else if(down)
        {
        	img.color = Color.Lerp(black, blackTransparent, bias);
        	bias += Time.deltaTime / animationDuration;
        	if(bias >= 1.0f)
        	{
        		down = false;
        		bias = 0.0f;
        		img.color = cyanTransparent;
        	}
        }
    }
}
