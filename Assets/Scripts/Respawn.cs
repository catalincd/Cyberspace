using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
	public UnityEvent onRespawn;
	public GameObject cam;
	public Text num;
	bool respawned = false;

	int hertz = 0;

	bool shaking = false;
	float cameraShakeBias = 0.0f;
	public float shakeAmplitude = 1.0f;
    public float shakeDuration = 0.7f;
    public AnimationCurve shakeCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.15f, 1.0f),new Keyframe(1.0f, 0.0f));
    Vector3 initCamPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shaking)
        {
        	cameraShakeBias += Time.deltaTime / shakeDuration;
        	float currentAmplitude = shakeAmplitude * shakeCurve.Evaluate(cameraShakeBias);
        	Vector3 offset = Random.insideUnitSphere * currentAmplitude;
        	cam.transform.position = initCamPos + offset;

        	if(cameraShakeBias >= 1.0f)
        	{
        		cam.transform.position = initCamPos;
        		cameraShakeBias = 0.0f;
        		shaking = false;
        	}
        }
    }

    public void begin()
    {
    	hertz = PlayerPrefs.GetInt("hertz", 0);
    	num.text = "" + hertz;
    	respawned = false;
    }

    public void respawn()
    {
    	if(!respawned)
    	{
    		shaking = true;
    		initCamPos = cam.transform.position;
    		onRespawn.Invoke();
    		respawned = true;
    		PlayerPrefs.SetInt("hertz", hertz-1);
    		hertz--;
    		//int hn = PlayerPrefs.GetInt("hertz", 0);
    		//Debug.Log("HERTZ" + hertz + " SAVED:" + hn);
    	}
    }
}
