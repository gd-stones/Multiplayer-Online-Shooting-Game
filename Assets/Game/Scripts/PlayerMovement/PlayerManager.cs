using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerMovement playerMovement;
    private CameraManager cameraManager;
    private Animator animator;
    public bool isInteracting;

    private PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void Start()
    {
        if (!view.IsMine)
        {
            Destroy(GetComponentInChildren<CameraManager>().gameObject);
        }
    }

    private void Update()
    {
        if (view.IsMine)
            inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
            playerMovement.HandleAllMovement();
    }

    private void LateUpdate()
    {
        if (view.IsMine)
        {
            cameraManager.HandleAllCameraMovement();

            isInteracting = animator.GetBool("isInteracting");
            playerMovement.isJumping = animator.GetBool("isJumping");
            animator.SetBool("isGrounded", playerMovement.isGrounded);
        }
    }
}
