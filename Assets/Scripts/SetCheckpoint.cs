using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpoint : MonoBehaviour
{
    public Checkpoint checkpoint;
    public GameManager gameManager;
    Collider checkpointCollider;

    void Start()
    {
        checkpointCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameManager.isCheckpointGreaterThanCurrentCheckpoint(checkpoint))
        {
            gameManager.setCheckpoint(checkpoint);
        }
    }
}
