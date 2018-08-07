using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bAnimController : MonoBehaviour {

    private Animator anim;
    private boss boss;

    // Use this for initialization
    void Start()
    {

        boss = GetComponent<boss>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        
        /*
         * Check Move State
         */
        if (boss.GetMoveState() == BossMoveState.RUNNING)
            {

                ClearMove();
                anim.SetBool("running", true);

            if (boss.aim.GetFacingRight())
            {

                anim.SetFloat("direction", 1.0f);

            }
            else
            {

                anim.SetFloat("direction", -1.0f);

            }

        }
            else if (boss.GetMoveState() == BossMoveState.SLIDING)
            {

                ClearMove();
                anim.SetBool("sliding", true);

            }
            else if (boss.GetMoveState() == BossMoveState.MIDAIR)
            {

                ClearMove();
                anim.SetBool("midair", true);

            }
            else if (boss.GetMoveState() == BossMoveState.JUMPING)
            {

                ClearMove();
                anim.SetTrigger("jump");

            }
            else if (boss.GetMoveState() == BossMoveState.DEAD)
            {

                ClearMove();
                anim.SetBool("dead", true);

            }

            /*
             * Check Action State
             */
            if (boss.GetActionState() == BossActionState.ATTACKING)
            {

            ClearAction();
                anim.SetTrigger("attacking");

            }
            else if (boss.GetActionState() == BossActionState.DEAD)
            {

            ClearAction();
            anim.SetBool("dead", true);

            }
            else if(boss.GetActionState() == BossActionState.PREPATTACK)
            {

            ClearAction();
            anim.SetBool("preAttack", true);

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
        anim.SetBool("preAttack", false);

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
