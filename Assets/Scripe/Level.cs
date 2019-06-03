using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    // Variable type should match function in 'GameManager'
    public int spawnLocation;
    //public Transform spawnLocation;
    //public Vector3 spawnLocation;
    // public GameObject spawnLocation;
 
    void Start () {

        // Check if spawnLocation was set properly
        if (spawnLocation < 0)
            // Set 'spawnLocation' to the first spawn point in the 'Level'
            spawnLocation = 0;

        // Call 'spawnPlayer()' from GameManager
        GameManager.instance.spawnPlayer(spawnLocation);
	}
}
