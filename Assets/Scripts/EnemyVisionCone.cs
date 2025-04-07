using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EnemyVisionCone : MonoBehaviour
{
    public float viewDistance = 5f;
    [Range(0, 360)] public float fov = 70f;
    public int rayCount = 50;

    public Transform enemyTransform;

    private Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        DrawCone();
    }

    void Update()
    {
        FollowEnemy();
        DrawCone();
    }

    void DrawCone()
{
    Vector3[] vertices = new Vector3[rayCount + 2];
    int[] triangles = new int[rayCount * 3];

    mesh.Clear();

    vertices[0] = Vector3.zero;

    float angle = -fov / 2f;
    float angleStep = fov / rayCount;

    // Determine which direction to face (left = -1, right = 1)
    float facingDirection = Mathf.Sign(enemyTransform.localScale.x);

    // If facing left, we keep the usual winding order
    bool isFacingLeft = facingDirection < 0;

    for (int i = 0; i <= rayCount; i++)
    {
        float rad = Mathf.Deg2Rad * angle;

        Vector3 direction = new Vector3(Mathf.Cos(rad) * facingDirection, Mathf.Sin(rad), 0f);
        vertices[i + 1] = direction * viewDistance;

        if (i < rayCount)
        {
            int idx = i * 3;

            // Swap vertex winding order when facing right to fix visibility
            if (isFacingLeft)
            {
                triangles[idx] = 0;
                triangles[idx + 1] = i + 1;
                triangles[idx + 2] = i + 2;
            }
            else
            {
                triangles[idx] = 0;
                triangles[idx + 1] = i + 2;
                triangles[idx + 2] = i + 1;
            }
        }

        angle += angleStep;
    }

    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.RecalculateNormals();
}



void FollowEnemy()
{
    if (enemyTransform == null) return;

    transform.position = enemyTransform.position;
    // Do NOT rotate or scale here anymore
}



}
