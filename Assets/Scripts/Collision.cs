using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collision : MonoBehaviour
{

    public MaterialManager material;
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
        //Debug.Log(gOther.tag);
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
        if(gOther.tag == "CT")
        {
            cube.CollideGreen(true);
        }
        else
        if(gOther.tag == "T")
        {
            if(!cube.powerUp)
            {
                material.startRed();
                cube.startRed();
            }
            else
                score.incrementMult();
            Destroy(gOther);
        }
        else
        if(gOther.tag == "T2")
        {
            if(!cube.powerUp)
            {
                material.startGreen();
                cube.startRed();
            }
            else
                score.incrementMult();
            Destroy(gOther);
        }
        else
        if(gOther.tag == "T3")
        {
            if(!cube.powerUp)
            {
                material.startBlue();
                cube.startRed();
            }
            else
                score.incrementMult();
            Destroy(gOther);
        }
    }
}
