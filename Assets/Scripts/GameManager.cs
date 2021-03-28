using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Checkpoint
{
    Start = 0,
}

public class GameManager : MonoBehaviour
{
    const float WIPEOUT_SCREEN_JUMP_DELAY = 0.2f;

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
        if (isWipeoutScreenEnabled && Input.GetButton("Jump")) { StartCoroutine(wipeoutEnd()); }
    }

    public void onWipeout()
    {
        uiManager.openWipeoutScreen();
        playerController.disableMovement();
        isWipeoutScreenEnabled = true;
    }

    IEnumerator wipeoutEnd()
    {
        uiManager.closeWipeoutScreen();
        isWipeoutScreenEnabled = false;

        yield return new WaitForSeconds(WIPEOUT_SCREEN_JUMP_DELAY);

        playerController.enableMovement();
    }

    public void RestartLevel(Checkpoint checkpoint)
    {
        playerController.teleport(startLocation, startRotation);
    }

}
