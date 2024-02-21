using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerMovement playerMovement;
    private CameraManager cameraManager;
    private Animator animator;

    public bool isInteracting;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();   
        isInteracting = animator.GetBool("isInteracting");
    }
}
