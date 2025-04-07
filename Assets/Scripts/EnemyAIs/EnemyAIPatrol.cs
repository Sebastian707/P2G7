using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float range = 10f;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 roamTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewTarget();
    }

    void Update()
    {
        // Check if on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // Move horizontally toward target
        Vector3 direction = (roamTarget - transform.position);
        direction.y = 0; // No vertical movement for patrol
        direction.Normalize();

        rb.linearVelocity = new Vector3(direction.x * moveSpeed, rb.linearVelocity.y, 0f);

        // If close to target, pick new one + optionally jump
        if (Vector3.Distance(transform.position, roamTarget) < 1f)
        {
            SetNewTarget();

            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    void SetNewTarget()
    {
        float x = Random.Range(-range, range);
        roamTarget = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
    }
}

