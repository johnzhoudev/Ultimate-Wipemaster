using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Checkpoint
{
    Start = 0,
}

// GameManager requires the RigidbodyController of the player
[RequireComponent(typeof(RigidbodyController))]
public class GameManager : MonoBehaviour
{
    public RigidbodyController playerController;
    public Vector3 startLocation;

    void Start()
    {
        //playerController = GameObject.Find("Player").GetComponent<RigidbodyController>();
    }

    public void RestartLevel(int checkpoint)
    {
        playerController.teleport(startLocation);
    }

}
