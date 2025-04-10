using System.Collections;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float flipYRotationTime = 0.5f;

    private Coroutine turnCoruotine;

    private PlayerController player;

    private bool isFacingRight;

    private void Awake()
    {
        player = playerTransform.gameObject.GetComponent<PlayerController>();

        isFacingRight = player.isFacingRight;
    }

    private void Update()
    {
        transform.position = playerTransform.position;
    }

    public void CallTurn()
    {
        turnCoruotine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while(elapsedTime < flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;

        if (isFacingRight)
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }
}
