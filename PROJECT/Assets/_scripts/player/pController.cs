using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class pController : MonoBehaviour {

    public float clampOffset;

    private DIRECTION dodgeDirection;

    private bool hitWall;

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;

    private CharacterController2D _controller;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;
    private player player;

    private menuManager menu;

    private ContactPoint2D[] contacts;

    void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        player = GetComponent<player>();

        // listen to some events for illustration purposes
        _controller.onControllerCollidedEvent += onControllerCollider;
        _controller.onTriggerEnterEvent += onTriggerEnterEvent;
        _controller.onTriggerExitEvent += onTriggerExitEvent;
    }

    private void Start()
    {

        menu = GameObject.Find("gameManager").GetComponent<menuManager>();

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


    // the Update loop contains a very simple example of moving the character around and controlling the animation
    void Update()
    {

        if (player.GetActionState() != PlayerActionState.DEAD && 
            pauseState.instance.GetPauseState() != PAUSESTATE.PAUSED)
        {

            GetInput();

            CheckWall();

            // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
            var smoothedMovementFactor = _controller.isGrounded ? player.groundDamping : player.inAirDamping; // how fast do we change direction?
            _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * player.moveSpeed, Time.deltaTime * smoothedMovementFactor);

            _velocity.y += player.gravity * Time.deltaTime;

            _controller.move(_velocity * Time.deltaTime);

            if(_controller._isGoingUpSlope && normalizedHorizontalSpeed > 0)
            {

                this.transform.position = new Vector3(transform.position.x, transform.position.y + _controller.skinWidth, transform.position.z);

            }

            // grab our current _velocity to use as a base for all calculations
            _velocity = _controller.velocity;

            Debug.Log("Grounded: " + _controller.isGrounded);

            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            screenPos.x = Mathf.Clamp(screenPos.x, 0f + clampOffset, Screen.width - clampOffset);
            screenPos.y = Mathf.Clamp(screenPos.y, 0f + clampOffset, Screen.height - clampOffset);

            transform.position = Camera.main.ScreenToWorldPoint(screenPos);

        }

    }

    private void CheckWall()
    {

        float rayLength;

        if(player.GetMoveState() == PlayerMoveState.SLIDING)
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

        if ((hits && 
            hits.transform.gameObject.layer == 8)) //8 = GROUND
        {

            _controller.hitWall = true;
            normalizedHorizontalSpeed = 0;
            transform.Translate(new Vector3(-1 - _controller.skinWidth, 0, 0) * (trackConstructor.instance.moveSpeed * 2.5f) * Time.deltaTime);
            
        }
        else
        {

            _controller.hitWall = false;

        }

    }

    private void GetInput()
    {

        if (_controller.isGrounded)
        {

            _velocity.y = 0;

        }

        if (player.GetActionState() != PlayerActionState.DODGING)
        {

            if (player.GetMoveRight())
            {

                normalizedHorizontalSpeed = 1;

            }
            else if (player.GetMoveLeft())
            {

                normalizedHorizontalSpeed = -1;

            }
            else
            {

                normalizedHorizontalSpeed = 0;

            }

            // we can only jump whilst grounded
            if (_controller.isGrounded &&
                player.GetJump())
            {

                _velocity.y = Mathf.Sqrt(2f * player.jumpHeight * -player.gravity);

            }

        }

        if(player.GetAttack() && !player.GetDodge())
        {

            player.SetActionState(PlayerActionState.ATTACKING);

        }
        else if(player.GetDodge())
        {

            player.SetActionState(PlayerActionState.DODGING);

            if (player.role != PlayerClass.KNIGHT)
            {

                if (player.aim.GetFacingRight())
                {

                    dodgeDirection = DIRECTION.LEFT;
                    normalizedHorizontalSpeed = -1;
                    
                }
                else
                {

                    dodgeDirection = DIRECTION.RIGHT;
                    normalizedHorizontalSpeed = 1;

                }

                _velocity.y = Mathf.Sqrt(2f * player.jumpHeight * -player.gravity);

            } else if(player.role == PlayerClass.KNIGHT)
            {

                dodgeDirection = DIRECTION.NONE;
                normalizedHorizontalSpeed = 0;

            }

        }
        else
        {

            player.SetActionState(PlayerActionState.NULL);

        }

        if(player.GetSlide() && 
            player.GetMoveState() != PlayerMoveState.MIDAIR &&
            player.GetActionState() != PlayerActionState.DODGING)
        {

            player.SetMoveState(PlayerMoveState.SLIDING);
            normalizedHorizontalSpeed = -0.5f;

        }else if (!_controller.isGrounded)
        {

            player.SetMoveState(PlayerMoveState.MIDAIR);

        }
        else
        {

            player.SetMoveState(PlayerMoveState.RUNNING);

        }

    }

    void ClearDodge()
    {

        player.SetActionState(PlayerActionState.NULL);

    }

}
