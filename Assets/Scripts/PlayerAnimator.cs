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
            // Optional: Apply jump force if physics-based jump

            /*
            // Set the jump direction animation based on movement
            if (input.x > 0)
            {
                anim.SetBool("IsJumpingRight", true);
                anim.SetBool("IsJumpingLeft", false);
            }
            else if (input.x < 0)
            {
                anim.SetBool("IsJumpingLeft", true);
                anim.SetBool("IsJumpingRight", false);
            }
            else
            {
                // If no horizontal movement, keep the current facing direction
                anim.SetBool("IsJumpingRight", false);
                anim.SetBool("IsJumpingLeft", false);
            }
            */
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input = new Vector2(moveX, moveY).normalized;
    }

    void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);

        if (input.x > 0)
        {
            anim.SetFloat("FacingX", -1);
        }
        else if (input.x < 0)
        {
            anim.SetFloat("FacingX", 1);
        }
       
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) != 0) // Or your terrain layer
        {
            isJumping = false;
            anim.SetBool("IsJumping", false);
            //anim.SetBool("IsJumpingLeft", false);
            //anim.SetBool("IsJumpingRight", false);
        }
    }

}
