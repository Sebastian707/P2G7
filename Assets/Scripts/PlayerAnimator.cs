using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    private Vector2 input;
    private Vector2 lastMoveDirection;

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

        // Detect jump input
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            anim.SetBool("IsJumping", true);
        }
    }

    void ProcessInputs()
    {
        // Capture horizontal and vertical input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Update the last move direction when the player stops moving
        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input = new Vector2(moveX, moveY).normalized;
    }

        /*
    void Animate()
        {
            anim.SetFloat("MoveX", input.x);    // Used for horizontal movement animation
            anim.SetFloat("MoveMagnitude", input.magnitude);  // Controls the walking/running animation speed
            anim.SetFloat("LastMoveX", lastMoveDirection.x);  // Last horizontal movement direction

            // Set the facing direction for animation based on horizontal input
            if (input.x > 0)
            {
                anim.SetFloat("FacingX", -1); // Facing right
            }
            else if (input.x < 0)
            {
                anim.SetFloat("FacingX", 1);  // Facing left
            }

        // If you plan to use vertical movement, uncomment the line below to use "MoveY"
        // anim.SetFloat("MoveY", input.y);  // For vertical animations like jumping, falling, etc.
    }
    */
        
        private float lastFacingDirection = 1; // Default to facing left initially

        void Animate()
        {
            // Set animation parameters
            anim.SetFloat("MoveX", input.x);
            anim.SetFloat("MoveMagnitude", input.magnitude);

            // Add a threshold for detecting significant horizontal movement
            const float threshold = 0.1f;
            if (Mathf.Abs(input.x) > threshold)
            {
                lastMoveDirection.x = input.x;
                lastFacingDirection = input.x > 0 ? -1 : 1; // Update to the current significant direction
            }

            anim.SetFloat("LastMoveX", lastMoveDirection.x);

            // Handle facing direction explicitly
            float facingDirection;
            if (Mathf.Abs(input.x) > threshold) // When actively moving
            {
                facingDirection = input.x > 0 ? -1 : 1; // Update based on input
            }
            else
            {
                facingDirection = lastFacingDirection; // Default to the last direction for idle/jump
            }

            anim.SetFloat("FacingX", facingDirection);
        }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump state when player lands on ground
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            isJumping = false;
            anim.SetBool("IsJumping", false);
        }
    }
}
