using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charTorsoAim : MonoBehaviour {

    [Header("List of Bones to Rotate")]
    public GameObject[] bones;

    [Header("Target to Look At")]
    public TARGETTYPE targetType;
    public Transform target;

    private bool facingRight;
    private List<float> boneInitRotation;

    private void Start()
    {

        boneInitRotation = new List<float>();
        
        if(bones.Length > 0)
        {

            for(int i = 0; i < bones.Length; i++)
            {

                boneInitRotation.Add(bones[i].transform.rotation.z);

            }

        }

    }

    private void Update()
    {

        Vector3 upAxis = new Vector3(0, 1, 0);

            if (targetType == TARGETTYPE.OBJECT)
            {

                Vector3 targetPos = target.position;

                if ((targetPos.x - this.transform.position.x) < -1 || 
                    (targetPos.x - this.transform.position.x) > 1)
                {

                /*
                for (int i = 0; i < bones.Length; i++)
                {

                    //bones[i].transform.LookAt(targetPos, upAxis);

                    if ((targetPos.x - this.transform.position.x) < -1)
                    {

                        facingRight = false;

                        //bones[i].transform.eulerAngles = new Vector3(0, 0, 
                            //(-transform.eulerAngles.z + boneInitRotation[i] + 270) / (i + 1));

                    }
                    else if ((targetPos.x - this.transform.position.x) > 1)
                    {

                        facingRight = true;

                        //bones[i].transform.eulerAngles = new Vector3(0, 0, 
                            //(-transform.eulerAngles.z + boneInitRotation[i] + 90) / (i + 1));


                    }

                }
                */

                if ((targetPos.x - this.transform.position.x) < -1)
                {

                    facingRight = false;

                }
                else if ((targetPos.x - this.transform.position.x) > 1)
                {

                    facingRight = true;


                }


            }

            }
            else if (targetType == TARGETTYPE.MOUSE)
            {

                Vector3 mouseScreenPosition = Input.mousePosition;

                //set mouses z to your targets
                mouseScreenPosition.z = transform.position.z;

                Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                if ((mouseWorldSpace.x - this.transform.position.x) < -1 || (mouseWorldSpace.x - this.transform.position.x) > 1)
                {

                    for (int i = 0; i < bones.Length; i++)
                    {

                        //bones[i].transform.LookAt(mouseWorldSpace, upAxis);

                        if ((mouseWorldSpace.x - this.transform.position.x) < -1)
                        {

                            facingRight = false;

                            //bones[i].transform.eulerAngles = new Vector3(0, 0,
                                //(-transform.eulerAngles.z + boneInitRotation[i] /*+ 270*/) / (i + 1));

                        }
                        else if ((mouseWorldSpace.x - this.transform.position.x) > 1)
                        {

                            facingRight = true;

                            //bones[i].transform.eulerAngles = new Vector3(0, 0,
                                //(-transform.eulerAngles.z + boneInitRotation[i] /*+ 90*/) / (i + 1));


                        }

                    }

                }

            }
            else if (target == null)
            {

                return;

            }
            else if (targetType == TARGETTYPE.NONE)
            {

                Destroy(this);
                //return;

            }

    }

    public void SetTarget(Transform set)
    {

        target = set;

    }

    public Transform GetTarget()
    {

        return target;

    }

    public void SetTargetType(TARGETTYPE set)
    {

        targetType = set;

    }

    public string GetTargetType()
    {

        if(targetType == TARGETTYPE.MOUSE)
        {
            
            return "mouse";

        }
        else if(targetType == TARGETTYPE.OBJECT)
        {

            return "object";

        }
        else
        {

            return "none";
        }

    }

    public void SetBones(GameObject[] set)
    {

        for(int i = 0; i < set.Length; i++)
        {

            bones[i] = set[i];

        }

    }

    public GameObject[] GetBones()
    {

        return bones;

    }

    public void SetFacingRight(bool set)
    {

        facingRight = set;

    }

    public bool GetFacingRight()
    {

        return facingRight;

    }

}

public enum TARGETTYPE
{

    NONE,
    MOUSE,
    OBJECT

}
