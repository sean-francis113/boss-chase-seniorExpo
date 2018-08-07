using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMinionMovement : MonoBehaviour {

    public float moveSpeed;
	
	// Update is called once per frame
	void Update () {

        if(this.transform.position.x > 0)
        {

            this.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        }
        else
        {

            this.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        }
		
	}
}
