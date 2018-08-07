using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiUpdate : MonoBehaviour {

    public Text pHealth;
    public Text bHealth;

    private player player;
    private boss curBoss;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (this.gameObject.activeSelf)
        {

            if (!player)
            {

                player = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();

            }

            pHealth.text = player.health.ToString();

            if(menuManager.instance.bossHealthSlider.IsActive())
            {

                curBoss = spawnEnemies.instance.GetCurrentBoss();

                menuManager.instance.bossHealthSlider.value = curBoss.health;

            }

        }

	}

}
