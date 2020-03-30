﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerObject : MonoBehaviour
{
    public float animationTime = 0.75f;
    public OverlayManager overlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameSceneNow()
    {
    	 SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void SetGameScene()
    {
        //Debug.Log("FUCK");
        StartCoroutine(SetGameSceneAfterSeconds());
    }

    IEnumerator SetGameSceneAfterSeconds()
    {
        overlay.start();
        yield return new WaitForSeconds(animationTime);
        //Debug.Log("FUCK");
        SetGameSceneNow();
    }
}