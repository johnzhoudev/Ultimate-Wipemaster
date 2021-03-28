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

    bool isWipeoutScreenEnabled;

    private void Start()
    {
        playerController.enableMovement();
    }

    void Update()
    {
        if (isWipeoutScreenEnabled && Input.GetButton("Jump")) { wipeoutEnd(); }
    }

    public void onWipeout()
    {
        uiManager.openWipeoutScreen();
        playerController.disableMovement();
        isWipeoutScreenEnabled = true;
    }

    void wipeoutEnd()
    {
        uiManager.closeWipeoutScreen();
        playerController.enableMovement();
        isWipeoutScreenEnabled = false;
    }

    public void RestartLevel(Checkpoint checkpoint)
    {
        playerController.teleport(startLocation, startRotation);
    }

}
