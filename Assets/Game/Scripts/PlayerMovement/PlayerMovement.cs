using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerManager playerManager;
    private Vector3 moveDirection;
    private Transform cameraGameObject;
    private Rigidbody playerRigidbody;

    [Header("Falling and Landing")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 0.5f;
    public LayerMask groundLayer;

    [Header("Movement flags")]
    public bool isMoving;
    public bool isSprinting;    

    [Header ("Movement values")]
    public float movementSpeed = 2f;
    public float rotationSpeed = 13f;

    public float sprintingSpeed = 7f;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraGameObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        if (playerManager.isInteracting) return;
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        //moveDirection = cameraGameObject.forward * inputManager.verticalInput; // old code
        moveDirection = new Vector3(cameraGameObject.forward.x, 0f, cameraGameObject.forward.z) * inputManager.verticalInput; // Fix bug: if the camera is above the player's head, the player runs slowly, if the camera is above the player's head, the player runs faster
        moveDirection = moveDirection + cameraGameObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else if (inputManager.movementAmount >= 0.5f)
        {
            moveDirection = moveDirection * movementSpeed;
            isMoving = true;
        }
        else if (inputManager.movementAmount >= 0f)
        {
            isMoving = false;
        }

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraGameObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraGameObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position; // 13:40

    }
}
