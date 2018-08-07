using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class knightAI : MonoBehaviour {

    public float posOffset;

    private bool inTrigger;
    private bool inRange;
    private bool attackCountdown;

    private float actionTimer;
    private float attackTimer;

    private minion stats;

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
    void Start () {

        stats = GetComponent<minion>();
        _controller = GetComponent<CharacterController2D>();

        actionTimer = stats.actionDelay;
        attackTimer = 0.25f;
        attackCountdown = false;

	}
	
	// Update is called once per frame
	void Update () {

        stats.FindTarget();
        player = stats.target.GetComponent<player>();

        if(!stats.dead && 
            pauseState.instance.GetPauseState() != PAUSESTATE.PAUSED &&
            player.GetMoveState() != PlayerMoveState.DEAD)
        {

            CoolDown();

            CheckWall();

            if(_controller.isGrounded)
            {

                stats.SetMoveState(MinionMoveState.RUNNING);

            }
            else
            {

                stats.SetMoveState(MinionMoveState.MIDAIR);

            }
            
            if(actionTimer > 0.0f)
            {

                stats.SetActionState(MinionActionState.NULL);

            }
            else if (actionTimer <= 0.0f)
            {

                MoveTo(stats.target);

                if (attackTimer <= 0.0f)
                {

                    Attack();
                    attackCountdown = false;
                    attackTimer = 0.25f;

                }

            }

            if (_controller._isGoingUpSlope)
            {

                normalizedHorizontalSpeed = 0;

            }

            // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
            var smoothedMovementFactor = _controller.isGrounded ? stats.groundDamping : stats.inAirDamping; // how fast do we change direction?

            _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * stats.moveSpeed, Time.deltaTime * smoothedMovementFactor);
            
        
            _velocity.y += stats.gravity * Time.deltaTime;

            _controller.move(_velocity * Time.deltaTime);

            // grab our current _velocity to use as a base for all calculations
            _velocity = _controller.velocity;

            /*
            if (_controller._isGoingUpSlope && normalizedHorizontalSpeed > 0)
            {

                this.transform.position = new Vector3(transform.position.x, transform.position.y + _controller.skinWidth, transform.position.z);

            }
            */

        }
		
	}

    void CoolDown()
    {

        if (actionTimer > 0.0f)
        {

            actionTimer -= Time.deltaTime;

        }

        if (attackCountdown && attackTimer > 0.0f)
        {

            attackTimer -= Time.deltaTime;

        }

    }

    private void CheckWall()
    {

        float rayLength;

        if (player.GetMoveState() == PlayerMoveState.SLIDING)
        {

            rayLength = 0.5f;

        }
        else
        {

            rayLength = 3.0f;

        }

        RaycastHit2D hits =
            Physics2D.Raycast(
                new Vector3(transform.position.x + 0.75f,
                    transform.position.y - 1.25f,
                    transform.position.z),
                Vector2.up,
                rayLength
                );

        Debug.DrawRay(new Vector3(transform.position.x + 0.75f,
                    transform.position.y - 1.25f,
                    transform.position.z),
                Vector2.up * rayLength,
                Color.red,
                0.05f);

        if (hits &&
            hits.transform.gameObject.layer == 8) //8 = GROUND
        {

            _controller.hitWall = true;
            normalizedHorizontalSpeed = 0;
            transform.Translate(new Vector3(-1 - _controller.skinWidth, 0, 0) * (trackConstructor.instance.moveSpeed * 2) * Time.deltaTime);

        }
        else
        {

            _controller.hitWall = false;

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
            attackCountdown = true;

        }

    }


    void Attack()
    {

        if(inRange)
        {

            stats.SetActionState(MinionActionState.ATTACKING);
            actionTimer = stats.actionDelay;

        }

    }

    public void Jump()
    {

        stats.SetMoveState(MinionMoveState.JUMPING);
        _velocity.y = Mathf.Sqrt(2f * stats.jumpHeight * -stats.gravity);

    }

    public void Slide()
    {

        stats.SetMoveState(MinionMoveState.SLIDING);

    }

    public void EndSlide()
    {

        stats.SetMoveState(MinionMoveState.RUNNING);

    }

}
