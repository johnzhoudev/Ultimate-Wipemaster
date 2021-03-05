using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public Transform playerTransform;
    bool toggleCrouchButton;
    bool holdCrouchButton;
    bool isCrouched;

    // toggleCrouch method will modify isCrouched
    void toggleCrouch()
    {
        Vector3 currentTransformScale = playerTransform.localScale;
        Vector3 currentTransformPosition = playerTransform.localPosition;

        if (isCrouched)
        {
            // uncrouch
            currentTransformPosition.y += currentTransformScale.y;
            currentTransformScale.y *= 2;
        } else
        {
            // crouch
            currentTransformScale.y /= 2;
            currentTransformPosition.y -= currentTransformScale.y;
        }

        playerTransform.localScale = currentTransformScale;
        playerTransform.localPosition = currentTransformPosition;
        isCrouched = !isCrouched;
    }

    // Update is called once per frame
    void Update()
    {

        toggleCrouchButton = Input.GetButtonDown("ToggleCrouch");
        bool prevHoldCrouch = holdCrouchButton;
        holdCrouchButton = Input.GetButton("Crouch");

        if (toggleCrouchButton && !holdCrouchButton)
        {
            toggleCrouch();
        }
        else if (holdCrouchButton && !isCrouched)
        {
            toggleCrouch();
        }
        else if (prevHoldCrouch && !holdCrouchButton && isCrouched)
        {
            toggleCrouch();
        }
        
    }
}
