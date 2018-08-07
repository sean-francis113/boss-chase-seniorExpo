using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "enemy" && 
            collision.gameObject.GetComponent<minion> ())
        {

            minion enemy = collision.gameObject.GetComponent<minion>();

            enemy.ResetSelf();
            spawnEnemies.instance.ReturnToPool(enemy);

        }

        if(collision.tag == "enemyProjectile")
        {

            Destroy(collision.gameObject);

        }

    }

}
