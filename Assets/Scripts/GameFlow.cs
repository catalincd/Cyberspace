using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameFlow : MonoBehaviour
{
	public UnityEvent onResume;
    bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if(paused && !pauseStatus)
        {
            paused = false;
        	onResume.Invoke();
        }
        if(!paused && pauseStatus)
        {
            paused = true;
        }
    }
}
