using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float jumpForce = 3;
    //self references
    private new Collider2D collider;
    private Rigidbody2D Rb;
    private void OnValidate()
    {
        collider = GetComponent<CapsuleCollider2D>();
        Rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Movement();

    }


    private void Movement()
    {
       // Ray2D ray = new Ray2D(transform.position, transform.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime);
        
        transform.position += transform.right * Input.GetAxis("Horizontal") *movementSpeed* Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

        }
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
