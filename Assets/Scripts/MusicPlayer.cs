using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public float volume = 0.75f;
    AudioSource source;
    AudioClip lastClip;

    void Awake()
    {
    	source = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(playNext());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator playNext()
    {
    	AudioClip currentClip = lastClip;
    	while(currentClip == lastClip)
    	{
    		currentClip = clips[Random.Range(0, clips.Length)];
    	}
    	lastClip = currentClip;
    	source.PlayOneShot(currentClip, volume);
    	yield return new WaitForSeconds(currentClip.length);
    	StartCoroutine(playNext());
    }
}
