using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minion : MonoBehaviour {

    public MinionClass role;

    public bool dead;
    public bool canDodge;
    public bool inTrigger;
    public bool blocked;

    public float moveSpeed;
    public float jumpHeight;
    public float jumpChance;
    public float gravity;
    public float actionDelay;
    public float groundDamping; // how fast do we change direction? higher means faster
    public float inAirDamping;

    public GameObject target;

    public float health;

    public List<Anima2D.SpriteMeshInstance> limbList;

    private bool grounded;
    
    private float invulTime;
    private float maxHealth;
    private float redFlashTimer;
    private float alphaFlashTimer;

    private Rigidbody2D rb;
    private mAnimController anim;
    public charTorsoAim aim;
    public soundPack sound;

    private MinionMoveState moveState;
    private MinionActionState actionState;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<mAnimController>();
        aim = GetComponent<charTorsoAim>();
        sound = GetComponent<soundPack>();

        maxHealth = health;

    }

    void Update () {

        FindTarget();

        if (pauseState.instance.GetPauseState() != PAUSESTATE.PAUSED &&
            target.GetComponent<player>().GetMoveState() != PlayerMoveState.DEAD)
        {

            anim.ContinueAnimations();
            aim.SetTarget(target.transform);
            CountDown();
            CheckDirection();
            CheckDeath();

        }else
        {

            anim.PauseAnimations();

        }

	}

    void CountDown()
    {

        invulTime -= Time.deltaTime;

        if (invulTime <= 0.0f)
        {

            for (int i = 0; i < limbList.Count; i++)
            {

                Color temp = limbList[i].color;
                temp.a = 1;
                limbList[i].color = temp;

            }

            if (blocked)
            {

                blocked = false;

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

        if (alphaFlashTimer > 0.0f)
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

    void CheckDeath()
    {

        if (health <= 0)
        {

            moveState = MinionMoveState.DEAD;
            actionState = MinionActionState.DEAD;
            sound.PlaySound(SOUND.DEATH);

        }

    }

    void CheckDirection()
    {

        Vector3 mScale = this.transform.localScale;

        if (aim.GetFacingRight())
        {

            transform.localScale = new Vector3(Mathf.Abs(mScale.x), mScale.y, mScale.z);

        }
        else
        {

            if (Mathf.Sign(mScale.x) == 1)
            {

                transform.localScale = new Vector3(-mScale.x, mScale.y, mScale.z);

            }
            else if (Mathf.Sign(mScale.x) == -1)
            {

                transform.localScale = new Vector3(mScale.x, mScale.y, mScale.z);

            }

        }

    }

    void Death()
    {

        this.gameObject.SetActive(false);

        if (target.GetComponent<player>())
        {

            target.GetComponent<player>().minionKills++;

        }

        ResetSelf();
        spawnEnemies.instance.ReturnToPool(this.GetComponent<minion>());

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "playerWeapon" ||
            collision.tag == "splashWeapon")
        {

            if (invulTime <= 0.0f &&
                !blocked)
            {

                health--;

                for (int i = 0; i < limbList.Count; i++)
                {

                    limbList[i].color = new Color(1.0f, 0.0f, 0.0f);

                }

                redFlashTimer = 0.08f;

                sound.PlaySound(SOUND.DAMAGED);

                if (collision.tag == "playerProjectile")
                {

                    Destroy(collision.gameObject);

                }

            }
            else if (blocked)
            {

                if (collision.tag == "playerProjectile")
                {

                    Destroy(collision.gameObject);

                }

            }

            invulTime = 0.15f;

        }

        if (collision.tag == "AIJump")
        {

            minionTrigger trigger = collision.gameObject.GetComponent<minionTrigger>();

            if (!inTrigger)
            {

                inTrigger = true;

                if (trigger.jump)
                {

                    float rand = Random.Range(1, 100);

                    if (rand <= trigger.jumpChance)
                    {

                        Jump();

                    }

                }

            }

        }

    }

    void Jump()
    {

        if(role == MinionClass.KNIGHT)
        {

            knightAI script = GetComponent<knightAI>();

            script.Jump();

        }

    }

    void Slide()
    {

        if (role == MinionClass.KNIGHT)
        {

            knightAI script = GetComponent<knightAI>();

            script.Slide();

        }

    }

    public void FindTarget()
    {

        GameObject[] players;

        players = GameObject.FindGameObjectsWithTag("Player");

        if(players.Length == 0)
        {

            Debug.Log("Cannot Find Player(s)!");
            return;

        }else if(players.Length == 1)
        {

            Debug.Log("Found One Player!");
            target = players[0];

        } else if(players.Length > 1)
        {

            Debug.Log("Found Multiple Players!");
            target = players[Random.Range(0, players.Length)];

        }

    }

    public void ResetSelf()
    {

        health = maxHealth;
        moveState = MinionMoveState.NULL;
        actionState = MinionActionState.NULL;

        for(int i = 0; i < limbList.Count; i++)
        {

            Color temp = limbList[i].color;
            temp.a = 1f;
            limbList[i].color = temp;

        }

        anim.ResetAnimationState();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "AIJump")
        {

            if (inTrigger)
            {

                inTrigger = false;

            }

        }

    }

    public MinionMoveState GetMoveState()
    {

        return moveState;

    }

    public MinionActionState GetActionState()
    {

        return actionState;

    }

    public void SetMoveState(MinionMoveState set)
    {

        moveState = set;

    }

    public void SetActionState(MinionActionState set)
    {

        actionState = set;

    }

}

public enum MinionMoveState
{

    NULL,
    RUNNING,
    SLIDING,
    JUMPING,
    MIDAIR,
    DOWNHILL,
    UPHILL,
    DEAD

}

public enum MinionActionState
{

    NULL,
    ATTACKING,
    DEAD

}

public enum MinionClass
{

    NONE,
    KNIGHT,
    ARCHER,
    BRAWLER,
    GRENADIER

}
