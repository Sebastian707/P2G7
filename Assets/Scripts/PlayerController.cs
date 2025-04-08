using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 3;
    //self references
    private new Collider2D collider;
    private Rigidbody2D Rb;
<<<<<<< Updated upstream
=======
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

    private float lastHorizontalInput = 0;
    public bool isFacingRight = true;
    private static PlayerController instance;

    [Header("Camera")]
    [SerializeField] private GameObject cameraFollowGO;
    private CameraFollowObject cameraFollowObject;

    private float horizontalInput;

>>>>>>> Stashed changes
    private void OnValidate()
    {
        collider = GetComponent<CapsuleCollider2D>();
        Rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

        }
    }
    private void FixedUpdate()
    {
        Rb.position += Vector2.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

    }

    public bool IsGrounded()
    {

        RaycastHit2D hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        hit = Physics2D.Raycast(this.transform.position, Vector2.down, collider.bounds.extents.y + 0.1f, mask);


        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, Vector3.down * (collider.bounds.extents.y + 0.1f));
    }
}
