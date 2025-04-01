using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 checkpointPosition;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    private Transform _transform;
    public Vector2 deathRecoil;
    private Rigidbody2D _rigidbody;
    public float deathDelay;

    private void Start()
    {
        // Default checkpoint to starting position
        checkpointPosition = transform.position;
        _transform = gameObject.GetComponent<Transform>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }

    public IEnumerator Respawn()

    {
        yield return StartCoroutine(Fade(1));
        transform.position = checkpointPosition;
        yield return StartCoroutine(Fade(0));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazard")) // Ensure hazards like spikes have the tag "Hazard"
        {
            Vector2 newForce;
            newForce.x = -_transform.localScale.x * deathRecoil.x;
            newForce.y = deathRecoil.y;
            _rigidbody.AddForce(newForce, ForceMode2D.Impulse);
            StartCoroutine(Respawn());
        }
    }
    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}

