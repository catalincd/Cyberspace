using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
	public AudioClip[] clips;
	public AudioSource source;
	public float musicVolume = 0.8f;
	float length;
	int q;

    // Start is called before the first frame update
    void Start()
    {
    	source.volume = 1.0f;
        int last = PlayerPrefs.GetInt("LastMusic", 0);
        int q = last;
        while(q == last)
        {
        	q = Random.Range(0, clips.Length);
        }

        length = clips[q].length;

        StartCoroutine(playNext());
    }

    IEnumerator playNext()
    {
    	source.PlayOneShot(clips[q], musicVolume);
    	yield return new WaitForSeconds(length);
    	StartCoroutine(playNext());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
