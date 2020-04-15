using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{

	public AudioClip[] clips;
	public float volume = 0.7f;
	AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    void Awake()
    {
    	source = GetComponent<AudioSource>();
    }

    public void playClip(int id = 0)
    {
    	source.PlayOneShot(clips[id], volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
