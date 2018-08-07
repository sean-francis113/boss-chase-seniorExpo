using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charBlock : MonoBehaviour {

    [Header("Reference Script - Only Choose One")]
    public player player;
    public minion minion;
    public boss boss;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "enemyWeapon" ||
            collision.tag == "enemyProjectile" ||
            collision.tag == "splashWeapon" ||
            collision.tag == "playerWeapon" ||
            collision.tag == "playerProjectile")
        {

            if(player)
            {

                player.blocked = true;

            }
            else if(minion)
            {

                minion.blocked = true;

            }
            else if(boss)
            {

                boss.blocked = true;

            }

        }

    }

}
