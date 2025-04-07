using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Coyote Time")]
    public float coyoteTime = 0f;
    private float coyoteTimeCounter;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Double Jump")]
    public int extraJumps = 1;
    private int extraJumpsRemaining;
    private float jumpResetTime = 3f;
    private float jumpResetTimer;

    [Header("Respawn")]
    public Transform respawnPoint;
    public float deathY = -10f;

    [Header("Effects")]
    public GameObject deathEffectPrefab;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || coyoteTimeCounter > 0f)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                coyoteTimeCounter = 0f;
                jumpResetTimer = jumpResetTime;
            }
            else if (extraJumpsRemaining > 0)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                extraJumpsRemaining--;
                jumpResetTimer = jumpResetTime;
            }
        }

        // Fall-death check
        if (transform.position.y < deathY)
        {
            Die();
        }

        // Auto-reset extra jumps after 3 seconds
        jumpResetTimer -= Time.deltaTime;
        if (jumpResetTimer <= 0f && extraJumpsRemaining < extraJumps)
        {
            extraJumpsRemaining = extraJumps;
            jumpResetTimer = jumpResetTime;
        }
    }

  void Die()
{
    Debug.Log("DIED! Instantiating effect...");

    GameObject obj = Instantiate(deathEffectPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
    Debug.Log("Instantiated: " + obj.name);

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
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
