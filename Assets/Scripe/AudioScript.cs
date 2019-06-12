using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    // Creates a class variable to keep track of 'GameManager' instance
    static AudioScript _instance = null;
        
    public AudioClip MusicClip;
    public AudioClip deathClip;
    public AudioClip TitleClip;
    public AudioClip LevelClip;
    public AudioClip PauseClip;
    public AudioClip FireballClip;
    public AudioClip EnemyDieClip;
    public AudioClip WinClip;
    public AudioClip FlagClip;
    private AudioSource audioClipSource;


    // Give access to private variables (instance variables)
    // - Not needed if using public variables
    // - Variable must be declared above
    // - Variable and method must be static
    public static AudioScript instance
    {
        get { return _instance; }   // can also use just 'get;'
        set { _instance = value; }  // can also use just 'set;'
    }

    // Start is called before the first frame update
    void Start()
    {

        // Check if 'GameManager' instance exists
        if (instance)
            // 'GameManager' already exists, delete copy
            Destroy(gameObject);
        else
        {
            // 'GameManager' does not exist so assign a reference to it
            instance = this;

            //MusicSource.clip = MusicClip;
            //MusicSource.Play();

            audioClipSource = gameObject.AddComponent<AudioSource>();

            // Do not destroy 'GameManager' on Scene change
            DontDestroyOnLoad(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayAudioClip(AudioClip ac, float v = 1)
    {
        audioClipSource.PlayOneShot(ac, v);
    }


}
