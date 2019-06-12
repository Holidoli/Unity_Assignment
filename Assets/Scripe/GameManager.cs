using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Must be added if using SceneManager functions
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private UI _Ui;
    private Animator myAnim;

    // Creates a class variable to keep track of 'GameManager' instance
    static GameManager _instance = null;

    // Used to keep track of 'score' in game
    int _score;

    // Used to instantiate 'Character'
    public GameObject playerPrefab;

    // Use this for initialization
    void Start () {

        // Check if 'GameManager' instance exists
        if (instance)
            // 'GameManager' already exists, delete copy
            Destroy(gameObject);
        else
        {
            // 'GameManager' does not exist so assign a reference to it
            instance = this;

            // Do not destroy 'GameManager' on Scene change
            DontDestroyOnLoad(this);
        }

        // Assign a starting score
        score = 0;
        Player_Control.health = 3;

        myAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        // Check if 'Escape' was pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If player is on 'Screen_Title' (Scene Name)
            if(SceneManager.GetActiveScene().name == "Screen_Title")
            {
                // Go to 'Level1' Scene
                // - Scene must be loaded in Build Settings or it will not work
                // - Build Settings are located at Menu Bar: Edit->Build Settings
                // - Drag the Scenes in the project into 'Scenes in Build' space
                Pause_Game.GameIsPaused = false;
                Time.timeScale = 1f;
                SceneManager.LoadScene("Level1");
            }

            // If player is on 'Level1' (Scene Name)
            else if (SceneManager.GetActiveScene().name == "Level1")
                // Go to 'Screen_Title' Scene
                // - Scene must be loaded in Build Settings or it will not work
                // - Build Settings are located at Menu Bar: Edit->Build Settings
                // - Drag the Scenes in the project into 'Scenes in Build' space
                SceneManager.LoadScene("Screen_Title");
            else if (SceneManager.GetActiveScene().name == "Game_Over")
                SceneManager.LoadScene("Screen_Title");
        }

        //if(SceneManager.GetActiveScene().name == "Level1" && Player_Control.health == 0)
        //{
        //    SceneManager.LoadScene("Game_Over");
        //}

    }

    // Called when 'Character' is spawned
    public void spawnPlayer(int spawnLocation)
    //public void spawnPlayer(Transform spawnLocation)
    //public void spawnPlayer(Vector3 spawnLocation)
    //public void spawnPlayer(GameObject spawnLocation)
    {
        // Requires spawnPoint to be named (SceneName)_(number)
        // - Level1_0
        string spawnPointName = SceneManager.GetActiveScene().name
            + "_" + spawnLocation;

        // Find location to spawn 'Character' at
        Transform spawnPointTransform = 
            GameObject.Find(spawnPointName).GetComponent<Transform>();

        // Check if 'playerPrefab' and 'spawnPointTransform' exist
        if (playerPrefab && spawnPointTransform)
        {
            // Instantiate (Create) 'Character' GameObject
       
            GameObject Player_spawn = Instantiate(playerPrefab, spawnPointTransform.position,
                spawnPointTransform.rotation);

            GameObject.FindWithTag("MainCamera").GetComponent<Camera_Control>().Target = Player_spawn;

            GameObject[] turrets = GameObject.FindGameObjectsWithTag("Enemy_Undestructable");

            for (int i = 0; i < turrets.Length; i++ )
            {
                print(turrets[i].name);
                turrets[i].GetComponent<RangedEnemy>().target = Player_spawn;
            }
        }
        else
            // Prints a message to Console (Shortcut: Control+Shift+C)
            Debug.LogError("Missing Player Prefab or SpawnPoint");

    }

    // Give access to private variables (instance variables)
    // - Not needed if using public variables
    // - Variable must be declared above
    // - Variable and method must be static
    public static GameManager instance
    {
        get { return _instance; }   // can also use just 'get;'
        set { _instance = value; }  // can also use just 'set;'
    }

    // Give access to private variables (instance variables)
    // - Not needed if using public variables
    public int score
    {
        get { return _score; }      // can also use just 'get;'
        set { _score = value; }     // can also use just 'set;'
    }
    public void PlayerDeath()
    {
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().Stop();
        AudioScript.instance.PlayAudioClip(AudioScript.instance.deathClip, 3);
        //myAnim.SetBool("Dead", true);
        StartCoroutine("Mario_Dead");
        Time.timeScale = 0f;
    }

    public void PlayerRestart()
    {
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().Stop();
        AudioScript.instance.PlayAudioClip(AudioScript.instance.deathClip, 3);
        StartCoroutine("Mario_Resetlevel");
        Time.timeScale = 0f;
        
    }

    public void Player_Win()
    {
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().Stop();
        AudioScript.instance.PlayAudioClip(AudioScript.instance.FlagClip, 3);
        AudioScript.instance.PlayAudioClip(AudioScript.instance.WinClip, 3);

        StartCoroutine("Mario_Win");
    }

    IEnumerator Mario_Dead() 
    {
        yield return new WaitForSecondsRealtime(4);
        Time.timeScale = 1f;
        //Player_Control.Destroy(gameObject);
        SceneManager.LoadScene("Game_Over");

    }

    IEnumerator Mario_Resetlevel()
    {
        yield return new WaitForSecondsRealtime(4);

        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    IEnumerator Mario_Win()
    {
        yield return new WaitForSecondsRealtime(8);
        SceneManager.LoadScene("Screen_Title");
    }
}
