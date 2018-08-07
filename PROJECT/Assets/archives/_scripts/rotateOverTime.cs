using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateOverTime : MonoBehaviour {

    private bool rotate;

    private DIRECTION direction;

    private float rotationSpeed;

    private float maxAngle;

    private float rotateTimer;

    private void Update()
    {

        if (rotate)
        {

            if (direction == DIRECTION.LEFT)
            {

                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

            }
            else if(direction == DIRECTION.RIGHT)
            {

                transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);

            }

            rotateTimer -= Time.deltaTime;

            if(rotateTimer <= 0.0f)
            {

                rotate = false;
                rotationSpeed = 0.0f;
                direction = DIRECTION.NONE;

            }

        }

    }

    public void SetRotation(bool rotate, float time, float speed, DIRECTION direction)
    {

        rotateTimer = time;
        this.rotate = rotate;
        this.direction = direction;
        rotationSpeed = speed;

    }

}
