using UnityEngine;

public class CollisionTest : MonoBehaviour
{
void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        Debug.Log("Collided with: Ground");
    }
}
}