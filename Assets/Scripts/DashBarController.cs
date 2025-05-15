using UnityEngine;
using UnityEngine.UI;

public class DashBarController : MonoBehaviour
{
    [SerializeField] private Slider dashSlider;
    [SerializeField] private PlayerController player;

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if (dashSlider != null && player != null)
        {
            dashSlider.maxValue = player.GetMaxDashCharges();
            dashSlider.value = player.GetCurrentDashCharges();
        }
    }

    private void Update()
    {
        if (dashSlider != null && player != null)
        {
            dashSlider.value = player.GetCurrentDashCharges();
        }
    }
}
