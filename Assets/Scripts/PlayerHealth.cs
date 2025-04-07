using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Respawn")]
    public Transform respawnPoint;

    [Header("UI")]
    public Slider healthSlider;
    public Image fillImage;
    public Gradient healthColorGradient;
    public TextMeshProUGUI healthText;
    [Header("Damage Settings")]
public float damageCooldown = 0.2f;
private float lastDamageTime = -999f;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

public void TakeDamage(int amount, Vector2 knockback)
{
    if (Time.time - lastDamageTime < damageCooldown)
        return;

    lastDamageTime = Time.time;
    currentHealth -= amount;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    UpdateHealthUI();

    if (currentHealth <= 0)
    {
        Die();
    }
    else
    {
        StartCoroutine(FlashBar());
    }

    // Knockback
    PlayerController controller = GetComponent<PlayerController>();
    if (controller != null)
    {
        controller.ApplyKnockback(knockback);
    }
}



    void Die()
    {
        Debug.Log("Player died!");
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

   private void UpdateHealthUI()
{
    if (healthSlider != null)
    {
        healthSlider.value = currentHealth;
    }

    if (healthText != null)
    {
        healthText.text = currentHealth.ToString("0") + " / " + maxHealth.ToString("0");
    }

    if (fillImage != null)
    {
        float healthPercent = (float)currentHealth / maxHealth;

        if (healthPercent > 0.6f)
        {
            fillImage.color = Color.green;
        }
        else if (healthPercent > 0.3f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }
    }
}


    // ✅ HERE'S THE FLASH EFFECT COROUTINE
    private IEnumerator FlashBar()
{
    Color originalColor = fillImage.color;

    // Temporary flash color
    fillImage.color = Color.white;

    yield return new WaitForSeconds(0.1f);

    // Recalculate and reapply correct health-based color
    float healthPercent = (float)currentHealth / maxHealth;

    if (healthPercent > 0.6f)
        fillImage.color = Color.green;
    else if (healthPercent > 0.3f)
        fillImage.color = Color.yellow;
    else
        fillImage.color = Color.red;
}


}
