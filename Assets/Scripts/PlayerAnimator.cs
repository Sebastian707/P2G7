using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // horizontalMove = 0f;
    //public float runSpeed = 40f;

    public float speed = 0.5f;
    public Rigidbody2D rb;
    private Vector2 input;

    private Animator anim;
    private Vector2 lastMoveDirection;
    //private bool FacingLeft = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"MoveX: {input.x}, MoveY: {input.y}, Magnitude: {input.magnitude}");

        Debug.Log("MoveMagnitude: " + input.magnitude);
        Debug.Log("FacingX: " + anim.GetFloat("FacingX"));
        //horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));



        ProccessInputs();
        Animate();

        /*
        if (input.x < 0 && !FacingLeft || input.x > 0 && FacingLeft)
        {
            //Flip();
        }
        */
        

        

    }


    void ProccessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }


        input.x = moveX;
        input.y = moveY;

        input.Normalize();
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
    /*
    void Flip() 
    {
        Vector3 scale = transform.localScale;
        scale.x = -1;
        //transform.localScale = scale;
        FacingLeft = !FacingLeft;
    }
    */
}
