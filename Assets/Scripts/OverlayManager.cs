using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour
{

	public Image img;
	public Color black;
	public Color cyan;
    public GameObject obj;

	float bias = 1.0f;
	public float animationDuration = 0.72f;

    bool up = false;
	bool up2 = false;
	bool down = true;
    bool down2 = false;
    bool toRestart = false;

	Color blackTransparent;
	Color cyanTransparent;

    // Start is called before the first frame update
    void Start()
    {
        obj.SetActive(true);
        img.color = black;
        down = true;
        bias = 0.0f;
        blackTransparent = black;
        blackTransparent.a = 0.0f;
        cyanTransparent = cyan;
        cyanTransparent.a = 0.0f;
    }

    public void restart()
    {
        up = false;
        down = false;
        down2 = true;
        up2 = false;
    }

    public void respStart()
    {
        obj.SetActive(true);
        up2 = true;
         up = false;
        down = false;
        down2 = false;
    }

    public void start()
    {
        obj.SetActive(true);
    	up = true;
    	down = false;
    }

    // Update is called once per frame
    void Update()
    {	
        if(up2)
        {
            img.color = Color.Lerp(blackTransparent, black, bias);
            bias += Time.deltaTime / animationDuration;
            if(bias >= 1.0f)
            {
                up2 = false;
                bias = 0.0f;
                img.color = black;
                restart();
            }
        }
        else if(down2)
        {
            img.color = Color.Lerp(black, blackTransparent, bias);
            bias += Time.deltaTime / animationDuration;
            if(bias >= 1.0f)
            {
                down2 = false;
                bias = 0.0f;
                img.color = blackTransparent;
                obj.SetActive(false);
            }
        }
        else if(up)
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
                obj.SetActive(false);
        	}
        }
    }
}
