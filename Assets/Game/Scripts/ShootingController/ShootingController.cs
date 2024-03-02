using UnityEngine;

public class ShootingController : MonoBehaviour
{
    private Animator animator;
    private InputManager inputManager;
    private PlayerMovement playerMovement;

    [Header("Shooting Var")]
    public Transform firePoint;
    public float fireRate = 0f;
    public float fireRange = 100f;
    public float fireDamage = 15f;
    private float nextFireTime = 0f;

    [Header("Reloading")]
    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1.5f;

    [Header("Shooting Flags")]
    public bool isShooting;
    public bool isWalking;
    public bool isShootingInput;
    public bool isReloading = false;

    [Header("Sound Effects")]
    public AudioSource soundAudioSource;
    public AudioClip shootingSoundClip;
    public AudioClip reloadingSoundClip;

    private void Start()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (isReloading || playerMovement.isSprinting)
        {
            animator.SetBool("Shoot", false);
            animator.SetBool("ShootingMovement", false);
            animator.SetBool("ShootWalk", false);
            return;
        }

        isWalking = playerMovement.isMoving;
        isShootingInput = inputManager.fireInput;

        if (isShootingInput && isWalking)
        {
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                Shoot();
                animator.SetBool("ShootWalk", true);
            }

            animator.SetBool("Shoot", false);
            animator.SetBool("ShootingMovement", true);
            isShooting = true;
        }
        else if (isShootingInput)
        {
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                Shoot();
            }

            animator.SetBool("ShootWalk", false);
            animator.SetBool("Shoot", true);
            animator.SetBool("ShootingMovement", false);
            isShooting = true;
        }
        else
        {
            animator.SetBool("ShootingMovement", false);
            animator.SetBool("Shoot", false);
            animator.SetBool("ShootWalk", false);
            isShooting = false;
        }

        if (inputManager.reloadInput && currentAmmo < maxAmmo)
        {
            Reload(); 
        }
    }

    private void Shoot()
    {
        if (currentAmmo > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, fireRange))
            {
                Debug.Log(hit.transform.name);

                // extract information from hit
                // apply damage to target
            }

            // play muzzle flash
            soundAudioSource.PlayOneShot(shootingSoundClip);
            currentAmmo--;
        }
        else Reload();
    }

    private void Reload()
    {
        if (!isReloading && currentAmmo < maxAmmo)
        {
            if (isShootingInput && isWalking)
            {
                animator.SetTrigger("ShootReload");
            }
            else
            {
                animator.SetTrigger("Reload");
            }

            isReloading = true;
            soundAudioSource.PlayOneShot(reloadingSoundClip);
            Invoke("FinishReloading", reloadTime);
        }
    }

    private void FinishReloading()
    {
        currentAmmo = maxAmmo;
        isReloading = false;

        if (isShootingInput && isWalking)
        {
            animator.ResetTrigger("ShootReload");
        }
        else
        {
            animator.ResetTrigger("Reload");
        }
    }
}
