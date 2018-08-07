using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pArmAim : MonoBehaviour {

    [SerializeField]
    private player player;

    private void Awake()
    {

        player = transform.parent.GetComponent<pTorsoAim>().GetPlayer();

    }

    // Update is called once per frame
    void Update () {

        if (player.GetActionState() == PlayerActionState.ATTACKING)
        {

            //Aim player at mouse
            //which direction is up
            Vector3 upAxis = new Vector3(0, 0, 1);
            Vector3 mouseScreenPosition = Input.mousePosition;

            //set mouses z to your targets
            mouseScreenPosition.z = transform.position.z;

            Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            transform.LookAt(mouseWorldSpace, upAxis);

            //zero out all rotations except the axis I want
            this.transform.eulerAngles = new Vector3(0, 0, (-transform.eulerAngles.z + 90) + mouseWorldSpace.z);

        }

    }

}
