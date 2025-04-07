using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private float direction = 1f;

    [Header("Coyote Time")]
    public float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Edge Detection")]
    public Transform groundAheadCheck;
    public float edgeCheckDistance = 0.1f;
    private float edgeBuffer = 0.1f;

    [Header("Double Jump")]
    public int extraJumps = 1;
    private int extraJumpsRemaining;
    private float jumpResetTime = 3f;
    private float jumpResetTimer;

    [Header("Patrol Settings")]
    public Transform leftEdge;
    public Transform rightEdge;

    [Header("Respawn")]
    public Transform respawnPoint;
    public float deathY = -10f;

    [Header("Effects")]
    public GameObject deathEffectPrefab;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpResetTimer = jumpResetTime;

        if (leftEdge == null || rightEdge == null)
            Debug.LogError("LEFT/RIGHT EDGE NOT ASSIGNED!");
        if (groundAheadCheck == null)
            Debug.LogError("GROUND AHEAD CHECK NOT ASSIGNED!");
        if (groundLayer == 0)
            Debug.LogError("GROUND LAYER NOT SET!");
    }

    void FixedUpdate()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            extraJumpsRemaining = extraJumps;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }

        Patrol();

        if (ShouldJump())
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

        jumpResetTimer -= Time.fixedDeltaTime;
        if (jumpResetTimer <= 0f && extraJumpsRemaining < extraJumps)
        {
            extraJumpsRemaining = extraJumps;
            jumpResetTimer = jumpResetTime;
        }

        if (transform.position.y < deathY)
        {
            Die();
        }
    }

    void Patrol()
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        // Ground detection in front
        bool groundAhead = Physics2D.Raycast(groundAheadCheck.position, Vector2.down, edgeCheckDistance, groundLayer);
        Debug.DrawRay(groundAheadCheck.position, Vector2.down * edgeCheckDistance, Color.red);

        // Turn at platform edges
        bool hitLeftLimit = direction < 0 && transform.position.x <= leftEdge.position.x + edgeBuffer;
        bool hitRightLimit = direction > 0 && transform.position.x >= rightEdge.position.x - edgeBuffer;

        if (!groundAhead || hitLeftLimit || hitRightLimit)
        {
            direction *= -1f;
        }

        // Flip sprite
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (direction > 0 ? 1 : -1);
        transform.localScale = scale;
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpResetTimer = jumpResetTime;
    }

    bool ShouldJump()
    {
        Vector2 origin = new Vector2(transform.position.x + (direction * 0.6f), transform.position.y + 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * direction, 0.2f, groundLayer);
        Debug.DrawRay(origin, Vector2.right * direction * 0.2f, Color.magenta);
        return hit.collider != null;
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

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (leftEdge != null && rightEdge != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(leftEdge.position, rightEdge.position);
        }

        if (groundAheadCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundAheadCheck.position, groundAheadCheck.position + Vector3.down * edgeCheckDistance);
        }
    }
}
