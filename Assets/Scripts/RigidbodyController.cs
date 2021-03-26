using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Transform playerTransform;
    public Transform groundCheckTransform;
    public CapsuleCollider playerCollider;
    public LayerMask environmentLayers;
    public float speed;
    public float gravity = -9.81f;

    public float groundCheckRadius;
    public float jumpDistance;

    void FixedUpdate()
    {
        // Get the inputs
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal")) * speed;
        Vector3 currentVelocity = getCurrentTranslationalVelocity();

        // Add horizontal movements
        playerRigidbody.AddRelativeForce(targetVelocity - currentVelocity, ForceMode.VelocityChange);

        // add gravity
        playerRigidbody.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

        // Jump check: If grounded and spacebar
        if (Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, environmentLayers) &&
            Input.GetButton("Jump"))
        {
            Vector3 velocity = playerRigidbody.velocity;
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpDistance);
            playerRigidbody.velocity = velocity;
        }
    }

    Vector3 getCurrentTranslationalVelocity()
    {
        Vector3 currentRelativeVelocity = playerTransform.InverseTransformDirection(playerRigidbody.velocity);
        currentRelativeVelocity.y = 0;
        return currentRelativeVelocity;
    }

    public void teleport(Vector3 coordinates)
    {
        playerTransform.position = coordinates;
    }

    public void teleport(Vector3 worldCoordinates, Vector3 worldRotation)
    {
        playerTransform.position = worldCoordinates;
        playerTransform.rotation = Quaternion.Euler(worldRotation);
    }

}
