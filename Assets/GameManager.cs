using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Checkpoint
{
    Start = 0,
}

public class GameManager : MonoBehaviour
{
    public RigidbodyController playerController;
    public Vector3 startLocation;
    public Vector3 startRotation;

    public void RestartLevel(Checkpoint checkpoint)
    {
        playerController.teleport(startLocation, startRotation);
    }

}
