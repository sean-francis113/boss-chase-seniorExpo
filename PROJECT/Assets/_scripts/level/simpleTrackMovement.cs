using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleTrackMovement : MonoBehaviour {

    public float moveSpeed;

    public DIRECTION direction;

	// Update is called once per frame
	void Update () {

        if (direction == DIRECTION.LEFT)
        {

            this.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.World);

        }
        else if(direction == DIRECTION.DOWN)
        {

            this.transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);

        }

	}
}
