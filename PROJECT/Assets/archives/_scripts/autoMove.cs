using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoMove : MonoBehaviour {

    public float moveSpeed;

    public List<GameObject> activePieces;

    private void Awake()
    {

        activePieces = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Active Pieces: " + activePieces.Count);

        if (activePieces.Count > 0)
        {

            for (int i = 0; i < activePieces.Count; i++)
            {

                activePieces[i].transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            }

        }

    }

    public void addToActive(GameObject trackPiece)
    {

        if (trackPiece != null)
        {

            activePieces.Add(trackPiece);

        } else
        {

            Debug.Log("Cannot Add New Piece: Null Exception!");

        }

    }

    public void removeFromActive(GameObject trackPiece)
    {

        activePieces.Remove(trackPiece);

    }

}
