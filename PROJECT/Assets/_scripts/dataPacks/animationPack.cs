using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationPack : MonoBehaviour {

    public string runAnimPath;
    public string jumpAnimPath;
    public string midairAnimPath;
    public string attackAnimPath;
    public string dodgeAnimPath;
    public string deathAnimPath;

    public string[] specialAnimPath;

}

public enum ANIMATION
{

    NONE,
    RUN,
    JUMP,
    MIDAIR,
    ATTACK,
    DODGE,
    DEATH

}
