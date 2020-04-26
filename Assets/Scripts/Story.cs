using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{

	public GameObject cam;
	public float camSpeed = 7.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = cam.transform.position + new Vector3(Time.deltaTime * camSpeed, 0.0f, 0.0f);
    }
}
