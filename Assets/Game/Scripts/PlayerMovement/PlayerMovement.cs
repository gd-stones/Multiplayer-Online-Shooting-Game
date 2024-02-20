using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputManager inputManager;
    private Vector3 moveDirection;
    private Transform cameraGameObject;
    private Rigidbody playerRigidbody;
    public float movementSpeed = 2f;
    public float rotationSpeed = 13f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraGameObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
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
        moveDirection = moveDirection * movementSpeed;

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
}
