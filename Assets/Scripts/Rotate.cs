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
        	rotate.x = getRand();
        	rotate.y = getRand();
        	rotate.z = getRand();
        }
        rotate *= speed;
    }

    float getRand()
    {
    	float q = Mathf.Floor(Random.value * 3.0f) - 1.0f;
    	//Debug.Log(""+q);
    	return q;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotate * Time.deltaTime);   
    }
}
