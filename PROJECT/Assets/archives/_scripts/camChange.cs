using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camChange : MonoBehaviour {

    [Header("Movement and Rotation Flags")]
    public bool rotate;
    public bool move;

    [Header("Movement and Rotation Speeds")]
    public float moveSpeed;
    public float rotateSpeed;

    [Header("Time to Rotate (In Seconds)")]
    public float rotateTime;

    [Header("Which Way to Move and Rotate")]
    public DIRECTION moveDirection;
    public ROTATION_DIRECTION rotateDirection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "MainCamera")
        {

            Debug.Log("Changing Camera!");

            camMove move = Camera.main.GetComponent<camMove>();

            //move.SetRotation(rotate, rotateTime, rotateSpeed, rotateDirection);
            //move.SetMovement(move, moveDirection, moveSpeed);

        }

    }

}
