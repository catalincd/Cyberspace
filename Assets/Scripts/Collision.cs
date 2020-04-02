using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collision : MonoBehaviour
{

    
	public ScoreManager score;
    public Cube cube;
    Blinker blinker;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        blinker = GetComponent<Blinker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject gOther = other.gameObject;
        if(gOther.tag == "P")
        {
            blinker.blink();
        	Destroy(gOther);
        	score.increment();
            //Debug.Log("UP");
        }
        else
        if(gOther.tag == "B")
        {
            blinker.blinkCyan();
            Destroy(gOther);
            score.incrementHertz();
            //Debug.Log("UP");
        }
        else
        if(gOther.tag == "CC")
        {
            cube.CollideGreen();
        }
        else
        if(gOther.tag == "CP")
        {
            cube.CollideGreen(true);
        }
        else
        if(gOther.tag == "T")
        {
            cube.startRed();
            Destroy(gOther);
        }
    }
}
