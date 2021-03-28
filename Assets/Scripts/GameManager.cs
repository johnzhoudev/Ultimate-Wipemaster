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

    // UI Related imports
    public UIManager uiManager;

    public void onWipeout()
    {
        uiManager.openWipeoutScreen();
    }

    public void RestartLevel(Checkpoint checkpoint)
    {
        playerController.teleport(startLocation, startRotation);
    }

}
