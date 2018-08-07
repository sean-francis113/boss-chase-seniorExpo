using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class goblinKingAI : MonoBehaviour {

    [Header("Timers")]
    public float vulnerableTime;
    public float prepAttackTime;
    public float blockTime;

    [Header("Attack Position Offset")]
    public float posOffset;

    [Header("Blocking Shield")]
    public GameObject shield;

    private AIState state;

    private bool attacked;
    private bool targetFound;

    private float vTimer;
    private float pATimer;
    private float bTimer;
    private float normalizedHorizontalSpeed = 0;

    private Vector3 attackPos;

    private CharacterController2D controller;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;
    private boss stats;

    // Use this for initialization
    void Start () {

        controller = GetComponent<CharacterController2D>();
        stats = GetComponent<boss>();

        vTimer = vulnerableTime;
        pATimer = prepAttackTime;
        bTimer = blockTime;
        state = AIState.BLOCKING;

	}
	
	// Update is called once per frame
	void Update () {

        if (!stats.dead &&
            pauseState.instance.GetPauseState() != PAUSESTATE.PAUSED &&
            stats.target.GetComponent<player>().GetMoveState() != PlayerMoveState.DEAD)
        {

            CoolDown();

            if (controller.isGrounded)
            {

                stats.SetMoveState(BossMoveState.RUNNING);

            }
            else
            {

                stats.SetMoveState(BossMoveState.MIDAIR);

            }

            if (bTimer > 0.0f)
            {

                state = AIState.BLOCKING;
                shield.SetActive(true);
                stats.blocked = true;
                attacked = false;
                targetFound = false;

            }
            else
            {

                if (pATimer > 0.0f)
                {

                    state = AIState.PREPATTACK;
                    stats.SetActionState(BossActionState.PREPATTACK);

                    if (!targetFound)
                    {

                        attackPos = stats.target.transform.position;
                        targetFound = true;

                    }

                }
                else if (pATimer <= 0.0f && !attacked)
                {

                    stats.SetActionState(BossActionState.NULL);
                    stats.anim.ResetAnimationState();

                    if (stats.aim.GetFacingRight())
                    {

                        transform.position = new Vector3(attackPos.x - posOffset,
                            attackPos.y,
                            attackPos.z);

                    }
                    else
                    {

                        transform.position = new Vector3(attackPos.x + posOffset,
                            attackPos.y,
                            attackPos.z);

                    }

                    state = AIState.ATTACK;
                    stats.SetActionState(BossActionState.ATTACKING);
                    shield.SetActive(false);
                    stats.blocked = false;
                    attacked = true;

                }
                else if (pATimer <= 0.0f && attacked)
                {

                    state = AIState.VULNERABLE;
                    stats.SetActionState(BossActionState.NULL);

                }

            }

            if (vTimer <= 0.0f && state == AIState.VULNERABLE)
            {

                ResetTimers();
                state = AIState.BLOCKING;

            }

            if (state == AIState.PREPATTACK)
            {

                normalizedHorizontalSpeed = -1;

            }
            else
            {

                normalizedHorizontalSpeed = 0;

            }

            // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
            var smoothedMovementFactor = controller.isGrounded ? stats.groundDamping : stats.inAirDamping; // how fast do we change direction?
            _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * stats.moveSpeed, Time.deltaTime * smoothedMovementFactor);

            _velocity.y += stats.gravity * Time.deltaTime;

            controller.move(_velocity * Time.deltaTime);

            // grab our current _velocity to use as a base for all calculations
            _velocity = controller.velocity;

        }
		
	}

    void CoolDown()
    {

        if(vTimer > 0.0f &&
            state == AIState.VULNERABLE)
        {

            vTimer -= Time.deltaTime;

        }

        if(pATimer > 0.0f &&
            state == AIState.PREPATTACK)
        {

            pATimer -= Time.deltaTime;

        }

        if(bTimer > 0.0f &&
            state == AIState.BLOCKING)
        {

            bTimer -= Time.deltaTime;

        }

    }

    void ResetTimers()
    {

        vTimer = vulnerableTime;
        pATimer = prepAttackTime;
        bTimer = blockTime;

    }

    void SetAttacked(bool set)
    {

        attacked = set;

    }

}

enum AIState
{

    NULL,
    BLOCKING,
    PREPATTACK,
    ATTACK,
    VULNERABLE

}