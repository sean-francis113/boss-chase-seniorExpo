/*
 * 
 * File: loadRun.cs 
 * Author: Sean Francis
 * Description: Loads All Assets Required for a Run.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadRun : MonoBehaviour {

    public GameObject player;

    void Start () {

        InitializeRun();

	}

    /// <summary>
    /// Initialize All Data for the Run
    /// </summary>
    private void InitializeRun()
    {

        /*
         * Load All Track Pieces
         */
        trackConstructor.instance.Initialize();

        /*
         * Load All Test Biome Minions
         */
        spawnEnemies.instance.Initialize();

        trackConstructor.instance.ChangeBiome();
        spawnEnemies.instance.ChangeBiome();

        trackConstructor.instance.SpawnRandomSegment();

        /*
         * Load in Player
         */
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);

    }

}
