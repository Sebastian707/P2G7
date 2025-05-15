using UnityEngine;
using UnityEngine.UI;

public class DashBarController : MonoBehaviour
{
    [SerializeField] private Slider dashSlider;
    [SerializeField] private PlayerController player;
    [SerializeField] private float smoothSpeed = 5f;

    private float targetValue;

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if (dashSlider != null && player != null)
        {
            dashSlider.maxValue = player.GetMaxDashCharges();
            targetValue = player.GetCurrentDashCharges();
            dashSlider.value = targetValue;
        }
    }

    private void Update()
    {
        if (dashSlider != null && player != null)
        {
            targetValue = player.GetCurrentDashCharges();

            // Smoothly interpolate the slider's value toward the target
            dashSlider.value = Mathf.Lerp(dashSlider.value, targetValue, Time.deltaTime * smoothSpeed);

            // Optional: Snap to value if it's very close to avoid endless small differences
            if (Mathf.Abs(dashSlider.value - targetValue) < 0.01f)
            {
                dashSlider.value = targetValue;
            }
        }
    }
}
