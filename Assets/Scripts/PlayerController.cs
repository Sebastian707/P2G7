using UnityEngine;
using TMPro;
using System.Collections;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip doubleJumpSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip respawnSound;
    private AudioSource audioSource;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float airAcceleration = 5f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float airDeceleration = 7f;
    [SerializeField] private float maxAcceleration = 30f;

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
    [SerializeField] private Vector2 deathRecoil;
    private Vector2 checkpointPosition;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    public float deathDelay;
    private bool isRespawning = false;
    public float fadeLinger = 5f;

    public bool isFacingRight = true;
    private static PlayerController instance;

    [Header("Camera")]
    [SerializeField] private GameObject cameraFollowGO;
    private CameraFollowObject cameraFollowObject;

    [Header("Grapple Settings")]
    [SerializeField] private float grappleSpeed = 5f;        
    [SerializeField] private float snapDistance = 0.1f;
    [SerializeField] private bool CanGrapple = false;
    private LineRenderer line;
    private Transform currentTarget;
    private Rigidbody2D rb;
    private float originalGravity;

    private float horizontalInput;

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
        audioSource = gameObject.GetComponent<AudioSource>();
        _transform = gameObject.GetComponent<Transform>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();

        if (dashText != null)
        {
            dashText.text = "Dashes: " + currentDashCharges + " / " + maxDashCharges;
        }

        cameraFollowObject = cameraFollowGO.GetComponent<CameraFollowObject>();

        // Grapple Setup
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0 && !isFacingRight) Flip();
        else if (horizontalInput < 0 && isFacingRight) Flip();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (IsGrounded())
        {
            CoyoteTimeCounter = coyoteTime;
        }
        else
        {
            CoyoteTimeCounter -= Time.deltaTime;
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

        if (Input.GetKey(KeyCode.Q) && CanGrapple)
        {
            rb.gravityScale = 0f;
            FindAndPullToClosestGrapplePoint();

        }
        else
        {
            StopGrapple();
        }
        if (Input.GetKeyUp(KeyCode.Space) && Rb.linearVelocity.y > 0)
        {
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, Rb.linearVelocity.y * varJump);
            CoyoteTimeCounter = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (isRespawning || isDashing) return;

        Vector2 velocity = Rb.linearVelocity;

        if (velocity.y < 0)
        {
            velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        velocity.y = Mathf.Max(velocity.y, maxFallSpeed);

        if (jumpBufferCounter > 0f)
        {
            if (CoyoteTimeCounter > 0f)
            {
                velocity.y = jumpForce;
                doubleJumpsLeft = maxDoubleJumps;
                jumpBufferCounter = 0f;
                PlaySound(jumpSound);
            }
            else if (canDoubleJump && doubleJumpsLeft > 0)
            {
                TriggerDoubleJumpEffect();
                velocity.y = doubleJumpForce;
                doubleJumpsLeft--;
                jumpBufferCounter = 0f;
                PlaySound(doubleJumpSound);
            }
        }

        float targetSpeed = horizontalInput * movementSpeed;
        float speedDifference = targetSpeed - velocity.x;
        float accelRate = Mathf.Abs(horizontalInput) > 0.1f ?
            (IsGrounded() ? acceleration : airAcceleration) :
            (IsGrounded() ? deceleration : airDeceleration);

        float maxSpeedChange = maxAcceleration * Time.fixedDeltaTime;
        float movementForce = Mathf.Clamp(speedDifference * accelRate * Time.fixedDeltaTime, -maxSpeedChange, maxSpeedChange);
        velocity.x += movementForce;

        Rb.linearVelocity = velocity;
    }

    private void Dash()
    {
        isDashing = true;
        currentDashCharges--;
        movementSpeed = 0f;
        canDoubleJump = false;
        Rb.gravityScale = 0f;
        Rb.linearVelocity = Vector2.zero;
        PlaySound(dashSound);

        Vector2 dashDirection = new Vector2(horizontalInput, 0).normalized;
        if (dashDirection == Vector2.zero)
        {
            dashDirection = isFacingRight ? Vector2.right : Vector2.left;
        }

        Rb.linearVelocity = new Vector2(dashDirection.x * dashDistance, 0);
        Invoke(nameof(EndDash), dashDuration);
    }

    private void EndDash()
    {
        isDashing = false;
        movementSpeed = defaultMovementSpeed;
 
        Rb.gravityScale = 1f;
        canDoubleJump = true;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        cameraFollowObject.CallTurn();
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
        yield return new WaitForSeconds(fadeLinger);
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

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void FindAndPullToClosestGrapplePoint()
    {
        GameObject[] grapplePoints = GameObject.FindGameObjectsWithTag("GrapplePoint");
        float closestDist = float.MaxValue;
        Transform closest = null;

        foreach (GameObject point in grapplePoints)
        {
            GrapplePoint gp = point.GetComponent<GrapplePoint>();
            if (gp != null && gp.IsPlayerInRange())
            {
                float dist = Vector2.Distance(transform.position, point.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closest = point.transform;
                }
            }
        }

        if (closest != null)
        {
            currentTarget = closest;

            Vector2 direction = (currentTarget.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, currentTarget.position);

            if (distance > snapDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, grappleSpeed * Time.deltaTime);
            }

            line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, currentTarget.position);
        }
        else
        {
            StopGrapple();
        }

        if (PlayerController.instance != null)
        {
        
            PlayerController.instance.doubleJumpsLeft = PlayerController.instance.maxDoubleJumps; 
        }
    }

    void StopGrapple()
    {
        rb.gravityScale = originalGravity;
        currentTarget = null;
        line.positionCount = 0;
    }

}
