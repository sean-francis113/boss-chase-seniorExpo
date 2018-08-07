using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseState : MonoBehaviour {

    public static pauseState instance;

    PAUSESTATE state;

    bool CanPause;

    void Awake()
    {

        if(!instance)
        {

            instance = this;

        }
        else
        {

            Destroy(this);

        }

    }

    public void SetCanPause(bool set)
    {

        CanPause = set;

    }

    public void SetPauseState(PAUSESTATE set)
    {

        state = set;

    }

    public PAUSESTATE GetPauseState()
    {

        return state;

    }

    public bool GetCanPause()
    {

        return CanPause;

    }

}

public enum PAUSESTATE
{

    NULL,
    PAUSED,
    UNPAUSED

}