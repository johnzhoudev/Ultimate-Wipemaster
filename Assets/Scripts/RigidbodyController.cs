using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Transform playerTransform;
    public float speed;

    void FixedUpdate()
    {
        // Get the inputs
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal")) * speed;
        Vector3 currentVelocity = playerTransform.InverseTransformDirection(playerRigidbody.velocity);

        playerRigidbody.AddRelativeForce(targetVelocity - currentVelocity, ForceMode.VelocityChange);
    }

}
