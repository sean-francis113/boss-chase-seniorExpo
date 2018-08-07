using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class testBossAI : MonoBehaviour {

    public float posOffset;

    private bool inTrigger;
    private bool inRange;
    private bool attackCountDown;

    private float actionTimer;
    private float attackTimer;

    private boss stats;

    private Rigidbody2D rb;
    private RaycastHit2D[] hit;

    private DIRECTION dodgeDirection;

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;

    private CharacterController2D _controller;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;
    private player player;

    // Use this for initialization
    void Start()
    {

        stats = GetComponent<boss>();
        _controller = GetComponent<CharacterController2D>();

        actionTimer = stats.actionDelay;
        attackTimer = 0.25f;
        attackCountDown = false;

    }

    #region Event Listeners

    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        //Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
    }


    void onTriggerEnterEvent(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
    }


    void onTriggerExitEvent(Collider2D col)
    {
        Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    #endregion

    // Update is called once per frame
    void Update()
    {

        stats.FindTarget();
        player = stats.target.GetComponent<player>();

        if (!stats.dead &&
            pauseState.instance.GetPauseState() != PAUSESTATE.PAUSED &&
            player.GetMoveState() != PlayerMoveState.DEAD)
        {

            CoolDown();

            if (_controller.isGrounded)
            {

                stats.SetMoveState(BossMoveState.RUNNING);

            }
            else
            {

                stats.SetMoveState(BossMoveState.MIDAIR);

            }

            if (actionTimer > 0.0f)
            {

                stats.SetActionState(BossActionState.NULL);

            }
            else if (actionTimer <= 0.0f)
            {

                MoveTo(player.gameObject);

                if (attackTimer <= 0.0f)
                {

                    Attack();
                    attackCountDown = false;
                    attackTimer = 0.25f;

                }

            }

            // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
            var smoothedMovementFactor = _controller.isGrounded ? stats.groundDamping : stats.inAirDamping; // how fast do we change direction?

            _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * stats.moveSpeed, Time.deltaTime * smoothedMovementFactor);


            _velocity.y += stats.gravity * Time.deltaTime;

            _controller.move(_velocity * Time.deltaTime);

            // grab our current _velocity to use as a base for all calculations
            _velocity = _controller.velocity;

        }

    }

    void CoolDown()
    {

        if (actionTimer > 0.0f)
        {

            actionTimer -= Time.deltaTime;

        }

        if(attackCountDown && attackTimer > 0.0f)
        {

            attackTimer -= Time.deltaTime;

        }

    }

    void MoveTo(GameObject target)
    {

        if (!target)
        {

            Debug.Log("Target Null!");
            return;

        }

        if (this.transform.position.x <= target.transform.position.x - posOffset)
        {

            inRange = false;
            //stats.facingRight = true;

            Debug.Log("Minion Moving Right!");

            normalizedHorizontalSpeed = 1;

        }
        else if (this.transform.position.x >= target.transform.position.x + posOffset)
        {

            inRange = false;
            //stats.facingRight = false;

            Debug.Log("Minion Moving Left!");

            normalizedHorizontalSpeed = -1;

        }
        else
        {

            inRange = true;
            normalizedHorizontalSpeed = 0;
            attackCountDown = true;

        }

    }


    void Attack()
    {

        if (inRange)
        {

            stats.SetActionState(BossActionState.ATTACKING);
            actionTimer = stats.actionDelay;
            stats.sound.PlaySound(SOUND.ATTACK);

        }

    }

}
