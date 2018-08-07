using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionDamage : MonoBehaviour {

    private void Update()
    {
        
        if(this.transform.position.y <= -40)
        {

            this.gameObject.SetActive(false);
            spawnEnemies.instance.ReturnToPool(this.GetComponent<minion>());

        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Triggered!");

        if(collision.tag == "playerWeapon")
        {

            this.gameObject.SetActive(false);
            spawnEnemies.instance.ReturnToPool(this.GetComponent<minion>());

        }

    }

}
