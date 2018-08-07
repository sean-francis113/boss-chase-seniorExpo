using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDeath : MonoBehaviour {

    private boss bossStats;

    private void Awake()
    {

        bossStats = GetComponent<boss>();

    }

    public void death()
    {

        trackConstructor.instance.SetBuffer(true);
        bossStats.spawned = false;

    }

}
