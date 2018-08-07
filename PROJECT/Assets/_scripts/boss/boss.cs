using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour {

    public bool spawned;
    public bool dead;
    public bool blocked;

    public float moveSpeed;
    public float jumpHeight;
    public float gravity;
    public float actionDelay;
    public float groundDamping; // how fast do we change direction? higher means faster
    public float inAirDamping;
    public float attackDelay;

    public GameObject target;

    public float health;

    public List<Anima2D.SpriteMeshInstance> limbList;

    private float invulCount;
    private float maxHealth;
    private float redFlashTimer;
    private float alphaFlashTimer;

    public bAnimController anim;
    public charTorsoAim aim;
    public soundPack sound;

    private BossMoveState moveState;
    private BossActionState actionState;

    private void Start()
    {

        maxHealth = health;
        anim = GetComponent<bAnimController>();
        aim = GetComponent<charTorsoAim>();
        sound = GetComponent<soundPack>();

    }

    void Update()
    {

        CheckDeath();
        FindTarget();

        if (pauseState.instance.GetPauseState() != PAUSESTATE.PAUSED &&
            target.GetComponent<player>().GetMoveState() != PlayerMoveState.DEAD)
        {

            CountDown();
            anim.ContinueAnimations();
            aim.SetTarget(target.transform);
            CheckDirection();
            CheckNearDeath();

        }
        else
        {

            anim.PauseAnimations();

        }

    }

    void CountDown()
    {

        if (invulCount > 0.0f)
        {

            invulCount -= Time.deltaTime;

            if (invulCount <= 0.0f)
            {

                for (int i = 0; i < limbList.Count; i++)
                {

                    Color temp = limbList[i].color;
                    temp.a = 1; ;
                    limbList[i].color = temp;

                }

                if (blocked)
                {

                    blocked = false;

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

    void CheckDirection()
    {

        Vector3 bScale = this.transform.localScale;

        if (aim.GetFacingRight())
        {

            transform.localScale = new Vector3(Mathf.Abs(bScale.x), bScale.y, bScale.z);

        }
        else
        {

            if (Mathf.Sign(bScale.x) == 1)
            {

                transform.localScale = new Vector3(-bScale.x, bScale.y, bScale.z);

            }
            else if (Mathf.Sign(bScale.x) == -1)
            {

                transform.localScale = new Vector3(bScale.x, bScale.y, bScale.z);

            }

        }

    }

    void CheckDeath()
    {

        if (health <= 0)
        {

            moveState = BossMoveState.DEAD;
            actionState = BossActionState.DEAD;
            sound.PlaySound(SOUND.DEATH);

        }

    }

    void Death()
    {

        menuManager.instance.RemoveBossHealthUI();

        if (target.GetComponent<player>())
        {

            target.GetComponent<player>().bossKills++;

        }

        ResetSelf();
        trackConstructor.instance.SetBuffer(true);
        this.gameObject.SetActive(false);
        health = maxHealth;
        moveState = BossMoveState.NULL;
        actionState = BossActionState.NULL;
        audioManager.instance.CrossFade(MUSIC.BOSS_DEATH);
        spawned = false;
        

    }

    public void ResetSelf()
    {

        health = maxHealth;
        anim.ResetAnimationState();

    }

    public void FindTarget()
    {

        GameObject[] players;

        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {

            Debug.Log("Cannot Find Player(s)!");
            return;

        }
        else if (players.Length == 1)
        {

            Debug.Log("Found One Player!");
            target = players[0];

        }
        else if (players.Length > 1)
        {

            Debug.Log("Found Multiple Players!");
            target = players[Random.Range(0, players.Length)];

        }

    }

    public void CheckNearDeath()
    {

        if ((health / maxHealth) <= 0.5f)
        {

            audioManager.instance.CrossFade(MUSIC.BOSS_NEARDEATH);

        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Triggered!");

        if (collision.tag == "playerWeapon" ||
            collision.tag == "playerProjectile")
        { 

            if (invulCount <= 0.0f &&
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

            invulCount = 0.15f;

        }

    }

    public BossMoveState GetMoveState()
    {

        return moveState;

    }

    public BossActionState GetActionState()
    {

        return actionState;

    }

    public void SetMoveState(BossMoveState set)
    {

        moveState = set;

    }

    public void SetActionState(BossActionState set)
    {

        actionState = set;

    }

}

public enum BossMoveState
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

public enum BossActionState
{

    NULL,
    ATTACKING,
    DODGING,
    PREPATTACK,
    DEAD

}