using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    [Header("Player Stats")]
    public PlayerClass role;
    public bool dead;
    public bool blocked;
    public float moveSpeed;
    public float jumpHeight;
    public float dodgeHeight;
    public float gravity;
    public float groundDamping; // how fast do we change direction? higher means faster
    public float inAirDamping;
    public float health;
    public float runTime;
    public int minionKills;
    public int bossKills;

    /*
     * Keyboard Inputs
     */
    [Space(25)]
    [Header("Keyboard Bindings")]
    public KeyCode mRightKey;
    public KeyCode mLeftKey;
    public KeyCode jumpKey;
    public KeyCode attackKey;
    public KeyCode dodgeKey;
    public KeyCode pauseKey;
    public KeyCode slideKey;

    public KeyCode mRightAltKey;
    public KeyCode mLeftAltKey;
    public KeyCode jumpAltKey;
    public KeyCode attackAltKey;
    public KeyCode dodgeAltKey;
    public KeyCode pauseAltKey;
    public KeyCode slideAltKey;

    /*
     * Joystick Inputs
     */
    [Space(25)]
    [Header("Joystick Bindings")]
    public KeyCode mRightJoy;
    public KeyCode mLeftJoy;
    public KeyCode jumpJoy;
    public KeyCode attackJoy;
    public KeyCode dodgeJoy;
    public KeyCode pauseJoy;
    public KeyCode slideJoy;

    public KeyCode mRightAltJoy;
    public KeyCode mLeftAltJoy;
    public KeyCode jumpAltJoy;
    public KeyCode attackAltJoy;
    public KeyCode dodgeAltJoy;
    public KeyCode pauseAltJoy;
    public KeyCode slideAltJoy;

    public List<Anima2D.SpriteMeshInstance> limbList;

    private bool grounded;
    private bool blockSoundPlayed;
    private bool deathSoundPlayed;

    private float invulTime;
    private float maxHealth;
    private float redFlashTimer;
    private float alphaFlashTimer;

    private PlayerMoveState moveState;
    private PlayerActionState actionState;

    private GameObject game;
    private menuManager menu;
    private pAnimController anim;
    public charTorsoAim aim;
    public soundPack sound;

    private void Start()
    {

        game = GameObject.Find("gameManager");
        menu = game.GetComponent<menuManager>();
        anim = GetComponent<pAnimController>();
        aim = GetComponent<charTorsoAim>();
        sound = GetComponent<soundPack>();

        maxHealth = health;

    }

    private void Update()
    {

        dead = CheckDeath();
        CheckNearDeath();

        if (!dead)
        {

            runTime += Time.deltaTime;

            CountDown();
            CheckDirection();

            deathSoundPlayed = false;

            if (GetPause() &&
                pauseState.instance.GetCanPause())
            {

                if (pauseState.instance.GetPauseState() == PAUSESTATE.UNPAUSED)
                {

                    pauseState.instance.SetPauseState(PAUSESTATE.PAUSED);
                    menu.ShowPauseMenu();
                    spawnEnemies.instance.SetCanSpawnMinions(false);
                    trackConstructor.instance.SetCanMove(false);


                }
                else
                {

                    pauseState.instance.SetPauseState(PAUSESTATE.UNPAUSED);

                    if (menu.pauseUI.activeSelf)
                    {

                        menu.ResumeGame();

                    }

                }

            }

            if (pauseState.instance.GetPauseState() == PAUSESTATE.PAUSED)
            {

                anim.PauseAnimations();

            }
            else
            {

                anim.ContinueAnimations();

            }

        }
        else
        {

            moveState = PlayerMoveState.DEAD;
            actionState = PlayerActionState.DEAD;
            spawnEnemies.instance.SetCanSpawnMinions(false);
            trackConstructor.instance.SetCanMove(false);

            if (!deathSoundPlayed)
            {

                sound.PlaySound(SOUND.DEATH);
                deathSoundPlayed = true;

            }

            audioManager.instance.CrossFade(MUSIC.PLAYER_DEATH);

        }

    }

    private bool CheckDeath()
    {

        if(health <= 0)
        {

            return true;

        }

        return false;

    }

    private void CountDown()
    {

        /*
         * Place to Deal with Timers
         */
        if (invulTime > 0.0f)
        {

            invulTime -= Time.deltaTime;

            if (invulTime <= 0.0f)
            {

                if (blocked)
                {

                    blocked = false;
                    blockSoundPlayed = false;

                }

            }

        }

        if (redFlashTimer > 0.0f)
        {

            redFlashTimer -= Time.deltaTime;

            if (redFlashTimer <= 0.0f)
            {

                for (int i = 0; i < limbList.Count; i++)
                {

                    limbList[i].color = new Color(1.0f, 1.0f, 1.0f, 0.25f);

                }

                alphaFlashTimer = 0.08f;

            }

        }

        if(alphaFlashTimer > 0.0f)
        {

            alphaFlashTimer -= Time.deltaTime;

            if (alphaFlashTimer <= 0.0f)
            {

                for (int i = 0; i < limbList.Count; i++)
                {

                    limbList[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

                }

            }

        }

    }

    private void CheckDirection()
    {

        Vector3 pScale = this.transform.localScale;

        if (aim.GetFacingRight())
        {

            transform.localScale = new Vector3(Mathf.Abs(pScale.x), pScale.y, pScale.z);

        }
        else
        {

            if (Mathf.Sign(pScale.x) == 1)
            {

                transform.localScale = new Vector3(-pScale.x, pScale.y, pScale.z);

            }
            else if (Mathf.Sign(pScale.x) == -1)
            {

                transform.localScale = new Vector3(pScale.x, pScale.y, pScale.z);

            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "enemyWeapon" || 
            collision.tag == "enemyProjectile" ||
            collision.tag == "splashWeapon")
        {

            if (invulTime <= 0.0f && 
                actionState != PlayerActionState.DODGING &&
                !blocked)
            {

                health--;

                for (int i = 0; i < limbList.Count; i++)
                {

                    limbList[i].color = new Color(1.0f, 0.0f, 0.0f);

                }

                redFlashTimer = 0.08f;

                sound.PlaySound(SOUND.DAMAGED);

                if (collision.tag == "enemyProjectile")
                {

                    Destroy(collision.gameObject);

                }

            }
            else if(blocked)
            {

                if(!blockSoundPlayed)
                {

                    sound.PlaySound(SOUND.BLOCKED);
                    blockSoundPlayed = true;

                }

                if (collision.tag == "enemyProjectile")
                {

                    Destroy(collision.gameObject);

                }

            }

            invulTime = 0.15f;

        }

    }

    public void SetDeathStats()
    {

        menuManager.instance.SetStats(minionKills, bossKills, runTime);

    }

    public void SetMoveState(PlayerMoveState set)
    {

        moveState = set;

    }

    public void SetActionState(PlayerActionState set)
    {

        actionState = set;

    }

    public void SetGrounded(bool set)
    {

        grounded = set;

    }

    public PlayerMoveState GetMoveState()
    {

        return moveState;

    }

    public PlayerActionState GetActionState()
    {

        return actionState;

    }

    public bool GetGrounded()
    {

        return grounded;

    }

    public bool GetMoveRight()
    {

        return Input.GetKey(mRightKey) ||
            Input.GetKey(mRightAltKey) ||
            Input.GetKey(mRightJoy) ||
            Input.GetKey(mRightAltJoy);

    }

    public bool GetMoveLeft()
    {

        return Input.GetKey(mLeftKey) ||
            Input.GetKey(mLeftAltKey) ||
            Input.GetKey(mLeftJoy) ||
            Input.GetKey(mLeftAltJoy);
        
    }

    public bool GetJump()
    {

        return Input.GetKey(jumpKey) ||
            Input.GetKey(jumpAltKey) ||
            Input.GetKey(jumpJoy) ||
            Input.GetKey(jumpAltJoy);

    }

    public bool GetAttack()
    {

        return Input.GetKey(attackKey) ||
            Input.GetKey(attackAltKey) ||
            Input.GetKey(attackJoy) ||
            Input.GetKey(attackAltJoy);

    }

    public bool GetDodge()
    {

        return Input.GetKey(dodgeKey) ||
            Input.GetKey(dodgeAltKey) ||
            Input.GetKey(dodgeJoy) ||
            Input.GetKey(dodgeAltJoy);

    }

    public bool GetPause()
    {

        return Input.GetKeyDown(pauseKey) ||
            Input.GetKeyDown(pauseAltKey) ||
            Input.GetKeyDown(pauseJoy) ||
            Input.GetKeyDown(pauseAltJoy);

    }

    public bool GetSlide()
    {

        return Input.GetKey(slideKey) ||
            Input.GetKey(slideAltKey) ||
            Input.GetKey(slideJoy) ||
            Input.GetKey(slideAltJoy);

    }

    public void ShowDeathMenu()
    {

        menu.ShowDeathMenu();

    }

    public void CheckNearDeath()
    {

        Debug.Log("Player Health %: " + (float)(health / maxHealth));

        if((health / maxHealth) <= 0.25f)
        {

            audioManager.instance.CrossFade(MUSIC.PLAYER_NEARDEATH);

        }

    }

}

public enum PlayerMoveState
{

    NULL,
    RUNNING,
    SLIDING,
    JUMPING,
    MIDAIR,
    DOWNHILL,
    UPHILL,
    PAUSED,
    DEAD

}

public enum PlayerActionState
{

    NULL,
    ATTACKING,
    DODGING,
    DEAD

}

public enum PlayerClass
{

    NULL,
    KNIGHT,
    ARCHER,
    GRENADIER,
    BRAWLER

}