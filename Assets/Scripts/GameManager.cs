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
    const float CAMERA_STRAIGHT_AHEAD = 0f;

    public RigidbodyController playerController;
    public MouseLook playerMouseLook;
    public PlayerView playerView;
    public Vector3 startLocation;
    public Vector3 startRotation;
    public float startCameraVerticalRotation;
    [Tooltip("Disables ground endgame")]
    public bool developmentMode = false;

    // UI Related imports
    public UIManager uiManager;

    // Sound Related Imports
    public SoundManager soundManager;

    bool isWipeoutScreenEnabled;

    private void Start()
    {
        playerController.enableMovement();
    }

    void Update()
    {
        if (isWipeoutScreenEnabled && Input.GetButton("Jump")) { StartCoroutine(wipeoutEnd()); }
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }

        bool isButtonInView = playerView.isButtonTriggerInView();
        if (!uiManager.isButtonTextActive() && isButtonInView) 
        { 
                uiManager.openButtonText(); 
        }
        else if (uiManager.isButtonTextActive() && !isButtonInView)
        {
            uiManager.closeButtonText();
        }

        if (isButtonInView && Input.GetButtonDown("Interact"))
        {
            Debug.Log("Beat the Game!");
        }
    }

    public void onWipeout()
    {
        if (developmentMode) { return; }
        soundManager.playSound("Splash");
        soundManager.playSound("AirHorn");
        uiManager.openWipeoutScreen();
        disableMovement();
        isWipeoutScreenEnabled = true;
    }

    IEnumerator wipeoutEnd()
    {
        if (developmentMode) { yield break; }
        soundManager.stopSound("AirHorn");
        uiManager.closeWipeoutScreen();
        isWipeoutScreenEnabled = false;
        playerMouseLook.enableMovement();

        yield return new WaitForSeconds(WIPEOUT_SCREEN_JUMP_DELAY);

        playerController.enableMovement();
    }

    public void RestartLevel(Checkpoint checkpoint)
    {
        if (developmentMode) { return; }
        playerController.teleport(startLocation, startRotation);
        playerMouseLook.setVerticalCameraRotation(CAMERA_STRAIGHT_AHEAD);
    }

    void disableMovement()
    {
        playerController.disableMovement();
        playerMouseLook.disableMovement();
    }

    void enableMovement()
    {
        playerController.enableMovement();
        playerMouseLook.enableMovement();
    }

}
