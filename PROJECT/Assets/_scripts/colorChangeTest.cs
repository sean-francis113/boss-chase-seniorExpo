using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChangeTest : MonoBehaviour {

    public Anima2D.SpriteMeshInstance mesh;
    public SpriteRenderer sprite;

	// Use this for initialization
	void Start () {

        mesh = GetComponent<Anima2D.SpriteMeshInstance>();
        sprite = GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {


        if(Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (mesh)
            {

                Color rand = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                mesh.color = rand;

            }
            else if(sprite)
            {

                Color rand = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                sprite.color = rand;

            }

        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {

            if (mesh)
            {

                Color temp = mesh.color;
                temp.a = Random.value;
                mesh.color = temp;

            }
            else if(sprite)
            {

                Color temp = sprite.color;
                temp.a = Random.value;
                sprite.color = temp;

            }

        }
		
	}
}
