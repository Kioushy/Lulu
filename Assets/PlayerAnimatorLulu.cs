using UnityEngine;

public class PlayerAnimatorLulu : MonoBehaviour
{
    [Header("References")]
    public Animator animator;           // Ton Animator
    public PlayerController controller; // Référence à ton PlayerController

    void Update()
    {
        if (controller == null || animator == null) return;
        animator.SetBool("isMoving", controller.moveInput.magnitude > 0.01f);

        // On envoie la direction pour choisir l'animation
        animator.SetFloat("MoveX", controller.moveInput.x);
        animator.SetFloat("MoveY", controller.moveInput.y);
    }
}
