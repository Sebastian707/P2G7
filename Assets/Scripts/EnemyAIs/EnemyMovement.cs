using System.Collections;
using System.Collections.Generic;
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

    [Header("Attack Settings")]
public Transform player;              // Player reference
public float attackRange = 1.5f;      // Attack distance
public float attackCooldown = 1f;     // Time between attacks
private float lastAttackTime = 0f;    // Tracks time since last attack

[Header("Chase Settings")]
public float chaseRange = 5f;
public float verticalChaseRange = 1.5f;
public float stopDistance = 0.5f;

[Header("Vision Settings")]
public float viewDistance = 5f;
[Range(0, 360)] public float fieldOfViewAngle = 60f;

[Header("Combat")]
public GameObject attackRangeTrigger;


public Transform enemySpriteTransform;



    private bool isChasing = false;


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

    float distanceToPlayer = Vector2.Distance(transform.position, player.position);
    // Check distance to player
    float horizontalDistance = Mathf.Abs(player.position.x - transform.position.x);
    float verticalDistance = Mathf.Abs(player.position.y - transform.position.y);


if (horizontalDistance <= chaseRange && verticalDistance <= verticalChaseRange)
{
    isChasing = true;
}
else if (horizontalDistance > chaseRange + 1f || verticalDistance > verticalChaseRange + 0.5f)
{
    isChasing = false;
}


if (isChasing)
{
    ChasePlayer();
}
else
{
    Patrol();
}


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

    if (horizontalDistance <= attackRange && Time.time >= lastAttackTime)
    {
        Attack();
        lastAttackTime = Time.time;
    }

    if (IsPlayerInVision())
{
    // Start chasing
    isChasing = true;
}
else if (isChasing)
{
    // Optional: go back to patrol if player left vision
    isChasing = false;
}

float dist = Vector2.Distance(transform.position, player.position);
if (dist <= attackRange && Time.time >= lastAttackTime)
{
    Attack();
}

if (attackRangeTrigger != null)
{
    Vector3 attackOffset = new Vector3(1f * direction, 0f, 0f); // distance in front of enemy
    attackRangeTrigger.transform.localPosition = attackOffset;
}

UpdateAttackZonePosition();

}

void UpdateAttackZonePosition()
{
    if (attackRangeTrigger != null)
    {
        Vector3 offset = new Vector3(1f * direction, 0f, 0f);
        attackRangeTrigger.transform.localPosition = offset;
    }
}

void Attack()
{
    if (Time.time < lastAttackTime + attackCooldown)
        return;

    lastAttackTime = Time.time;

    // Enable the attack zone briefly
    StartCoroutine(EnableAttackZone());
}

IEnumerator EnableAttackZone()
{
    attackRangeTrigger.SetActive(true);
    yield return new WaitForSeconds(0.2f); // attack window
    attackRangeTrigger.SetActive(false);
}


  void Patrol()
{
    rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

    bool groundAhead = Physics2D.Raycast(groundAheadCheck.position, Vector2.down, edgeCheckDistance, groundLayer);
    Debug.DrawRay(groundAheadCheck.position, Vector2.down * edgeCheckDistance, Color.red);

    bool hitLeftLimit = direction < 0 && transform.position.x <= leftEdge.position.x + edgeBuffer;
    bool hitRightLimit = direction > 0 && transform.position.x >= rightEdge.position.x - edgeBuffer;

    if (!groundAhead || hitLeftLimit || hitRightLimit)
    {
        direction *= -1f;

        // 👉 Update the AttackZone side
        if (attackRangeTrigger != null)
        {
            Vector3 offset = new Vector3(1f * direction, 0f, 0f);
            attackRangeTrigger.transform.localPosition = offset;
        }
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

    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        // Knockback direction: from enemy to player
        Vector2 direction = (collision.transform.position - transform.position).normalized;
        Vector2 knockbackForce = direction * 5f + Vector2.up * 3f;

        // Apply damage to the player
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
        if (health != null)
        {
            Debug.Log("Applying damage to player!");
            health.TakeDamage(1, knockbackForce);
        }
    }
}

void ChasePlayer()
{
    float directionToPlayer = Mathf.Sign(player.position.x - transform.position.x);

    if (Mathf.Abs(player.position.x - transform.position.x) > stopDistance)
    {
        rb.linearVelocity = new Vector2(directionToPlayer * moveSpeed, rb.linearVelocity.y);
    }
    else
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y); // stop moving if too close
    }

    // Flip sprite
    Vector3 scale = transform.localScale;
    scale.x = Mathf.Abs(scale.x) * (directionToPlayer > 0 ? 1 : -1);
    transform.localScale = scale;
}

void OnDrawGizmosSelected()
{
    Gizmos.color = Color.yellow;

    // Get directions for left and right edges of the cone
    Vector3 forward = transform.right;
    Vector3 leftBoundary = Quaternion.Euler(0, 0, -fieldOfViewAngle / 2) * forward;
    Vector3 rightBoundary = Quaternion.Euler(0, 0, fieldOfViewAngle / 2) * forward;

    // Draw lines showing the FOV
    Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewDistance);
    Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewDistance);
}

bool IsPlayerInVision()
{
    if (player == null) return false;

    Vector2 dirToPlayer = (player.position - transform.position).normalized;
    float angleToPlayer = Vector2.Angle(transform.right, dirToPlayer);
    float distanceToPlayer = Vector2.Distance(transform.position, player.position);

    return angleToPlayer < fieldOfViewAngle / 2f && distanceToPlayer <= viewDistance;
}



}
