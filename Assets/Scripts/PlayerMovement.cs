using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;
    public Transform playerTransform;
    public Transform groundCheckTransform;

    Vector3 velocity; // instantiated to 000

    public float playerSpeed;
    public float groundResetVelocity = -2f;
    public float groundCheckDistance = 1f;
    public float gravity = -9.81f;
    public float jumpHeight;

    // Ground Layer Mask
    public LayerMask groundMask;

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {

        // Get movement x and y vector
        Vector3 movement = playerTransform.right * Input.GetAxis("Horizontal") + playerTransform.forward * Input.GetAxis("Vertical");

        playerController.Move(movement * playerSpeed * Time.deltaTime);

        // Ground check - reset velocity
        // isGrounded = Physics.Raycast(groundCheckTransform.position, Vector3.down, groundCheckDistance);
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckDistance, groundMask);

        // Debug.Log(isGrounded ? "GROUNDED" : "NOT_GROUNDED");

        if (isGrounded && velocity.y < 0) { velocity.y = groundResetVelocity; }

        // Apply jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // physics checks out
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }
        else
        {
            // apply gravity, as the gravitational force always applies
            velocity.y += gravity * Time.deltaTime;
        }

        //Debug.Log(velocity.y);
        playerController.Move(((movement * playerSpeed) + velocity) * Time.deltaTime); // insert change in Y
    }
}
