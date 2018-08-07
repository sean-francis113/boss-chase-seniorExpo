using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConstructor : MonoBehaviour {

    public static LevelConstructor instance; //singleton reference!

    [Header("Segment Prefabs")]
    //Prefabs put into the public lists below will have copies instantiated into the scene.
    //Drag in prefabs directly from your assets.
    public List<LevelSegment> normalSegmentPrefabs;
    public List<LevelSegment> bossSegmentPrefabs;

    //The below lists are the pools of INSTANTIATED objects added to your scene.
    //When loaded, a SHALLOW COPY is made into the Active Pool. Do not remove elements from these lists.
    List<LevelSegment> m_normalSegmentPool = new List<LevelSegment>();
    List<LevelSegment> m_bossSegmentPool = new List<LevelSegment>();

    [Header("Spawn Settings")]    
    public float minimumXPosition = -20;
    public Vector3 spawnPosition;
    public float moveSpeed = 5;
    public float spawnTriggerX = 0;

    public Vector3 initialSpawnPosition = Vector3.zero;


    [Header("Active Pools")]
    public LevelSegment LastSpawnedSegment;
    public List<LevelSegment> activePool;
    public List<LevelSegment> activeSegments;

    Coroutine spawnLoop;

    private void Awake()
    {
        //enforce singleton
        if (!instance) instance = this;
        else Destroy(this);
    }

    // Use this for initialization
    void Start () {

        //instantiating the object pools into your scene.
        //You can 'load' these pools in during gameplay to change the availability of different tile sets. 
		for(int i = 0; i < bossSegmentPrefabs.Count; i++)
        {
            LevelSegment newSegment = Instantiate(bossSegmentPrefabs[i]);
            newSegment.gameObject.name = "Boss Segment (" + i + ")";
            m_bossSegmentPool.Add(newSegment);
            newSegment.gameObject.SetActive(false);
        }

        for (int i = 0; i < normalSegmentPrefabs.Count; i++)
        {
            LevelSegment newSegment = Instantiate(normalSegmentPrefabs[i]);
            newSegment.gameObject.name = "Normal Segment (" + i + ")";
            m_normalSegmentPool.Add(newSegment);
            newSegment.gameObject.SetActive(false);

        }

        //This loads the normal segments into the active pool, which will start to spawn automatically.
        activePool = LoadPool(m_normalSegmentPool);
    }
	
	// Update is called once per frame
	void Update () {

    }

    /// <summary>
    /// clears the Active Pool and Active Segment lists, and copies in elements from one of your 
    /// </summary>
    /// <param name="pool"></param>
    /// <returns></returns>
    public List<LevelSegment> LoadPool(List<LevelSegment> pool)
    {
        activePool.Clear();
        activeSegments.Clear();
        return new List<LevelSegment>(pool); //shallow copy of the object pool.
    }

    public void SpawnRandomSegment()
    {
        if(activePool.Count <= 0)
        {
            Debug.LogWarning("Tried to spawn from an empty pool! [" + activePool.ToString() + "]");
            return;
        }

        LevelSegment newSegment = activePool[Random.Range(0, activePool.Count)];
        activeSegments.Add(newSegment);
        activePool.Remove(newSegment);
        if (LastSpawnedSegment != null)
            newSegment.transform.position = LastSpawnedSegment.snapLocation.position;
        else
            newSegment.transform.position = initialSpawnPosition;
        newSegment.gameObject.SetActive(true);
        LastSpawnedSegment = newSegment;
    }


    /// <summary>
    /// Will return the object to the current pool if it is considered an 'active' segment.
    /// </summary>
    /// <param name="segment"></param>
    public void ReturnToPool(LevelSegment segment)
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
}
