using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float airAcceleration = 5f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float airDeceleration = 7f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float doubleJumpForce = 2.5f;
    [SerializeField] private int maxDoubleJumps = 1;
    [SerializeField] private float varJump = 0.5f;
    [SerializeField] private float maxFallSpeed = -10f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private GameObject doubleJumpEffectPrefab;
    [SerializeField] private Transform effectPosition;
    private float CoyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private new Collider2D collider;
    private Rigidbody2D Rb;
    [SerializeField] private bool canDoubleJump = false;
    private int doubleJumpsLeft = 0;

    [Header("Dash Settings")]
    [SerializeField] private bool CanDash = false;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private int maxDashCharges = 3;
    [SerializeField] private float chargeRestoreRate = 1f;
    private int currentDashCharges;
    private bool isDashing = false;
    private float dashStartTime;
    public TextMeshProUGUI dashText;
    private float defaultMovementSpeed;

    [Header("Respawn Settings")]
    private Vector2 checkpointPosition;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    private Transform _transform;
    public Vector2 deathRecoil;
    private Rigidbody2D _rigidbody;
    public float deathDelay;
    private bool isRespawning = false;

    private float lastHorizontalInput = 0;
    private bool isFacingRight = true;
    private static PlayerController instance;

    private void OnValidate()
    {
        collider = GetComponent<Collider2D>();
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        defaultMovementSpeed = movementSpeed;
        currentDashCharges = maxDashCharges;
        checkpointPosition = transform.position;
        _transform = gameObject.GetComponent<Transform>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();

        if (dashText != null)
        {
            dashText.text = "Dashes: " + currentDashCharges + " / " + maxDashCharges;
        }
    }

    private void Update()
    {
        if (Rb.linearVelocity.y < 0)
        {
            Rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (Rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            Rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

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

        if (!isDashing)
        {
            lastHorizontalInput = horizontalMovement;

            float targetSpeed = horizontalMovement * movementSpeed;
            float speedDifference = targetSpeed - Rb.linearVelocity.x;

            float accelRate = Mathf.Abs(horizontalMovement) > 0.1f ? (IsGrounded() ? acceleration : airAcceleration) : (IsGrounded() ? deceleration : airDeceleration);

            float movementForce = speedDifference * accelRate;
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x + movementForce * Time.deltaTime, Mathf.Max(Rb.linearVelocity.y, maxFallSpeed));
        }

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
                TriggerDoubleJumpEffect();
                Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, doubleJumpForce);
                doubleJumpsLeft--;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && Rb.linearVelocity.y > 0)
        {
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, Rb.linearVelocity.y * varJump);
            CoyoteTimeCounter = 0f;
        }

        if (CanDash && currentDashCharges > 0 && Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            Dash();
        }

        if (!isDashing && currentDashCharges < maxDashCharges && Time.time >= dashStartTime + chargeRestoreRate)
        {
            currentDashCharges++;
            dashStartTime = Time.time;
        }

        if (dashText != null)
        {
            dashText.text = CanDash ? "Dashes: " + currentDashCharges + " / " + maxDashCharges : "";
        }
    }

    private void Dash()
    {
        if (CanDash && currentDashCharges > 0)
        {
            isDashing = true;
            currentDashCharges--;

            movementSpeed = 0f;
            canDoubleJump = false;
            Rb.gravityScale = 0f;
            Rb.linearVelocity = Vector2.zero;

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
        canDoubleJump = true;
        movementSpeed = defaultMovementSpeed;
        Rb.gravityScale = 1f;
        isDashing = false;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, collider.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground")).collider != null;
    }

    public void EnableDoubleJump(bool enable) => canDoubleJump = enable;
    public void EnableDash(bool enable) => CanDash = enable;
    public void SetCheckpoint(Vector2 newCheckpoint) => checkpointPosition = newCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard") && !isRespawning)
        {
            isRespawning = true;
            movementSpeed = 0f;
            _rigidbody.AddForce(new Vector2(-_transform.localScale.x * deathRecoil.x, deathRecoil.y), ForceMode2D.Impulse);
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        movementSpeed = defaultMovementSpeed;
        yield return StartCoroutine(Fade(1));
        transform.position = checkpointPosition;
        yield return StartCoroutine(Fade(0));
        isRespawning = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha, elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = targetAlpha;
    }

    private void TriggerDoubleJumpEffect()
    {
        GameObject effect = Instantiate(doubleJumpEffectPrefab, effectPosition.position, Quaternion.identity);

        effect.transform.SetParent(transform);

        Destroy(effect, 0.21f);
    }
}
