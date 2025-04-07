using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    public int damage = 1;
    public Vector2 knockbackForce = new Vector2(5f, 3f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Vector2 direction = (other.transform.position - transform.position).normalized;
                playerHealth.TakeDamage(damage, direction * knockbackForce);
            }
        }
    }

}
