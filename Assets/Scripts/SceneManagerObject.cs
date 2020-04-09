using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneManagerObject : MonoBehaviour
{
    public float animationTime = 0.75f;
    public OverlayManager overlay;
    public UnityEvent destroyEvent;

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
        destroyEvent.Invoke();
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

    public void SetSettingsSceneNow()
    {
    	 SceneManager.LoadScene("SettingsScene", LoadSceneMode.Single);
    }

    public void SetSettingsScene()
    {
        StartCoroutine(SetSettingsSceneAfterSeconds());
    }

    IEnumerator SetSettingsSceneAfterSeconds()
    {
        overlay.start();
        yield return new WaitForSeconds(animationTime);
        //Debug.Log("FUCK");
        SetSettingsSceneNow();
    }

    public void SetMainMenuSceneNow()
    {
         SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    public void SetMainMenuScene()
    {
        StartCoroutine(SetMainMenuSceneAfterSeconds());
    }

    IEnumerator SetMainMenuSceneAfterSeconds()
    {
        overlay.start();
        yield return new WaitForSeconds(animationTime);
        //Debug.Log("FUCK");
        SetMainMenuSceneNow();
    }
}
