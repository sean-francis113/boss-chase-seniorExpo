/*
using System.Collections.Generic;
using UnityEngine;

public class trackManager : MonoBehaviour {

    /// <summary>
    /// The Starting Position of the Track.
    /// Used to Place the First Track Piece of a Run.
    /// </summary>
    public Vector3 startPos;

    /// <summary>
    /// The List of Levels in the Test Biome.
    /// </summary>
    private List<GameObject> testLevels;

    /// <summary>
    /// The List of Boss Levels in the Test Biome.
    /// </summary>
    private List<GameObject> testBossLevels;

    /// <summary>
    /// Integer Representing the Biome that is Currently Active.
    /// 0 - Test Biome
    /// 1 - Grasslands
    /// 2 - City of Metal
    /// 3 - Molten Fields
    /// </summary>
    private int currentBiome;

    private trackPiece lastPiece;
    private maintainRun maintain;

    private void Awake()
    {

        maintain = GetComponent<maintainRun>();

        testLevels = new List<GameObject>();
        testBossLevels = new List<GameObject>();

    }

    /// <summary>
    /// Adds a Passed In Level to the List of Levels According to the 
    /// Associated Biome.
    /// </summary>
    /// <param name="levelToAdd">The Prefab that Contains All the 
    /// Information for the Track Piece.</param>
    /// <param name="biome">The Number that Represents the Biome 
    /// List to Add To.
    /// 0 - Test Biome
    /// 1 - Grasslands
    /// 2 - City of Metal
    /// 3 - Molten Fields</param>
    /// <param name="bossLevel">Boolean that Determines if the 
    /// levelToAdd is a Boss Level Piece or Not.</param>
	public void AddLevel(GameObject levelToAdd, int biome, bool bossLevel)
    {
        /*
        * Checks the Current Biome Level to Determine Which 
        * List to Put the New Level into.
        
        switch (biome)
        {

            case 0:
                /*
                * If the Piece is a Boss Level, Add it to the
                * Boss Level List.
                
                if (bossLevel)
                {

                    testBossLevels.Add(levelToAdd);

                }

                /*
                 * If the Piece is NOT a Boss Level, Add it to
                 * the Normal Level List
                 
                else
                {

                    testLevels.Add(levelToAdd);

                }

                break;

        }

    }

    /// <summary>
    /// Randomly Chooses and Returns a Track Piece to Use.
    /// </summary>
    /// <param name="levelList">The List of Levels to Choose From.</param>
    /// <param name="boss">The Boolean That Represents if the 
    /// Track Needed to Spawn is a Boss Piece</param>
    /// <param name="bossStart">The Boolean That Represents if the
    /// Pieced Needed to Spawn is the Boss Start Piece</param>
    /// <returns>The Randomly Chosen Track Piece.</returns>
    private GameObject ChoosePiece(List<GameObject> levelList, bool boss, bool bossStart)
    {

        if (boss && bossStart)
        {

            return levelList[0];

        }

        else
        {

            int index;

            if (boss)
            {

                index = Random.Range(1, levelList.Count);

            } else
            {

                index = Random.Range(0, levelList.Count);

            }

            return levelList[index];

        }
    }

    /// <summary>
    /// Spawns the Chosen Track Piece into the Scene.
    /// Then Sets lastPiece for Future Use.
    /// </summary>
    /// <param name="boss">If the Track Piece
    /// Needed to Spawn is a Boss Piece.</param>
    public void SpawnPiece(bool boss, bool bossStart)
    {

        switch (currentBiome)
        {

            case 0:
                if (boss)
                {

                    SpawnPiece(
                        ChoosePiece(
                            testBossLevels, 
                            boss,
                            bossStart
                            ));

                }
                else
                {

                    SpawnPiece(
                        ChoosePiece(
                            testLevels, 
                            boss,
                            bossStart
                            ));

                }

                break;

        }

    }

    /// <summary>
    /// Spawns the Chosen Track Piece into the Scene.
    /// Then Sets lastPiece for Future Use.
    /// </summary>
    /// <param name="trackPiece">The Piece to Spawn.</param>
    private void SpawnPiece(GameObject trackPiece)
    {


        if (trackPiece.activeSelf)
        {

            GameObject newPiece = Instantiate(
                trackPiece, 
                new Vector3(0, 7, 0), 
                Quaternion.identity);

            trackPiece = newPiece;

        }

        /*
        * If lastPiece is Null Which Means It is the First Piece
        * to be Set.
        
        if (!lastPiece)
        {

            trackPiece.transform.Translate(
                startPos
                );

        } else
        {

            trackPiece.transform.Translate(
                lastPiece.spawnAt.transform.position
                );

        }

        trackPiece.SetActive(true);
        AddLevel(trackPiece, currentBiome, 
            trackPiece.GetComponent<trackPiece>().bossLevel);
        SetLastPiece(trackPiece.GetComponent<trackPiece>());

    }


    /***********Set Functions**********

    /// <summary>
    /// Sets the Number of the Current Biome.
    /// </summary>
    /// <param name="biome">The Integer of the Biome to Set.</param>
    public void SetCurBiome(int biome)
    {

        currentBiome = biome;

    }

    /// <summary>
    /// Sets the Reference of the Last Track Piece Placed to be Used 
    /// Later.
    /// </summary>
    /// <param name="piece">The Track Piece that Was Just Spawned.</param>
    public void SetLastPiece(trackPiece piece)
    {

        lastPiece = piece;

    }


    /***********Get Functions**********

    /// <summary>
    /// Gets the Number of the Current Biome.
    /// </summary>
    /// <returns>The Integer of the Current Biome.</returns>
    public int GetCurBiome()
    {

        return currentBiome;

    }

    /// <summary>
    /// Gets the Last Track Piece Placed into the Scene.
    /// </summary>
    /// <returns>The trackPiece Reference of the Last Piece Spawned.</returns>
    public trackPiece GetLastPiece()
    {

        return lastPiece;

    }

}
*/