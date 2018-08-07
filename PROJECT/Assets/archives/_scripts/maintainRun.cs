/*
 * 
 * File: maintainRun.cs 
 * Author: Sean Francis
 * Description: Continuously Load and Snap Level Pieces
 *              to Make Sure the Run Continues.
 * 
 */

 /*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maintainRun : MonoBehaviour {

    public int biomeLength;

    public List<GameObject> testMinions;
    public GameObject testBoss;

    public float trackTimerBase;

    private bool bossAlive;
    private bool bossStartSpawned = false;

    private float trackTimer;
    private float minionTimer;

    private int minionGroup;
    private int spawnedPiecesNum;

    private trackManager tMang;
    private spawnEnemies spawn;

    private void Awake()
    {

        tMang = GetComponent<trackManager>();
        spawn = GetComponent<spawnEnemies>();

        testMinions = new List<GameObject>();

    }

    // Update is called once per frame
    void Update () {

        CountDown();
		
        if(trackTimer <= 0.0f)
        {

            if (spawnedPiecesNum <= biomeLength)
            {

                tMang.SpawnPiece(false, false);
                spawnedPiecesNum++;


            } else
            {

                if (!bossStartSpawned)
                {

                    tMang.SpawnPiece(true, true);
                    bossStartSpawned = true;

                } else
                {

                    tMang.SpawnPiece(true, false);

                }

            }

        }

	}

    /// <summary>
    /// Countdown All of the Timers.
    /// </summary>
    void CountDown()
    {

        trackTimer -= Time.deltaTime;

    }

    /// <summary>
    /// Adds a Passed In Enemy to the List of Enemies and if 
    /// Enemy is a Boss or Not.
    /// </summary>
    /// <param name="enemy">The Prefab that Contains All the 
    /// Information for the Enemy.</param>
    /// <param name="isBoss">Boolean that Determines if the 
    /// enemy is a Boss or Not.</param>
    public void AddEnemy(GameObject enemy, int biome, bool isBoss)
    {

        switch (biome) {

            case 0:
                /*
                * If the Enemy is a Boss, Set it to the Boss
                * Variable.
                
                if (isBoss)
                {

                    testBoss = enemy;

                }

                /*
                * If the Enemy is NOT a Boss, Add it to
                * the Minions List
                
                else
                {

                    testMinions.Add(enemy);

                }

            break;

        }

    }

    /// <summary>
    /// Sets Up the Next Biome.
    /// </summary>
    public void NewBiome()
    {

        /*
         * Clears Out spawnEnemies' List of Minions 
         * and Boss If Not Already Clear
         
        spawn.ClearEnemies();

        /*
         * Sets the New Current Biome
         
        tMang.SetCurBiome(0); //Will Later Be Random.Range(1, 7)


        /**********CROSSFADE BACKGROUND HERE************/


        /*
         * Sets spawnEnemies' List of Minions and Boss
         * by Checking the New Current Biome.
         * Then Spawn Three Track Pieces.
         
        switch (tMang.GetCurBiome())
        {

            case 0:
                for(int i = 0; i < testMinions.Count; i++)
                {

                    spawn.AddEnemy(testMinions[i], false);

                }

                spawn.AddEnemy(testBoss, true);

                for(int i = 0; i < 3; i++)
                {

                    tMang.SpawnPiece(false, false);

                }

                break;

        }

    }

    public void SetBossAlive(bool set)
    {

        bossAlive = set;

    }

    public void SetBossStart(bool set)
    {

        bossStartSpawned = set;

    }

    public bool GetBossAlive()
    {

        return bossAlive;

    }

    public bool GetBossStart()
    {

        return bossStartSpawned;

    }

}
*/