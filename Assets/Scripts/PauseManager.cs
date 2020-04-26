using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{

    public Cube cube;
	public GameObject canvas;
    public TextMeshProUGUI soundText;
    public RectTransform pause;
    public RectTransform fps;
    public GameObject ready;
    public AudioSource source;
    bool readyForMainMenu = false;
    bool soundON = true;

    // Start is called before the first frame update
    void Start()
    {
        fps.anchoredPosition = fps.anchoredPosition + new Vector2(0.0f, Data.adHeight);
        pause.anchoredPosition = pause.anchoredPosition + new Vector2(0.0f, Data.adHeight);
        readyForMainMenu = false;
        soundON = (PlayerPrefs.GetInt("SOUND", 1) > 0);
        soundText.text = "SOUND:" + (soundON? "ON":"OFF");
        AudioListener.volume = soundON? 1.0f : 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSound()
    {
        soundON = !soundON;
        PlayerPrefs.SetInt("SOUND", soundON? 1:0);
        soundText.text = "SOUND:" + (soundON? "ON":"OFF");
        AudioListener.volume = soundON? 1.0f : 0.0f;
    }

    public void onMainMenu()
    {
        if(!readyForMainMenu)
        {
            readyForMainMenu = true;
            ready.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        }

    }

    public void onPause()
    {
        if(!cube.gameOver)
        {
            readyForMainMenu = false;
            ready.SetActive(false);
            Time.timeScale = 0.0f;
            canvas.SetActive(true);
            source.Pause();
        }
    }

    public void onResume()
    {
    	Time.timeScale = 1.0f;
    	canvas.SetActive(false);
    	source.UnPause();
    }
}
