using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegment : MonoBehaviour {

    public float despawnXPosition = 0;
    public Transform snapLocation;
    public bool spawnedNextSegment = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.left * LevelConstructor.instance.moveSpeed * Time.deltaTime);

        if(transform.position.x <= LevelConstructor.instance.spawnTriggerX && !spawnedNextSegment)
        {
            LevelConstructor.instance.SpawnRandomSegment();
            spawnedNextSegment = true;
        }

		if(transform.position.x <= LevelConstructor.instance.minimumXPosition)
        {
            LevelConstructor.instance.ReturnToPool(this);
        }
	}
}
