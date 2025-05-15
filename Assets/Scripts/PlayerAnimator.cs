using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    private Vector2 input;
    private float lastFacingDirection = -1; // Default to facing left
    private bool isJumping = false;

    public LayerMask groundLayer;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
        Animate();

        // Handle jumping
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            anim.SetBool("IsJumping", true);
        }
    }

    void ProcessInputs()
    {
        // Only capture horizontal input for animation
        float moveX = Input.GetAxisRaw("Horizontal");
        input = new Vector2(moveX, 0).normalized; // Ignore vertical input
    }

    void Animate()
    {
        // Use only horizontal movement for animation
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveMagnitude", Mathf.Abs(input.x)); // Use absolute value to ensure positive magnitude

        // Update facing direction if moving
        const float threshold = 0.1f;
        if (Mathf.Abs(input.x) > threshold)
        {
            lastFacingDirection = input.x > 0 ? -1 : 1; // Flip sprite based on direction
        }

        // Set facing and last movement direction
        anim.SetFloat("LastMoveX", input.x);
        anim.SetFloat("FacingX", Mathf.Abs(input.x) > threshold ? (input.x > 0 ? -1 : 1) : lastFacingDirection);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jumping state on ground contact
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            isJumping = false;
            anim.SetBool("IsJumping", false);
        }
    }
}
