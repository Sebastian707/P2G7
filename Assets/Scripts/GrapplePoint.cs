using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public Color inRangeColor = Color.green;
    public Color outOfRangeColor = Color.red;
    public float grappleRange = 3f;

    private SpriteRenderer sr;
    private Transform player;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        sr.color = dist <= grappleRange ? inRangeColor : outOfRangeColor;
    }

    public bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= grappleRange;
    }
}
