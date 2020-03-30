using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationY : MonoBehaviour
{

	public float transitionSpeed = 5.0f;
	public float offset = 1.0f;
	public bool randimzeDirection = true;
	float targetZLeft;
	float targetZRight;
	Vector3 initialPos;
	float direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        targetZLeft = transform.position.y + offset;
        targetZRight = transform.position.y - offset;

        if(randimzeDirection)
        {
        	if(Random.Range(0.0f, 1.0f) > 0.5f)
        	{
        		transitionSpeed = transitionSpeed * (-1.0f);
        	}
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0, transitionSpeed * Time.deltaTime, 0);
    	if(transform.position.y > targetZLeft || transform.position.y < targetZRight)
    		transform.position = initialPos;
    }
}
