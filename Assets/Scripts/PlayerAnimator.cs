using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    private Vector2 input;
    private Vector2 lastMoveDirection;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
        Animate();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input = new Vector2(moveX, moveY).normalized;
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
}
