using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppSet : MonoBehaviour
{

	public int targetFps = 60;

	void Awake()
	{

		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = targetFps;
	}


    // Start is called before the first frame update
    void Start()
    {
    	QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFps;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
