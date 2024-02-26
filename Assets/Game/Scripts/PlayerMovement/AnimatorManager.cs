using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    private int horizontalValue;
    private int verticalValue;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontalValue = Animator.StringToHash("Horizontal");
        verticalValue = Animator.StringToHash("Vertical");
    }

    public void ChangeAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        float snappedHorizontalMovement;
        float snappedVerticalMovement;

        #region Snapped Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snappedHorizontalMovement = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontalMovement = 1f; 
        }
        else if (horizontalMovement < 0f  && horizontalMovement > -0.55f)
        {
            snappedHorizontalMovement = -0.55f;
        }
        else if (horizontalMovement < -0.55f)
        {
             snappedHorizontalMovement = -1f;
        }
        else
        {
             snappedHorizontalMovement = 0f;
        }
        #endregion

        #region Snapped Vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVerticalMovement = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVerticalMovement = 1f;
        }
        else if (verticalMovement < 0f && verticalMovement > -0.55f)
        {
            snappedVerticalMovement = -0.55f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVerticalMovement = -1f;
        }
        else
        {
            snappedVerticalMovement = 0f;
        }
        #endregion

        if (isSprinting)
        {
            snappedHorizontalMovement = horizontalMovement;
            snappedVerticalMovement = 2; 
        }

        animator.SetFloat(horizontalValue, snappedHorizontalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat(verticalValue, snappedVerticalMovement, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnim(string targerAnim, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targerAnim, 0.2f);
    }
}
