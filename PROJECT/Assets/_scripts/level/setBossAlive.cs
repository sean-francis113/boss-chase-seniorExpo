using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBossAlive : MonoBehaviour {

    public GameObject gameManager;

    private spawnEnemies spawn = spawnEnemies.instance;
    private trackConstructor constructor = trackConstructor.instance;

	// Use this for initialization
	void Awake () {



	}

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            if (!spawn.GetBossSpawned() && !constructor.GetBuffer())
            {

                Debug.Log("Boss Spawn Triggered");

                spawn.SetBossSpawned(true);
                spawn.SpawnBoss();

            }

        }

    }

}
