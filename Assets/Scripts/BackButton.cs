using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackButton : MonoBehaviour
{
	public UnityEvent onMainBack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void back()
    {
    	onMainBack.Invoke();
    }

}
