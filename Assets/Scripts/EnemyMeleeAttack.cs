using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public int damage = 1;
    public Vector2 knockback = new Vector2(5f, 3f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                Vector2 direction = (other.transform.position - transform.position).normalized;
                ph.TakeDamage(damage, direction * knockback);
            }
        }
    }
}

