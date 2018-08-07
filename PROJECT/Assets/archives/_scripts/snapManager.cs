/*
 * 
 * File: snapManager.cs 
 * Author: Sean Francis
 * Description: Manages Creating the Track at the Start
 *              and Throughout the Run.
 * 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapManager : MonoBehaviour {

    //The Test Level Prefabs
    public List<GameObject> testLevels;
    public List<GameObject> testBossLevel;
    
    //The Grasslands Level Prefabs
    public List<GameObject> grassLevels;
    public List<GameObject> grassBossLevel;
    
    //The City of Metal Level Prefabs
    public List<GameObject> metalLevels;
    public List<GameObject> metalBossLevel;

    //The Molten Fields Level Prefabs
    public List<GameObject> heatLevels;
    public List<GameObject> heatBossLevel;

    //The Land of Ice and Snow Level Prefabs
    public List<GameObject> snowLevels;
    public List<GameObject> snowBossLevel;

    //The Crystalline Caverns Level Prefabs
    public List<GameObject> crystalLevels;
    public List<GameObject> crystalBossLevel;

    //The Corrupted Castle Level Prefabs
    public List<GameObject> castleLevels;
    public List<GameObject> castleBossLevel;

    //The Roaring Sea Level Prefabs
    public List<GameObject> seaLevels;
    public List<GameObject> seaBossLevel;

    //Reference to the AutoMove Script
    private autoMove aMove;

    //If the Start of the Track was Created Successfully or Not
    private bool trackStartSuccessful;

    //The Last Track Piece to be Generated
    //private GameObject lastPiece;
    //private GameObject lastLeftSnapPoint;
    //private GameObject lastRightSnapPoint;

    //public LevelChunk lastChunk;

    //The Track Piece to be Generated Next
    private GameObject newlySpawnedPiece;
    //private GameObject newLeftSnapPoint;
    public Transform snapPoint;

    //The Biome the Player is Currently Running Through.
    //0 - Test, 1 - Grasslands, 2 - City of Metal, 3 - Molten Fields
    //4 - Land of Ice and Snow, 5 - Crystalline Caverns, 6 - Corrupted Castle
    //7 - Roaring Sea
    private int currentBiome;

    //The Biome to Set Up Next.
    //0 - Test, 1 - Grasslands, 2 - City of Metal, 3 - Molten Fields
    //4 - Land of Ice and Snow, 5 - Crystalline Caverns, 6 - Corrupted Castle
    //7 - Roaring Sea
    private int nextBiome;

    //How Long Will Each Biome Be Before the Boss Fight?
    private int biomeLength = 1;

    private maintainRun maintain;

    private void Awake()
    {

        //Get the Auto Move Script
        aMove = GetComponent<autoMove>();

        //Get the Maintain Run Script
        maintain = GetComponent<maintainRun>();

    }

    public bool loadTrackStart()
    {

        //Later will be = Random.Range(1, 7)
        //Determine Random Biome
        currentBiome = 0;

        switch (currentBiome)
        {

            case 0:
                return createTrack(testLevels, testBossLevel);

        }

        return true;

    }

    private bool snapTrack(List<GameObject> biomeLevels)
    {

        //Randomly Choose a New Piece to Snap into Track
        //newly spawned piece will be snapped to snapPoint
        newlySpawnedPiece = choosePiece(biomeLevels, 0);
        
        if (newlySpawnedPiece != null)
        {
            //LevelChunk currentChunk = newlySpawnedPiece.GetComponent<LevelChunk>();
            if (currentChunk)
            {
                snapPoint = currentChunk.snapPoint;
            }
            //newLeftSnapPoint = snapPiece.GetComponentInChildren<Transform>().
            //Find("leftSnapPoint").gameObject;
            //snapPoint = snapPiece.GetComponentInChildren<Transform>().
                //Find("rightSnapPoint").gameObject;

            //if (lastChunk != null)
            {
                
                //Snap Piece into the Track
                newlySpawnedPiece.transform.Translate(
                    new Vector3(
                        //lastChunk.snapPoint.position.x,
                        //lastChunk.snapPoint.position.y,
                        //lastChunk.snapPoint.position.z
                        //));

            }
            else //This is the first piece spawned
            {
                //Snap Piece into the Track
                newlySpawnedPiece.transform.Translate(
                    new Vector3(-6, -7, 0));

            }

            //Display Piece
            newlySpawnedPiece.SetActive(true);
            aMove.addToActive(newlySpawnedPiece);
            lastChunk = currentChunk;
            //lastLeftSnapPoint = newLeftSnapPoint;
            //lastRightSnapPoint = snapPoint;

        }
        else //failed to spawn a new piece!
        {
            Debug.LogError("failed to spawn a new piece");
            //Instantiate/Load Copy of snapPiece
            GameObject newPiece =
                Instantiate(biomeLevels[Random.Range(0, biomeLevels.Count)], 
                new Vector3(
                    lastChunk.snapPoint.position.x,
                    lastChunk.snapPoint.position.y,
                    lastChunk.snapPoint.position.z
                    ), 
                Quaternion.identity);

           // newLeftSnapPoint = newPiece.GetComponentInChildren<Transform>().
               // Find("leftSnapPoint").gameObject;
            //snapPoint = newPiece.GetComponentInChildren<Transform>().
               // Find("rightSnapPoint").gameObject;

            //Add New Piece into List
            biomeLevels.Add(newPiece);

            //Display New Piece
            newPiece.SetActive(true);
            aMove.addToActive(newPiece);
            lastChunk = newPiece.GetComponent<LevelChunk>();
            //lastLeftSnapPoint = newLeftSnapPoint;
           // lastRightSnapPoint = snapPoint;

        }

        return true;

    }

    public bool createTrack(List<GameObject> levels, List<GameObject> bossLevel)
    {

        //Generate biomeLength Amounts of Track Pieces
        for (int i = 0; i < biomeLength; i++)
        {

            //Create One Piece of Track
            trackStartSuccessful = snapTrack(levels);

            //If Track Somehow Errored/Failed, 
            //Escape Loop and Return False
            if (!trackStartSuccessful)
            {

                return trackStartSuccessful;

            }

        }

        //Generates Four Boss Pieces
        for (int i = 0; i < 4; i++)
        {

            trackStartSuccessful = snapBossTrack(bossLevel);

        }

        //Track Start Generated Successfully
        return true;

    }

    //If Called Without Specific Boss Level List
    public bool snapBossTrack()
    {

        switch (currentBiome)
        {

            case 0:
                snapBossTrack(testBossLevel);
                break;
            case 1:
                snapBossTrack(grassBossLevel);
                break;
            case 2:
                snapBossTrack(metalBossLevel);
                break;
            case 3:
                snapBossTrack(heatBossLevel);
                break;
            case 4:
                snapBossTrack(snowBossLevel);
                break;
            case 5:
                snapBossTrack(crystalBossLevel);
                break;
            case 6:
                snapBossTrack(castleBossLevel);
                break;
            case 7:
                snapBossTrack(seaBossLevel);
                break;

        }

        return true;

    }

    public bool snapBossTrack(List<GameObject> bossLevel)
    {

        GameObject bossPiece = null;

        for(int i = 0; i < bossLevel.Count; i++)
        {

            //Checks to Make Sure that the 'Start'
            //Piece is not spawned again unnecessarily
            if ((i != 0 && maintain.bossAlive) ||
                (i == 0 && !maintain.bossAlive))
            {

                if (!bossLevel[i].activeSelf)
                {

                    bossPiece = bossLevel[i];
                    break;

                }

            }

        }

        if(bossPiece != null)
        {

            //newLeftSnapPoint = bossPiece.GetComponentInChildren<Transform>().
               //     Find("leftSnapPoint").gameObject;
            //snapPoint = bossPiece.GetComponentInChildren<Transform>().
               // Find("rightSnapPoint").gameObject;

            //Debug.Log("Last Boss Right Snap Pos X Before: " + lastRightSnapPoint.transform.position.x);
            //Debug.Log("Last Boss Right Snap Pos Y Before: " + lastRightSnapPoint.transform.position.y);

            bossPiece.transform.Translate(
                new Vector3(
                    lastChunk.snapPoint.position.x,
                    lastChunk.snapPoint.position.y,
                    lastChunk.snapPoint.position.z
                    ));

            //Display New Piece
            bossPiece.SetActive(true);
            aMove.addToActive(bossPiece);
            lastChunk = bossPiece.GetComponent<LevelChunk>();
            //lastLeftSnapPoint = newLeftSnapPoint;
            //lastRightSnapPoint = snapPoint;
            //Debug.Log("Last Boss Right Snap Pos X After: " + lastRightSnapPoint.transform.position.x);
            //Debug.Log("Last Boss Right Snap Pos Y After: " + lastRightSnapPoint.transform.position.y);

        } else
        {

            //Debug.Log("Last Boss Right Snap Pos X Before: " + lastRightSnapPoint.transform.position.x);
            //Debug.Log("Last Boss Right Snap Pos Y Before: " + lastRightSnapPoint.transform.position.y);

            //Spawn a Non-Starter Boss Piece
            /*
            GameObject newBossPiece = Instantiate(bossLevel[1],
                new Vector3(
                    lastRightSnapPoint.transform.position.x,
                    lastRightSnapPoint.transform.position.y,
                    lastRightSnapPoint.transform.position.z
                    ),
                Quaternion.identity);

            bossLevel.Add(newBossPiece);

            //newLeftSnapPoint = newBossPiece.GetComponentInChildren<Transform>().
                   // Find("leftSnapPoint").gameObject;
            //newRightSnapPoint = newBossPiece.GetComponentInChildren<Transform>().
               // Find("leftSnapPoint").Find("rightSnapPoint").gameObject;

            //Display New Piece
            
            newBossPiece.SetActive(true);
            aMove.addToActive(newBossPiece);
            lastPiece = bossPiece;
            //lastLeftSnapPoint = newLeftSnapPoint;
            //lastRightSnapPoint = snapPoint;
            Debug.Log("Last Boss Right Snap Pos X After: " + lastRightSnapPoint.transform.position.x);
            Debug.Log("Last Boss Right Snap Pos Y After: " + lastRightSnapPoint.transform.position.y);
            
        }

        return true;

    }

    public bool newBiome()
    {

        //Later will be = Random.Range(1, 7)
        nextBiome = 0;

        /*
         * //If We Wish to Not Allow the Same Biome to happen in a Row
         * if(nextBiome == currentBiome){
         * 
         * newBiome();
         * 
         * }else {}
         * 
         

        switch (currentBiome)
        {

            case 0:
                return createTrack(testLevels, testBossLevel);


        }

        return true;

    }

    GameObject choosePiece(List<GameObject> trackList, int itteration)
    {

        GameObject chosenPiece = trackList[Random.Range(0, trackList.Count)];

        if (itteration < trackList.Count)
        {

            if (!chosenPiece.activeSelf)
            {

                return chosenPiece;

            }
            else
            {

                choosePiece(trackList, itteration++);

            }

        } else
        {

            return null;

        }

        return null;

    }

    public int getCurrentBiome()
    {

        return currentBiome;

    }

}*/