using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;
    public float maxY = 10f;
    public float minY = 0f;

    public float maxX = 20f; // optional
    public float minX = -20f;

    void LateUpdate()
    {
        if (target == null) return;

        float cameraY = Mathf.Clamp(target.position.y + offset.y, minY, maxY);
        float cameraX = Mathf.Clamp(target.position.x + offset.x, minX, maxX);

        Vector3 desiredPosition = new Vector3(
            cameraX,
            cameraY,
            transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
