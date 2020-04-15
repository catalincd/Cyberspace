using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Coins")]
    public AudioClip[] coinClips;
    public float volumeCoin = 0.8f;
    public float lowPitchCoin = 0.85f;
    public float highPitchCoin = 1.15f;



   	public AudioClip explosion;
   	public float explosionVolume = 1.0f;

   	public AudioClip hertz;
   	public float hertzVolume = 1.0f;

   	public AudioClip trig;
   	public float trigVolume = 1.0f;

    private AudioSource source;

    public void playCoin()
    {
        source.pitch = Random.Range (lowPitchCoin,highPitchCoin);

        AudioClip coinClip = coinClips[Random.Range(0, coinClips.Length)];
        source.PlayOneShot(coinClip, volumeCoin);
    }

    public void playHertz()
    {
        source.PlayOneShot(hertz, hertzVolume);
    }

    public void playExplosion()
    {
    	source.PlayOneShot(explosion, explosionVolume);
    }

    public void playTrig()
    {
    	source.PlayOneShot(trig, trigVolume);
    }

    void Awake()
    {
    	source = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
