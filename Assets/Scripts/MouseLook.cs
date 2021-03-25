﻿using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseXSensitivity = 100f;
    public float mouseYSensitivity = 100f;
    float xRotation = 0f; // Rotation in euler angles (degrees) relative to parent
    public Transform player;
    public Rigidbody playerRigidbody;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse x movement and rotate playerBody
        float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity;
        player.Rotate(Vector3.up * mouseX);
        //playerRigidbody.rotation *= Quaternion.Euler(Vector3.up * mouseX);

        // Get Mouse Y movement and rotate camera
        // Easier to just set the rotation instead of checking to see if past 90 degrees or -90 degrees and adjusting accordingly
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamping
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // rotate camera instead of player
    }
}
