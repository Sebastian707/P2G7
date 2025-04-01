using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure player has the tag "Player"
        {
            other.GetComponent<PlayerRespawn>().SetCheckpoint(transform.position);
        }
    }
}