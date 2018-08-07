using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour {

    public GameObject[] spawnPoints;

    private float upperThreshold = Screen.height * 0.6f; //60% of Screen Height
    private float lowerThreshold = Screen.height * 0.2f; // 20% of Screen Height

    private GameObject player;

    private float smoothTime = 0.15f;

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update () {

        if(!player)
        {

            return;

        }
        else
        {

            Vector3 playerPos = Camera.main.WorldToScreenPoint(player.transform.position);

            if(playerPos.y < lowerThreshold)
            {

                /*
                transform.position = Vector3.SmoothDamp(transform.position, 
                    new Vector3(this.transform.position.x, 
                        this.transform.position.y - 5, 
                        this.transform.position.z), 
                    ref velocity, 
                    smoothTime);
                */

                /*
                if(spawnPoints.Length > 0)
                {

                    for(int i = 0; i < spawnPoints.Length; i++)
                    {

                        spawnPoints[i].transform.Translate(new Vector3(
                            spawnPoints[i].transform.position.x, 
                            spawnPoints[i].transform.position.y - 2, 
                            spawnPoints[i].transform.position.z));

                    }

                }
                */

            }
            else if(playerPos.y > upperThreshold)
            {

                transform.position = Vector3.SmoothDamp(transform.position, 
                    new Vector3(this.transform.position.x, 
                        this.transform.position.y + 5, 
                        this.transform.position.z), 
                    ref velocity, 
                    smoothTime);

                /*
                if (spawnPoints.Length > 0)
                {

                    for (int i = 0; i < spawnPoints.Length; i++)
                    {

                        spawnPoints[i].transform.Translate(new Vector3(
                            spawnPoints[i].transform.position.x,
                            spawnPoints[i].transform.position.y + 1,
                            spawnPoints[i].transform.position.z));

                    }

                }
                */

            }

        }

    }

    public void GetPlayer()
    {

        player = GameObject.FindGameObjectWithTag("Player");

    }

}
