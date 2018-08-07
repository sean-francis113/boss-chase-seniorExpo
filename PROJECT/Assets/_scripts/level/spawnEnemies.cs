using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour {

    [Header("Testing Overrides")]
    public bool BOSS_SPAWN_OVERRIDE; //If True, No Boss Can Spawn
    public bool MINION_SPAWN_OVERRIDE; //If True, No Minions Can Spawn

    /// <summary>
    /// Static Instance of this Script to 
    /// Inforce Singleton
    /// </summary>
    public static spawnEnemies instance;

    /// <summary>
    /// The Number to Reset the Spawn Timer to
    /// </summary>
    public float spawnTimerBase;

    /// <summary>
    /// The List of Minions that Can Be Spawned
    /// in the Test Biome
    /// </summary>
    public List<minion> testMinions = new List<minion>();

    /// <summary>
    /// The Boss that Can Be Spawned in the
    /// Test Biome
    /// </summary>
    public boss testBoss;

    /// <summary>
    /// The List of Minions that Can Be Spawned
    /// in the Grass Biome
    /// </summary>
    public List<minion> grassMinions = new List<minion>();

    /// <summary>
    /// The Boss that Can Be Spawned in the
    /// Grass Biome
    /// </summary>
    public boss grassBoss;

    /// <summary>
    /// The List of Minions that Can Be Spawned
    /// in the City of Metal Biome
    /// </summary>
    public List<minion> metalMinions = new List<minion>();

    /// <summary>
    /// The Boss that Can Be Spawned in the
    /// City of Metal Biome
    /// </summary>
    public boss metalBoss;

    /// <summary>
    /// The List of Minions that Can Be Spawned
    /// in the Molten Fields Biome
    /// </summary>
    public List<minion> heatMinions = new List<minion>();

    /// <summary>
    /// The Boss that Can Be Spawned in the
    /// Molten Fields Biome
    /// </summary>
    public boss heatBoss;

    /// <summary>
    /// The Percentage Chance (1 - 100) of 
    /// Enemies Spawning Per Spawn Timer Hit
    /// </summary>
    public int spawnChance;

    public List<Transform> spawnPointList;

    /// <summary>
    /// Are Minions Allowed to be Spawned Yet?
    /// </summary>
    [SerializeField]
    private bool canSpawnMinions;

    /// <summary>
    /// The List of Test Biome Minions That Have Been Initialized
    /// </summary>
    private List<minion> i_testMinions = new List<minion>();

    /// <summary>
    /// The Initialized Test Biome Boss
    /// </summary>
    private boss i_testBoss;

    /// <summary>
    /// The List of Test Biome Minions That Have Been Initialized
    /// </summary>
    private List<minion> i_grassMinions = new List<minion>();

    /// <summary>
    /// The Initialized Test Biome Boss
    /// </summary>
    private boss i_grassBoss;

    /// <summary>
    /// The Pool that Minions Can Be Spawned From
    /// </summary>
    [SerializeField]
    private List<minion> activePool = new List<minion>();

    /// <summary>
    /// The Current Active Minions in the Scene
    /// </summary>
    [SerializeField]
    private List<minion> activeMinions = new List<minion>();

    /// <summary>
    /// The Boos to Be Spawned When the Player Reaches
    /// the Boss Point
    /// </summary>
    private boss curBoss;

    /// <summary>
    /// The Spawn Timer to Countdown Until the Next
    /// Enemy Spawn
    /// </summary>
    private float spawnTimer;

    private void Awake()
    {

        if (!instance)
        {

            instance = this;

        } else
        {

            Destroy(this);

        }

        spawnTimer = spawnTimerBase;

    }

    // Update is called once per frame
    void Update()
    {

        if (canSpawnMinions)
        {

            CountDown();

            if(spawnTimer <= 0.0f)
            {

                float chance = Random.Range(0, 100);

                if(chance <= spawnChance)
                {

                    //List<GameObject> activeSpawnPoints = 
                        //trackConstructor.instance.GetSpawnPiece().spawnPoints;

                    SpawnEnemy(
                        ChooseEnemy(activePool)
                        );

                }

                spawnTimer = spawnTimerBase;

            }

        }

    }

    public void Initialize()
    {

        //instantiating the object pools into your scene.
        //You can 'load' these pools in during gameplay to change the availability of different tile sets. 
        for (int i = 0; i < testMinions.Count; i++)
        {

            minion newMinion = Instantiate(testMinions[i]);
            newMinion.gameObject.name = "Test Minion (" + i + ")";
            i_testMinions.Add(newMinion);
            newMinion.gameObject.SetActive(false);

        }

        {

            boss newBoss = Instantiate(testBoss);
            newBoss.gameObject.name = "Test Boss";
            i_testBoss = newBoss;
            newBoss.gameObject.SetActive(false);

        }

        //instantiating the object pools into your scene.
        //You can 'load' these pools in during gameplay to change the availability of different tile sets. 
        for (int i = 0; i < grassMinions.Count; i++)
        {

            minion newMinion = Instantiate(grassMinions[i]);
            newMinion.gameObject.name = "Grass Minion (" + i + ")";
            i_grassMinions.Add(newMinion);
            newMinion.gameObject.SetActive(false);

        }

        {

            boss newBoss = Instantiate(grassBoss);
            newBoss.gameObject.name = "Grass Boss";
            i_grassBoss = newBoss;
            newBoss.gameObject.SetActive(false);

        }

    }

    /// <summary>
    /// Countdown All of the Timers.
    /// </summary>
    private void CountDown()
    {

        spawnTimer -= Time.deltaTime;

    }

    private minion ChooseEnemy(List<minion> enemyList)
    {

        if (enemyList.Count > 0)
        {

            int index = Random.Range(0, enemyList.Count - 1);

            return enemyList[index];

        }
        else if(enemyList.Count <= 0)
        {

            minion enemy;

            switch (biome.GetCurrentBiome())
            {

                case BIOME.TEST:
                    enemy = Instantiate(testMinions[Random.Range(0, i_testMinions.Count - 1)]);
                    enemy.name = "Test Minion (Backup)";
                    i_testMinions.Add(enemy);
                    enemy.gameObject.SetActive(false);
                    return enemy;
                case BIOME.GRASS:
                    enemy = Instantiate(grassMinions[Random.Range(0, i_grassMinions.Count - 1)]);
                    enemy.name = "Grass Minion (Backup)";
                    i_grassMinions.Add(enemy);
                    enemy.gameObject.SetActive(false);
                    return enemy;

            }

        }

        return null;

    }

    /// <summary>
    /// Spawns the Passed in Enemy into the Scene, Checking to Make Sure
    /// Nothing is Null
    /// </summary>
    /// <param name="enemy">The Enemy to be Spawned In</param>
    private void SpawnEnemy(minion enemy)
    {

        if (!MINION_SPAWN_OVERRIDE)
        {

            if (!enemy)
            {

                return;

            }

            Vector3 spawnLoc = Camera.main.ScreenToWorldPoint(
                new Vector3(
                    Random.Range(Screen.width * 0.2f, Screen.width * 0.8f),
                    Screen.height * 0.8f,
                    10));

            activeMinions.Add(enemy);

            if (activePool.Contains(enemy))
            {

                activePool.Remove(enemy);

            }

            enemy.gameObject.transform.position =
                    spawnLoc;

            enemy.gameObject.SetActive(true);

        }

    }

    public void SpawnBoss()
    {

        if (!BOSS_SPAWN_OVERRIDE)
        {

            menuManager.instance.ShowBossHealthUI();

            audioManager.instance.CrossFade(MUSIC.BOSS_BG);

            Vector3 spawnLoc = Camera.main.ScreenToWorldPoint(
                new Vector3(
                    Random.Range(Screen.width * 0.2f, Screen.width * 0.8f),
                    Screen.height * 0.8f,
                    10));

            canSpawnMinions = false;
            curBoss.gameObject.transform.position =
                spawnLoc;

            curBoss.gameObject.SetActive(true);

        }

    }

    /// <summary>
    /// clears the Active Pool and Active Minion lists, and copies in elements from one of your 
    /// </summary>
    /// <param name="pool"></param>
    /// <returns></returns>
    public List<minion> LoadPool(List<minion> pool)
    {

        activePool.Clear();
        activeMinions.Clear();
        return new List<minion>(pool); //shallow copy of the object pool.

    }

    /// <summary>
    /// Will return the object to the current pool if it is considered an 'active' minion.
    /// </summary>
    /// <param name="minion"></param>
    public void ReturnToPool(minion minion)
    {

        if (activeMinions.Contains(minion))
        {

            activePool.Add(minion);

        }

        activeMinions.Remove(minion);

        minion.gameObject.SetActive(false);

    }

    public void Nullify()
    {

        canSpawnMinions = false;

    }

    public void ResetEnemies()
    {

        activeMinions.Clear();
        activePool.Clear();

    }

    public void ChangeBiome()
    {

        switch (biome.GetCurrentBiome())
        {

            case BIOME.TEST:
                activePool = LoadPool(i_testMinions);
                curBoss = i_testBoss;
                break;
            case BIOME.GRASS:
                activePool = LoadPool(i_grassMinions);
                curBoss = i_grassBoss;
                break;

        }

        curBoss.spawned = false;
        canSpawnMinions = true;

    }


    /***********Set Functions***********/

    /// <summary>
    /// Sets Whether Minions can Currently be Spawned
    /// </summary>
    /// <param name="set">The Boolean to Set canSpawnMinions to</param>
    public void SetCanSpawnMinions(bool set)
    {
        if (!curBoss.spawned)
        {

            canSpawnMinions = set;

        }

    }

    public void SetBossSpawned(bool set)
    {

        curBoss.spawned = set;

    }


    /***********Get Functions***********/

    /// <summary>
    /// Returns Whether Minions can Currently be Spawned
    /// </summary>
    /// <returns>If Minions can be Spawned</returns>
    public bool GetCanSpawnMinions()
    {

        return canSpawnMinions;

    }

    public bool GetBossSpawned()
    {

        return curBoss.spawned;

    }

    public bool GetBossKilled()
    {

        return curBoss.spawned;

    }

    public boss GetCurrentBoss()
    {

        return curBoss;

    }

}