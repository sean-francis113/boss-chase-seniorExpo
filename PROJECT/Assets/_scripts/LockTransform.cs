using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransform : MonoBehaviour {

    public TransformLockMode mode;

    public Vector3 lockedTransform;

    private void Update()
    {
        
        if(mode == TransformLockMode.POS_X)
        {

            transform.position = 
                new Vector3(
                    lockedTransform.x, 
                    transform.position.y, 
                    transform.position.z);

        }

    }

}

public enum TransformLockMode
{

    NULL,
    POS_X,
    POS_Y,
    POS_z

}
