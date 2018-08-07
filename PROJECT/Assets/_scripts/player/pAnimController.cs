using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pAnimController : MonoBehaviour {

    private Animator anim;
    private player player;

	// Use this for initialization
	void Start () {

        player = GetComponent<player>();
        anim = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {

        /*
         * Check Move State
         */
        if(player.GetMoveState() == PlayerMoveState.RUNNING)
        {

            ClearMove();
            anim.SetBool("running", true);

            if(player.aim.GetFacingRight())
            {

                anim.SetFloat("direction", 1.0f);

            }
            else
            {

                anim.SetFloat("direction", -1.0f);

            }

        }
        else if(player.GetMoveState() == PlayerMoveState.SLIDING)
        {

            ClearMove();
            anim.SetBool("sliding", true);

        }
        else if(player.GetMoveState() == PlayerMoveState.MIDAIR)
        {

            ClearMove();
            anim.SetBool("midair", true);

        }
        else if(player.GetMoveState() == PlayerMoveState.JUMPING)
        {

            ClearMove();
            //anim.SetTrigger("jump");

        }
        else if(player.GetMoveState() == PlayerMoveState.DEAD)
        {

            ClearMove();
            anim.SetBool("dead", true);

        }

        /*
         * Check Action State
         */
        if(player.GetActionState() == PlayerActionState.ATTACKING)
        {

            ClearAction();
            anim.SetTrigger("attacking");

        }
        else if(player.GetActionState() == PlayerActionState.DODGING)
        {

            ClearAction();
            anim.SetBool("dodge", true);

        }
        else if(player.GetActionState() == PlayerActionState.DEAD)
        {

            ClearAction();
            anim.SetBool("dead", true);

        }
        else
        {

            ClearAction();

        }
		
	}

    void ClearMove()
    {

        anim.SetBool("running", false);
        anim.SetBool("sliding", false);
        anim.SetBool("midair", false);
        anim.SetBool("dead", false);

    }

    void ClearAction()
    {

        anim.ResetTrigger("attacking");
        anim.SetBool("dodge", false);
        anim.SetBool("dead", false);

    }

    public void PauseAnimations()
    {

        anim.speed = 0;

    }

    public void ContinueAnimations()
    {

        anim.speed = 1;

    }

    void SyncAnimations()
    {

        //CURRENTLY UNUSED

    }

}
