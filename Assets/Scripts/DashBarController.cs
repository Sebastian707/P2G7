using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBarController : MonoBehaviour
{
    [SerializeField] private Slider dashSlider;
    [SerializeField] private PlayerController player;
    [SerializeField] private float smoothSpeed = 5f;

    [Header("Notch Settings")]
    [SerializeField] private RectTransform notchContainer;
    [SerializeField] private GameObject notchPrefab;

    private List<GameObject> activeNotches = new List<GameObject>();
    private float targetValue;
    private float previousMaxCharges = -1;

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if (dashSlider != null && player != null)
        {
            UpdateDashUIVisibility(player.CanDash);

            if (player.CanDash)
            {
                previousMaxCharges = player.GetMaxDashCharges();
                dashSlider.maxValue = previousMaxCharges;
                targetValue = player.GetCurrentDashCharges();
                dashSlider.value = targetValue;

                GenerateNotches(previousMaxCharges);
            }
        }
    }

    private void Update()
    {
        if (dashSlider == null || player == null) return;

        // Handle UI visibility based on CanDash
        UpdateDashUIVisibility(player.CanDash);

        if (!player.CanDash)
            return;

        targetValue = player.GetCurrentDashCharges();
        dashSlider.value = Mathf.Lerp(dashSlider.value, targetValue, Time.deltaTime * smoothSpeed);

        if (Mathf.Abs(dashSlider.value - targetValue) < 0.01f)
        {
            dashSlider.value = targetValue;
        }

        float currentMax = player.GetMaxDashCharges();
        if (currentMax != previousMaxCharges)
        {
            dashSlider.maxValue = currentMax;
            previousMaxCharges = currentMax;
            GenerateNotches(currentMax);
        }
    }

    private void UpdateDashUIVisibility(bool isVisible)
    {
        // Toggle UI based on whether the player can dash
        if (dashSlider != null)
            dashSlider.gameObject.SetActive(isVisible);

        if (notchContainer != null)
            notchContainer.gameObject.SetActive(isVisible);
    }

    private void GenerateNotches(float count)
    {
        foreach (var notch in activeNotches)
        {
            Destroy(notch);
        }
        activeNotches.Clear();

        if (count <= 1 || notchPrefab == null || notchContainer == null)
            return;

        float width = ((RectTransform)notchContainer).rect.width;

        for (int i = 1; i < count; i++)
        {
            GameObject notch = Instantiate(notchPrefab, notchContainer);
            RectTransform rt = notch.GetComponent<RectTransform>();

            float normalized = (float)i / count;
            rt.anchoredPosition = new Vector2(normalized * width, 0f);

            activeNotches.Add(notch);
        }
    }
}
