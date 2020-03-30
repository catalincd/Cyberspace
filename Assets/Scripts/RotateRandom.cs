using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandom : MonoBehaviour
{


    public float time = 3;
    public float randomTime = 1;
    public float timeScale = 2.0f;
    public bool distanceVariant = true;
    public float invariance = 60.0f;
    public float variance = 360.0f;
    private Vector3 targets;
    private Vector3 starts;
    private Vector3 timeScales;
    private Vector3 bias;



    void Start()
    {
        starts = transform.rotation.eulerAngles;
        bias = new Vector3(1.1f,1.1f,1.1f);
    }

    

    void Update()
    {
        bias += timeScales * timeScale * Time.deltaTime;
        if(bias.x > 1.0f)newTargetX();
        if(bias.y > 1.0f)newTargetY();
        if(bias.z > 1.0f)newTargetZ();

        Vector3 current = new Vector3();
        current.x = Mathf.Lerp(starts.x, targets.x, bias.x);
        current.y = Mathf.Lerp(starts.y, targets.y, bias.y);
        current.z = Mathf.Lerp(starts.z, targets.z, bias.z);
        transform.rotation = Quaternion.Euler(current.x, current.y, current.z);
    }

    void newTargetX()
    {
        timeScales.x = (time + Random.Range(0, randomTime)) / 60.0f;
        bias.x = 0;
        starts.x = targets.x;
        targets.x = starts.x + Random.Range(invariance, 360.0f - invariance);
        if(distanceVariant)timeScales.x *= Mathf.Abs(targets.x - starts.x) / variance;
    }

    void newTargetY()
    {
        timeScales.y = (time + Random.Range(0, randomTime)) / 60.0f;
        bias.y = 0;
        starts.y = targets.y;
        targets.y = starts.y + Random.Range(invariance, 360.0f - invariance);
        if(distanceVariant)timeScales.y *= Mathf.Abs(targets.y - starts.y) / variance;
    }

    void newTargetZ()
    {
        timeScales.z = (time + Random.Range(0, randomTime)) / 60.0f;
        bias.z = 0;
        starts.z = targets.z;
        targets.z = starts.z + Random.Range(invariance, 360.0f - invariance);
        if(distanceVariant)timeScales.z /= Mathf.Abs(targets.z - starts.z) / variance;
    }


}