using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 10f;
    public int extraJumps = 1;

    [Header("Coyote Time")]
    public float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private int extraJumpsRemaining;
    [Header("Respawn/Death")]
public GameObject deathEffectPrefab;
public Transform respawnPoint;
public float deathY = -10f;

private float jumpResetTime = 3f;
private float jumpResetTimer;


    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumpsRemaining = extraJumps;
    }

    void Update()
    {
        // Check if on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            extraJumpsRemaining = extraJumps;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Jump input
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || coyoteTimeCounter > 0f)
            {
                Jump();
                coyoteTimeCounter = 0f;
            }
            else if (extraJumpsRemaining > 0)
            {
                Jump();
                extraJumpsRemaining--;
            }
        }
        if (transform.position.y < deathY)
{
    Die();
}
    }

    void FixedUpdate()
    {
        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Reset vertical
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Die()
    {
        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        Respawn();
    }

    void Respawn()
    {
        transform.position = respawnPoint.position;
        rb.linearVelocity = Vector2.zero;
        extraJumpsRemaining = extraJumps;
        jumpResetTimer = jumpResetTime;
    }
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

public void ApplyKnockback(Vector2 knockback)
{
    rb.linearVelocity = Vector2.zero;
    rb.AddForce(knockback, ForceMode2D.Impulse);

}


}
