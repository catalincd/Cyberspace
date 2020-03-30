using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour
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
        targetZLeft = transform.position.z + offset;
        targetZRight = transform.position.z - offset;

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
        transform.position = transform.position + new Vector3(0, 0, transitionSpeed * Time.deltaTime);
    	if(transform.position.z > targetZLeft || transform.position.z < targetZRight)
    		transform.position = initialPos;
    }
}
