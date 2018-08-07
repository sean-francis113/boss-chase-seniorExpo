using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pTorsoAim : MonoBehaviour {

    private player player;

    private void Awake()
    {

        player = GetComponent<player>();

    }

    // Update is called once per frame
    void Update () {

        if (player.GetMoveState() != PlayerMoveState.DEAD ||
            pauseState.instance.GetPauseState() != PAUSESTATE.PAUSED)
        {

            //Aim player at mouse
            //which direction is up
            Vector3 upAxis = new Vector3(0, 0, 1);
            Vector3 mouseScreenPosition = Input.mousePosition;

            //set mouses z to your targets
            mouseScreenPosition.z = transform.position.z;

            Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            if ((mouseWorldSpace.x - this.transform.position.x) < -1 || (mouseWorldSpace.x - this.transform.position.x) > 1)
            {

                transform.LookAt(mouseWorldSpace, upAxis);

                if ((mouseWorldSpace.x - this.transform.position.x) < -1)
                {

                    //player.facingRight = false;

                    //zero out all rotations except the axis I want
                    if (player.GetMoveState() != PlayerMoveState.SLIDING)
                    {

                        this.transform.eulerAngles = new Vector3(0, 0, (-transform.eulerAngles.z + 270) / 2);

                    }
                    else if (player.GetMoveState() == PlayerMoveState.SLIDING)
                    {

                        this.transform.eulerAngles = new Vector3(0, 0, (-transform.eulerAngles.z + 270) / 3);

                    }

                }
                else if ((mouseWorldSpace.x - this.transform.position.x) > 1)
                {

                    //player.facingRight = true;

                    //zero out all rotations except the axis I want
                    if (player.GetMoveState() != PlayerMoveState.SLIDING)
                    {

                        this.transform.eulerAngles = new Vector3(0, 0, (-transform.eulerAngles.z + 90) / 2);

                    }
                    else if (player.GetMoveState() == PlayerMoveState.SLIDING)
                    {

                        this.transform.eulerAngles = new Vector3(0, 0, (-transform.eulerAngles.z + 90) / 3);

                    }

                }

            }

        }

    }

    public player GetPlayer()
    {

        return player;

    }

}
