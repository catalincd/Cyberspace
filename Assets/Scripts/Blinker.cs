using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{

	public AnimationCurve curve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.2f, 1.0f), new Keyframe(1.0f, 0.0f));
	public Color targetColor;
    public Color targetDissolveColor;
    public Color targetFadeColor;
	[ColorUsageAttribute(true,true)]
	public Color targetEmColor;
    [ColorUsageAttribute(true,true)]
    public Color targetEmDissolveColor;
    [ColorUsageAttribute(true,true)]
    public Color targetEmFadeColor;

    public float duration = 0.2f;
    public float dissolveDuration = 1.0f;
	public float fadeDuration = 1.0f;

    Color initColor;
	Color currentColor;
	Color currentEmColor;
    bool blinking = false;
	bool dissolving = false;
    bool fading = false;
	float bias = 0.0f;

	Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        initColor = mat.GetColor("_Color");
        currentColor = mat.GetColor("_BaseColor");
        currentEmColor = mat.GetColor("_EmissionColor");
        blinking = false;
        dissolving = false;
        fading = false;
        bias = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(blinking)
        {
        	bias += Time.deltaTime / duration;
        	if(bias >= 1.0f)
        	{
        		blinking = false;
        		bias = 0.0f;
        	}

        	mat.SetColor("_BaseColor", Color.Lerp(currentColor, targetColor, curve.Evaluate(bias)));
        	mat.SetColor("_EmissionColor", Color.Lerp(currentEmColor, targetEmColor, curve.Evaluate(bias)));
        }

        if(dissolving)
        {
            bias += Time.deltaTime / dissolveDuration;
            

            float thisBias = Mathf.SmoothStep(0.0f, 1.0f, bias);
            mat.SetColor("_BaseColor", Color.Lerp(currentColor, targetDissolveColor, thisBias));
            mat.SetColor("_EmissionColor", Color.Lerp(currentEmColor, targetEmDissolveColor, thisBias));

            if(bias >= 1.0f)
            {
                dissolving = false;
                bias = 0.0f;

                currentColor = targetDissolveColor;
                currentEmColor = targetEmDissolveColor;

                mat.SetColor("_BaseColor", targetDissolveColor);
                mat.SetColor("_EmissionColor", targetEmDissolveColor);
            }
        }

        if(fading)
        {
            bias += Time.deltaTime / fadeDuration;
            
            float thisBias = Mathf.SmoothStep(0.0f, 1.0f, bias);
            mat.SetColor("_Color", Color.Lerp(initColor, targetFadeColor, thisBias));
            mat.SetColor("_BaseColor", Color.Lerp(currentColor, targetFadeColor, thisBias));
            mat.SetColor("_EmissionColor", Color.Lerp(currentEmColor, targetEmFadeColor, thisBias));

            if(bias >= 1.0f)
            {
                fading = false;
                bias = 0.0f;
                mat.SetColor("_Color", targetFadeColor);
                mat.SetColor("_BaseColor", targetFadeColor);
                mat.SetColor("_EmissionColor", targetEmFadeColor);
            }
        }
    }

    public void blink()
    {
    	blinking = true;
    	bias = 0.0f;
    }

    public void dissolveColor()
    {
        dissolving = true;
        blinking = false;
        bias = 0.0f;
    }

    public void fadeOut()
    {
        dissolving = false;
        blinking = false;
        fading = true;
        bias = 0.0f;
    }
}
