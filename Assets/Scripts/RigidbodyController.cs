using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public Transform playerTransform;
    public float speed;
    public float gravity = -9.81f;

    void FixedUpdate()
    {
        // Get the inputs
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal")) * speed;
        Vector3 currentVelocity = getCurrentTranslationalVelocity();

        playerRigidbody.AddRelativeForce(targetVelocity - currentVelocity, ForceMode.VelocityChange);

        // add gravity
        playerRigidbody.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
    }

    Vector3 getCurrentTranslationalVelocity()
    {
        Vector3 currentRelativeVelocity = playerTransform.InverseTransformDirection(playerRigidbody.velocity);
        currentRelativeVelocity.y = 0;
        return currentRelativeVelocity;
    }

}
