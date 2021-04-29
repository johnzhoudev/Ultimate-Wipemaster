using UnityEngine;

public enum LaunchStatus
{
    Normal,
    BeingLaunched,
    Launched
}

public class RigidbodyController : MonoBehaviour
{

    public GameManager gameManager;
    public Rigidbody playerRigidbody;
    public Transform playerTransform;
    public Transform groundCheckTransform;
    public CapsuleCollider playerCollider;
    public LayerMask environmentLayers;
    public float speed;
    public float launchedSpeed;
    public float gravity = -9.81f;
    public float groundCheckRadius;
    public float jumpDistance;
    public float horizontalLaunchSpeed;
    public float verticalLaunchSpeed;

    bool isMovementEnabled;
    LaunchStatus launchStatus;
    Vector3 punchingGloveLaunchDirection = new Vector3(0, 0, -1);

    const string ROTATING_BAR_TAG = "RotatingBar";
    const string GROUND_TAG = "Ground";
    const string PUNCHING_GLOVE_TAG = "PunchingGlove";

    void FixedUpdate()
    {
        if (!isMovementEnabled) { return; }

        bool isGrounded = isRigidbodyGrounded();

        // Get the inputs
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal")) * speed;
        Vector3 currentVelocity = getCurrentTranslationalVelocity();

        // Handle launch statuses
        if (launchStatus == LaunchStatus.BeingLaunched && !isGrounded) { launchStatus = LaunchStatus.Launched; }
        else if (launchStatus == LaunchStatus.Launched && isGrounded) { launchStatus = LaunchStatus.Normal; }

        // Add horizontal movements
        if (launchStatus == LaunchStatus.Launched)
        {
            playerRigidbody.AddRelativeForce(targetVelocity * launchedSpeed, ForceMode.Acceleration);
        } else
        {
            playerRigidbody.AddRelativeForce(targetVelocity - currentVelocity, ForceMode.VelocityChange);
        }

        // add gravity
        playerRigidbody.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

        // Jump check: If grounded and spacebar
        if (isGrounded && Input.GetButton("Jump"))
        {
            Vector3 velocity = playerRigidbody.velocity;
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpDistance);
            playerRigidbody.velocity = velocity;
        }
    }

    bool isRigidbodyGrounded()
    {
        return (Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, environmentLayers));
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

    void OnCollisionEnter(Collision collision)
    {
        Vector3 normalDirection;
        switch(collision.gameObject.tag)
        {
            case GROUND_TAG:
                //GameObject.Find("GameManager").GetComponent<GameManager>().RestartLevel(Checkpoint.Start);
                gameManager.onWipeout();
                gameManager.RestartLevel(Checkpoint.Start);
                break;
            case ROTATING_BAR_TAG:
                if (collision.contactCount == 0) { return; }
                launchStatus = LaunchStatus.BeingLaunched;
                normalDirection = collision.GetContact(0).normal;
                launchPlayer(normalDirection);
                break;
            case PUNCHING_GLOVE_TAG:
                if (collision.contactCount == 0) { return; }
                launchStatus = LaunchStatus.BeingLaunched;
                launchPlayer(punchingGloveLaunchDirection);
                break;
        }
    }

    void launchPlayer(Vector3 flatDirection)
    {
        playerRigidbody.AddForce(flatDirection.normalized * horizontalLaunchSpeed + 
                                Vector3.up * verticalLaunchSpeed, ForceMode.VelocityChange);
    }

    public void disableMovement() { isMovementEnabled = false; }

    public void enableMovement() { isMovementEnabled = true; }

}
