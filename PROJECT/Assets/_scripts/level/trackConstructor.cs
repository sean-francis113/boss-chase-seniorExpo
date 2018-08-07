using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackConstructor : MonoBehaviour {

    public static trackConstructor instance; //singleton reference!

    [Header("Testing Overrides")]
    public bool BIOMESPAWN_OVERRIDE; //If True, Overrides Changing Biomes to biomeToOverride
    public BIOME biomeToOverride; //Biome to Change to When BIOMESPAWN_OVERRIDE is True
    public bool BIOMELENGTH_OVERRIDE; //If True, normalSpawned Will Never Increase, Which Means Boss Pieces will Never Spawn

    [Header("Test Biome Pieces")]
    //Prefabs put into the public lists below will have copies instantiated into the scene.
    //Drag in prefabs directly from your assets.
    public List<trackPiece> testNormalPieces;
    public List<trackPiece> testBossPieces;
    public trackPiece testBossStart;

    [Header("Grasslands Biome Pieces")]
    public List<trackPiece> grassNormalPieces;
    public List<trackPiece> grassBossPieces;
    public trackPiece grassBossStart;

    [Header("City of Metal Biome Pieces")]
    public List<trackPiece> metalNormalPieces;
    public List<trackPiece> metalBossPieces;
    public trackPiece metalBossStart;

    [Header("Molten Fields Biome Pieces")]
    public List<trackPiece> heatNormalPieces;
    public List<trackPiece> heatBossPieces;
    public trackPiece heatBossStart;

    [Header("Biome Length Settings")]
    /// <summary>
    /// How Many Segments to Spawn Before the Boss Pieces Spawn
    /// </summary>
    public int biomeLength;

    [Header("Boss Buffer Length Settings")]
    /// <summary>
    /// How Many Segments Do We Want to Buffer
    /// With Before Switching Biomes?
    /// </summary>
    public int bufferLength;

    //The below lists are the pools of INSTANTIATED objects added to your scene.
    //When loaded, a SHALLOW COPY is made into the Active Pool. Do not remove elements from these lists.
    List<trackPiece> i_testNormalPool = new List<trackPiece>();
    List<trackPiece> i_testBossPool = new List<trackPiece>();
    trackPiece i_testBossStart;

    List<trackPiece> i_grassNormalPool = new List<trackPiece>();
    List<trackPiece> i_grassBossPool = new List<trackPiece>();
    trackPiece i_grassBossStart;

    List<trackPiece> i_metalNormalPool = new List<trackPiece>();
    List<trackPiece> i_metalBossPool = new List<trackPiece>();
    trackPiece i_metalBossStart;

    List<trackPiece> i_heatNormalPool = new List<trackPiece>();
    List<trackPiece> i_heatBossPool = new List<trackPiece>();
    trackPiece i_heatBossStart;

    [Header("Spawn Settings")]
    public float minimumXPosition = -20;
    public Vector3 spawnPosition;
    public float moveSpeed = 5;
    public float spawnTriggerX = 0;

    public Vector3 initialSpawnPosition = Vector3.zero;

    [Header("Active Pools")]
    public trackPiece LastSpawnedSegment;
    public trackPiece spawnSegment;
    public List<trackPiece> activePool;
    public List<trackPiece> activeSegments;

    Coroutine spawnLoop;

    [SerializeField]
    private int normalSpawned;
    [SerializeField]
    private int bufferSpawned;

    /// <summary>
    /// Do we need a buffer of segments before
    /// Switching Biomes?
    /// </summary>
    [SerializeField]
    private bool buffer;

    private trackPiece newSegment;

    private bool canMove;

    private void Awake()
    {
        //enforce singleton
        if (!instance)
        {

            instance = this;

        }else {

            Destroy(this);

        }

    }

    public void Initialize()
    {

        //instantiating the object pools into your scene.
        //You can 'load' these pools in during gameplay to change the availability of different tile sets. 

        /*
         * TEST BIOME
         */
        for (int i = 0; i < testNormalPieces.Count; i++)
        {

            trackPiece newSegment = Instantiate(testNormalPieces[i]);
            newSegment.gameObject.name = "Test Normal Segment (" + i + ")";
            i_testNormalPool.Add(newSegment);
            newSegment.gameObject.SetActive(false);


        }

        for (int i = 0; i < testBossPieces.Count; i++)
        {

            trackPiece newSegment = Instantiate(testBossPieces[i]);
            newSegment.gameObject.name = "Test Boss Segment (" + i + ")";
            i_testBossPool.Add(newSegment);
            newSegment.gameObject.SetActive(false);

        }

        {

            trackPiece newSegment = Instantiate(testBossStart);
            newSegment.gameObject.name = "Test Boss Segment (Start)";
            i_testBossStart = newSegment;
            newSegment.gameObject.SetActive(false);

        }

        /*
         * GRASS BIOME
         */
        for (int i = 0; i < grassNormalPieces.Count; i++)
        {

            trackPiece newSegment = Instantiate(grassNormalPieces[i]);
            newSegment.gameObject.name = "Grass Normal Segment (" + i + ")";
            i_grassNormalPool.Add(newSegment);
            newSegment.gameObject.SetActive(false);


        }
        for (int i = 0; i < grassBossPieces.Count; i++)
        {

            trackPiece newSegment = Instantiate(grassBossPieces[i]);
            newSegment.gameObject.name = "Grass Boss Segment (" + i + ")";
            i_grassBossPool.Add(newSegment);
            newSegment.gameObject.SetActive(false);

        }

        {

            trackPiece newSegment = Instantiate(grassBossStart);
            newSegment.gameObject.name = "Grass Boss Segment (Start)";
            i_grassBossStart = newSegment;
            newSegment.gameObject.SetActive(false);

        }

    }

    public void ChangeBiome()
    {

        audioManager.instance.CrossFade(MUSIC.CHANGING_BIOME);

        if (BIOMESPAWN_OVERRIDE)
        {
         
            biome.SetCurrentBiome(biomeToOverride);

        }else
        {

            switch(Random.Range(1, biome.GetNumberofBiomes()))
            {

                case 0:
                    biome.SetCurrentBiome(BIOME.TEST);
                    break;
                case 1:
                    biome.SetCurrentBiome(BIOME.GRASS);
                    break;
                case 2:
                    biome.SetCurrentBiome(BIOME.METAL);
                    break;
                case 3:
                    biome.SetCurrentBiome(BIOME.HEAT);
                    break;
                

            }

        }

        switch (biome.GetCurrentBiome())
        {

            case BIOME.TEST:
                activePool = LoadPool(i_testNormalPool);

                //CROSSFADE BACKGROUND

                //CROSSFADE MUSIC/SOUNDS

                break;

            case BIOME.GRASS:
                activePool = LoadPool(i_grassNormalPool);

                //Change Music and Sounds

                //CROSSFADE BACKGROUND

                //CROSSFADE MUSIC/SOUNDS
                audioManager.instance.CrossFade(MUSIC.GEN_BG);

                break;

        }

    }

    /// <summary>
    /// clears the Active Pool and Active Segment lists, and copies in elements from one of your 
    /// </summary>
    /// <param name="pool"></param>
    /// <returns></returns>
    public List<trackPiece> LoadPool(List<trackPiece> pool)
    {

        activePool.Clear();
        activeSegments.Clear();
        return new List<trackPiece>(pool); //shallow copy of the object pool.

    }

    public void SpawnRandomSegment()
    {

        //Check for Empty Pool
        if (activePool.Count <= 0)
        {

            Debug.LogWarning("Tried to spawn from an empty pool! [" + activePool.ToString() + "]");

            Debug.LogWarning("Pulling New Piece to Spawn");

            switch(biome.GetCurrentBiome())
            {

                case BIOME.TEST:
                    if(spawnEnemies.instance.GetBossSpawned())
                    {

                        trackPiece newPiece = Instantiate(testBossPieces[0]);
                        newPiece.gameObject.SetActive(false);
                        newPiece.name = "Test Boss Piece (Backup)";
                        activePool.Add(newPiece);
                        i_testBossPool.Add(newPiece);

                    }
                    else
                    {

                        trackPiece newPiece = Instantiate(testNormalPieces[0]);
                        newPiece.gameObject.SetActive(false);
                        newPiece.name = "Test Normal Piece (Backup)";
                        activePool.Add(newPiece);
                        i_testNormalPool.Add(newPiece);

                    }
                    break;
                case BIOME.GRASS:
                    if (spawnEnemies.instance.GetBossSpawned())
                    {

                        trackPiece newPiece = Instantiate(grassBossPieces[0]);
                        newPiece.gameObject.SetActive(false);
                        newPiece.name = "Grass Boss Piece (Backup)";
                        activePool.Add(newPiece);
                        i_grassBossPool.Add(newPiece);

                    }
                    else
                    {

                        trackPiece newPiece = Instantiate(grassNormalPieces[Random.Range(0, grassNormalPieces.Count - 1)]);
                        newPiece.gameObject.SetActive(false);
                        newPiece.name = "Grass Normal Piece (Backup)";
                        activePool.Add(newPiece);
                        i_grassNormalPool.Add(newPiece);

                    }
                    break;

            }

            //return;

        }

        //Various Checks for Tracking if Biome Length
        //Has Been Reached, If Buffer Length has Been
        //Reached and if Boss Start Segment Needs to 
        //Spawn
        if (buffer && bufferSpawned < bufferLength)
        {

            bufferSpawned++;

        }
        else if (buffer && bufferSpawned >= bufferLength)
        {

            buffer = false;
            bufferSpawned = 0;

            ChangeBiome();
            spawnEnemies.instance.ChangeBiome();

        }
        else
        {

            if (!spawnEnemies.instance.GetBossSpawned())
            {

                if (!BIOMELENGTH_OVERRIDE)
                {

                    normalSpawned++;

                }

                if (normalSpawned > biomeLength)
                {

                    switch (biome.GetCurrentBiome())
                    {

                        case BIOME.TEST:
                            activePool = LoadPool(i_testBossPool);
                            spawnEnemies.instance.SetBossSpawned(false);
                            break;
                        case BIOME.GRASS:
                            activePool = LoadPool(i_grassBossPool);
                            //spawnEnemies.instance.SetBossSpawned(false);
                            break;

                    }

                    normalSpawned = -1;

                }

            }

        }

        if (normalSpawned == -1)
        {

            switch (biome.GetCurrentBiome())
            {

                case BIOME.TEST:
                    newSegment = i_testBossStart;
                    break;

                case BIOME.GRASS:
                    newSegment = i_grassBossStart;
                    break;

            }

            normalSpawned = 0;

        }
        else
        {

            newSegment = activePool[Random.Range(0, activePool.Count)];
            activeSegments.Add(newSegment);
            activePool.Remove(newSegment);

        }
        
        /*
        for (int i = 0; i < newSegment.spawnPoints.Count; i++)
        {

            spawnEnemies.instance.AddPoint(newSegment.spawnPoints[i]);

        }
        */

        if (LastSpawnedSegment != null)
        {

            newSegment.transform.position = LastSpawnedSegment.snapLocation.position;

        }
        else
        {

            newSegment.transform.position = initialSpawnPosition;

        }

        newSegment.gameObject.SetActive(true);
        spawnSegment = LastSpawnedSegment;
        LastSpawnedSegment = newSegment;

    }


    /// <summary>
    /// Will return the object to the current pool if it is considered an 'active' segment.
    /// </summary>
    /// <param name="segment"></param>
    public void ReturnToPool(trackPiece segment)
    {

        if (activeSegments.Contains(segment))
        {

            activePool.Add(segment);

        }

        activeSegments.Remove(segment);

        segment.gameObject.SetActive(false);
        segment.transform.position = spawnPosition;
        segment.spawnedNextSegment = false;

    }

    public void Nullify()
    {

        LastSpawnedSegment = null;
        spawnSegment = null;

        normalSpawned = 0;
        bufferSpawned = 0;

    }

    public void ResetTracks()
    {

        activePool.Clear();
        activeSegments.Clear();

    }

    public bool GetBuffer()
    {

        return buffer;

    }

    public int GetBufferSpawned()
    {

        return bufferSpawned;

    }

    public trackPiece GetLastSpawnedPiece()
    {

        return LastSpawnedSegment;

    }

    public trackPiece GetSpawnPiece()
    {

        return spawnSegment;

    }

    public bool GetCanMove()
    {

        return canMove;

    }

    public void SetBuffer(bool set)
    {

        buffer = set;

    }

    public void SetBufferSpawned(int set)
    {

        bufferSpawned = set;

    }

    public void SetCanMove(bool set)
    {

        canMove = set;
        
    }

}