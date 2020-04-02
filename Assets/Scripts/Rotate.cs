using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotate;
    public float speed = 300;
    public bool randomize = true;


    void Start()
    {
        if(randomize)
        {
        	//rotate.x = getRand();
        	//rotate.y = getRand();
        	//rotate.z = getRand();
            
            rotate.x = Random.value;
            rotate.y = Random.Range(0, rotate.x);
            rotate.z = 1.0f - (rotate.x + rotate.y);

            rotate.x *= randSign();
            rotate.y *= randSign();
            rotate.z *= randSign();
        }
        rotate *= speed;
    }

    float randSign()
    {
        return Mathf.Sign(Random.value - 0.5f);
    }

    float getRand()
    {
    	float q = Mathf.Floor(Random.value * 3.0f) - 2.0f;
    	//Debug.Log(""+q);
    	return q;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotate * Time.deltaTime);   
    }
}
