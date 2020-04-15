using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneManagerObject : MonoBehaviour
{
    public float animationTime = 0.75f;
    public OverlayManager overlay;
    public UnityEvent destroyEvent;
    public AdManager ads;
    bool adjusted = false;

    public bool mainMenu = false;
    public RectTransform upperText;
    public RectTransform left;
    public RectTransform right;
    public RectTransform start;
    public RectTransform extras;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnGUI()
    {
        if(!adjusted && mainMenu)
        {
            if(Screen.height / Screen.width < 1.5)
            {
                Vector2 offset = new Vector2(0.0f, Screen.width * 0.10f);

                start.anchoredPosition = start.anchoredPosition - new Vector2(0.0f, Screen.width * 0.20f);
                extras.anchoredPosition = extras.anchoredPosition - new Vector2(0.0f, Screen.width * 0.10f);


                left.anchoredPosition = left.anchoredPosition - offset;
                right.anchoredPosition = right.anchoredPosition - offset;
                upperText.anchoredPosition = upperText.anchoredPosition - offset;


                start.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                extras.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            }   
            adjusted = true;
        }
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
        if(PlayerPrefs.GetInt("ADS_INGAME", 1) == 0)
        {
            ads.HideBanner();
        }
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
