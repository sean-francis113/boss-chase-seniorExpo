using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mAnimController : MonoBehaviour {

    private Animator anim;
    private minion minion;

    // Use this for initialization
    void Start()
    {

        minion = GetComponent<minion>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        /*
         * Check Move State
        */
        if (minion.GetMoveState() == MinionMoveState.RUNNING)
        {

            ClearMove();
            anim.SetBool("running", true);

            if (minion.aim.GetFacingRight())
            {

                anim.SetFloat("direction", 1.0f);

            }
            else
            {

                anim.SetFloat("direction", -1.0f);

            }

        }
        else if (minion.GetMoveState() == MinionMoveState.SLIDING)
        {

            ClearMove();
            anim.SetBool("sliding", true);

        }
        else if (minion.GetMoveState() == MinionMoveState.MIDAIR)
        {

            ClearMove();
            anim.SetBool("midair", true);

        }
        else if (minion.GetMoveState() == MinionMoveState.JUMPING)
        {

            ClearMove();
            anim.SetTrigger("jump");

        }
        else if (minion.GetMoveState() == MinionMoveState.DEAD)
        {

            ClearMove();
            anim.SetBool("dead", true);

        }

        /*
         * Check Action State
         */
        if (minion.GetActionState() == MinionActionState.ATTACKING)
        {

            ClearAction();
            anim.SetTrigger("attacking");

        }
        else if (minion.GetActionState() == MinionActionState.DEAD)
        {

            ClearAction();
            anim.SetBool("dead", true);

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

    public void ResetAnimationState()
    {

        ClearMove();
        ClearAction();

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
