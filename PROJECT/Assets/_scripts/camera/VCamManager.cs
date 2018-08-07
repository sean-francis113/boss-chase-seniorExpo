using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamManager : MonoBehaviour {

    public static VCamManager instance;

    public GameObject vCam;

    private Cinemachine.CinemachineVirtualCamera virtualCamera;

    private void Awake()
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

    // Use this for initialization
    void Start () {

        if (vCam)
        {

            virtualCamera = vCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();

        }

	}

    public void GetPlayer()
    {

        virtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;

    }

}
