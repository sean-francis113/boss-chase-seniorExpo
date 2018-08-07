using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pOrient : MonoBehaviour {

    public float rotationSpeed;

    RaycastHit2D[] hit;
    RaycastHit2D[] leftOrientHit;
    RaycastHit2D[] rightOrientHit;

    bool leftGrounded;
    bool rightGrounded;

    player player;

    private void Start()
    {

        player = GetComponent<player>();

    }

    // Update is called once per frame
    void Update () {

        CheckGrounding();
        CheckOrientation();

    }

    void CheckGrounding()
    {

        hit = Physics2D.RaycastAll(
            new Vector2(
                this.transform.position.x,
                this.transform.position.y - 3f
                ),
            Vector2.down,
            0.5f
            );

        for (int i = 0; i < hit.Length; i++)
        {

            if (hit[i].collider.tag == "ground")
            {

                player.SetGrounded(true);
                return;

            }


        }

        player.SetGrounded(false);
        return;

    }

    void CheckOrientation()
    {

        leftOrientHit = Physics2D.RaycastAll(
            new Vector2(
                this.transform.position.x - 1f,
                this.transform.position.y - 3f
                ),
            Vector2.down,
            0.5f
            );

        rightOrientHit = Physics2D.RaycastAll(
            new Vector2(
                this.transform.position.x + 1f,
                this.transform.position.y - 3f
                ),
            Vector2.down,
            0.5f
            );

        for(int i = 0; i < leftOrientHit.Length; i++)
        {

            if(leftOrientHit[i].collider.tag == "ground")
            {

                leftGrounded = true;
                break;

            }

            leftGrounded = false;

        }

        for(int i = 0; i < rightOrientHit.Length; i++)
        {

            if (rightOrientHit[i].collider.tag == "ground")
            {

                rightGrounded = true;
                break;

            }


            rightGrounded = false;

        }

        if(!leftGrounded && player.GetGrounded())
        {

            this.transform.Rotate(Vector2.left * rotationSpeed);

        }
        else if(!rightGrounded && player.GetGrounded())
        {

            this.transform.Rotate(Vector2.right * rotationSpeed);

        }

    }

}
