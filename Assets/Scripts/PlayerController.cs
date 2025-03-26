using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float doubleJumpForce = 2.5f; 
    [SerializeField] private int maxDoubleJumps = 1;
    [SerializeField] private float varJump = 0.5f;

    [SerializeField] private float coyoteTime = 0.2f;
    private float CoyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private new Collider2D collider;
    private Rigidbody2D Rb;

   
    [SerializeField] private bool canDoubleJump = false;
    private int doubleJumpsLeft = 0;

    [Header("Dash settings")]
    [SerializeField] private bool CanDash = false;
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    public int maxDashCharges = 3;
    public float chargeRestoreRate = 1f;

    private int currentDashCharges;
    private bool isDashing = false;
    private float dashStartTime;

    public TextMeshProUGUI dashText;

    private float lastHorizontalInput = 0; 
    private bool isFacingRight = true; 

    private void OnValidate()
    {
        collider = GetComponent<Collider2D>();
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentDashCharges = maxDashCharges;
        if (dashText != null)
        {
            dashText.text = "Dashes: " + currentDashCharges + " / " + maxDashCharges;
        }
    }

    private void Update()
    {
        if (IsGrounded())
        {
            CoyoteTimeCounter = coyoteTime;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            jumpBufferCounter = jumpBufferTime;

        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }


        float horizontalMovement = Input.GetAxis("Horizontal");
        if (horizontalMovement > 0 && !isFacingRight) Flip();
        else if (horizontalMovement < 0 && isFacingRight) Flip();

        if (jumpBufferCounter > 0f)
        {
            if (CoyoteTimeCounter > 0f)
            {
                Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, jumpForce);
                doubleJumpsLeft = maxDoubleJumps;
                jumpBufferCounter = 0f;

            }
            else if (canDoubleJump && doubleJumpsLeft > 0)
            {
                Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, doubleJumpForce);
                doubleJumpsLeft--;
              
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && Rb.linearVelocity.y > 0)
        {
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, (float)(Rb.linearVelocity.y * varJump));

            CoyoteTimeCounter = 0f;
        }

  
        if (CanDash && currentDashCharges > 0 && Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            Dash();
        }

        if (!isDashing && currentDashCharges < maxDashCharges)
        {
            if (Time.time >= dashStartTime + chargeRestoreRate)
            {
                currentDashCharges++;
                dashStartTime = Time.time;
            }
        }

        if (CanDash && dashText != null)
        {
            dashText.text = "Dashes: " + currentDashCharges + " / " + maxDashCharges;
        }
        else if (dashText != null)
        {
            dashText.text = ""; 
        }

        if (!isDashing)
        {
            lastHorizontalInput = horizontalMovement; 
            Rb.linearVelocity = new Vector2(horizontalMovement * movementSpeed, Rb.linearVelocity.y); 
        }
    }

    private void Dash()
    {
        if (CanDash && currentDashCharges > 0)
        {
            isDashing = true;
            currentDashCharges--;

            Rb.gravityScale = 0f;

            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, 0);

            Vector2 dashDirection = new Vector2(Rb.linearVelocity.x, 0).normalized;

            if (dashDirection.x == 0)
            {
                dashDirection = isFacingRight ? Vector2.right : Vector2.left;
            }
            Rb.linearVelocity = new Vector2(dashDirection.x * dashDistance, Rb.linearVelocity.y);

            Invoke("EndDash", dashDuration); 
        }
    }

    private void EndDash()
    {
        Rb.gravityScale = 1f;

        isDashing = false;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        hit = Physics2D.Raycast(this.transform.position, Vector2.down, collider.bounds.extents.y + 0.1f, mask);
        return hit.collider != null;
    }

    public void EnableDoubleJump(bool enable)
    {
        canDoubleJump = enable;
    }

    public void EnableDash(bool enable)
    {
        CanDash = enable;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, Vector3.down * (collider.bounds.extents.y + 0.1f));
    }
}