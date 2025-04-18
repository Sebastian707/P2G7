using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlayer : MonoBehaviour
{
    private MovingTrap _movingTrap;

    void Start()
    {
        _movingTrap = GetComponent<MovingTrap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // Parent player to platform
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            DontDestroyOnLoad(collision.gameObject);

        }
    }
}