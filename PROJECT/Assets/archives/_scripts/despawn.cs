using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawn : MonoBehaviour {

    private autoMove aMove;

    private void Awake()
    {

        aMove = GameObject.Find("gameManager").GetComponent<autoMove>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Despawn Triggered.");

        if (other.tag == "track")
        {

            if (other.gameObject.activeSelf)
            {

                aMove.removeFromActive(other.gameObject);
                other.gameObject.SetActive(false);

            }

        }

    }

}
