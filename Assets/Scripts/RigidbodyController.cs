using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Transform playerTransform;
    public CapsuleCollider playerCollider;
    public float speed;
    public float gravity = -9.81f;

    public float groundCheckDistance;
    public float jumpDistance;
    float distanceToGround;

    private void Start()
    {
        distanceToGround = playerCollider.bounds.extents.y;
    }

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
        if (Physics.Raycast(playerTransform.position, Vector3.down, distanceToGround + groundCheckDistance) &&
            Input.GetButton("Jump"))
        {
            playerRigidbody.AddForce(Vector3.up * Mathf.Sqrt(-2f * gravity * jumpDistance), ForceMode.VelocityChange);
        }
    }

    Vector3 getCurrentTranslationalVelocity()
    {
        Vector3 currentRelativeVelocity = playerTransform.InverseTransformDirection(playerRigidbody.velocity);
        currentRelativeVelocity.y = 0;
        return currentRelativeVelocity;
    }

}
