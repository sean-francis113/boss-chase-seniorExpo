using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackPiece : MonoBehaviour {

    public float despawnXPosition = 0;
    public Transform snapLocation;
    public bool spawnedNextSegment = false;
    public List<GameObject> spawnPoints;

    private trackConstructor constructor = trackConstructor.instance;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (constructor.GetCanMove())
        {

            transform.Translate(Vector3.left * constructor.moveSpeed * Time.deltaTime);

        }

        if (transform.position.x <= constructor.spawnTriggerX && !spawnedNextSegment)
        {

            constructor.SpawnRandomSegment();
            spawnedNextSegment = true;

        }

        if (transform.position.x <= constructor.minimumXPosition)
        {

            /*
            for(int i = 0; i < spawnPoints.Count; i++)
            {

                spawnEnemies.instance.RemovePoint(spawnPoints[i]);

            }
            */

            constructor.ReturnToPool(this);

        }

    }

    public void Nullify()
    {

        spawnedNextSegment = false;

    }

}
