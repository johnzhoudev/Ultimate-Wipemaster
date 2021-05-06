using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Checkpoint
{
    Start = 0,
    RotatingBar = 1,
    PunchingWall = 2,
    Balls = 3
}


public class GameManager : MonoBehaviour
{

    const float WIPEOUT_SCREEN_JUMP_DELAY = 0.1f;
    const float CAMERA_STRAIGHT_AHEAD = 0f;

    public RigidbodyController playerController;
    public MouseLook playerMouseLook;
    public PlayerView playerView;

    // Checkpoints
    [System.Serializable]
    public struct CheckpointData
    {
        public Checkpoint checkpoint;
        public Vector3 location;
        public Vector3 rotation;
    }

    public CheckpointData[] _checkpoints;
    Dictionary<Checkpoint, CheckpointData> checkpoints = new Dictionary<Checkpoint, CheckpointData>();

    public float startCameraVerticalRotation;
    public float stopMusicButtonHoldTime;

    [Tooltip("Disables ground endgame")]
    public bool developmentMode = false;

    // UI Related imports
    public UIManager uiManager;

    // Sound Related Imports
    public SoundManager soundManager;

    // Private members
    Checkpoint currentCheckpoint = Checkpoint.Start;
    float timeChangeMusicHeldDown = 0f;

    void Start()
    {
        playerController.enableMovement();
        uiManager.setScreenState(ScreenState.Normal);
        if (!developmentMode) { soundManager.startMusic(); }
        loadCheckpoints();
    }

    void loadCheckpoints() 
    { 
        for (int i = 0; i < _checkpoints.Length; ++i)
        {
            checkpoints.Add(_checkpoints[i].checkpoint, _checkpoints[i]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        if (Input.GetButton("ChangeMusic")) 
        {
            timeChangeMusicHeldDown += Time.deltaTime;
            if (timeChangeMusicHeldDown > stopMusicButtonHoldTime)
            {
                Debug.Log("Stop Music");
                soundManager.stopMusic();
            }
        } else
        {
            timeChangeMusicHeldDown = 0f;
        }

        switch(uiManager.getScreenState())
        {
            case ScreenState.WipeoutScreen:
                if (Input.GetButtonDown("Jump")) { StartCoroutine(wipeoutEnd()); }
                break;
            case ScreenState.EndGameScreen:
                if (Input.GetButtonDown("ChangeMusic")) { soundManager.nextSong(); }
                if (Input.GetButtonDown("Jump")) { StartCoroutine(endGameScreenEnd()); }
                break;
            case ScreenState.Normal:
                if (Input.GetButtonDown("ChangeMusic")) { soundManager.nextSong(); }
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
                    endGame();
                }
                break;
        }
    }

    public void setCheckpoint(Checkpoint checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void endGame()
    {
        soundManager.playSound("AirHorn");
        uiManager.openEndGameScreen();
        disableMovement();
    }
    public void onWipeout()
    {
        if (developmentMode) { return; }
        soundManager.stopMusic();
        RestartLevel(currentCheckpoint);
        soundManager.playSound("Splash");
        soundManager.playSound("AirHorn");
        uiManager.openWipeoutScreen();
        disableMovement();
    }

    IEnumerator wipeoutEnd()
    {
        if (developmentMode) { yield break; }
        soundManager.startMusic();
        soundManager.stopSound("AirHorn");
        uiManager.closeWipeoutScreen();
        playerMouseLook.enableMovement();

        yield return new WaitForSeconds(WIPEOUT_SCREEN_JUMP_DELAY);

        playerController.enableMovement();
    }
    IEnumerator endGameScreenEnd()
    {
        setCheckpoint(Checkpoint.Start);
        RestartLevel(Checkpoint.Start);
        soundManager.stopSound("AirHorn");
        uiManager.closeEndGameScreen();
        playerMouseLook.enableMovement();

        yield return new WaitForSeconds(WIPEOUT_SCREEN_JUMP_DELAY);

        playerController.enableMovement();
    }

    public void RestartLevel(Checkpoint checkpoint)
    {
        CheckpointData checkpointData = checkpoints[checkpoint];
        playerController.teleport(checkpointData.location, checkpointData.rotation);
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

    public bool isCheckpointGreaterThanCurrentCheckpoint(Checkpoint newCheckpoint)
    {
        return (int)currentCheckpoint < (int)newCheckpoint;
    }

}
