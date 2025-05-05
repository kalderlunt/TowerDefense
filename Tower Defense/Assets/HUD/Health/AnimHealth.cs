using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimHealth : MonoBehaviour
{

    
    [Header("Parameters")]
    [SerializeField] private float chipSpeed = 2.0f; // Speed at which the health bar updates
    [Header("Components")]
    public Health health; // Reference to the health component
    [SerializeField] private TMP_Text healthTextPlayer; // Player health text display
    [SerializeField] private Image frontHealthBar; // Front health bar image
    [SerializeField] private Image backHealthBar; // Back health bar image

    private float displayedHealth;

    void Update()
    {
        UpdateAnimHealth();
    }

    /// <summary>
    /// Initializes the UX elements of the health bar.
    /// </summary>
    private void StartAnimHeal()
    {
        frontHealthBar.fillAmount = health.healthPoint / health.healthMax;
        backHealthBar.fillAmount = health.healthPoint / health.healthMax;
    }

    /// <summary>
    /// Updates all elements of the health bar.
    /// </summary>
    private void UpdateAnimHealth()
    {
        UxUpdateCheck();
        UxRefreshText();
    }

    /// <summary>
    /// Updates the fill amounts of the front and back health bars based on the current health.
    /// </summary>
    private void UxUpdateCheck()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health.healthPoint / health.healthMax; // Decimal representation of health (0 to 1)

        // Update back health bar (damage taken)
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            health.lerpTimer += Time.deltaTime;

            float percentComplete = health.lerpTimer / chipSpeed; // Percentage of the animation complete (0 to 1)health.LerpTimer / _chipSpeed;
            percentComplete = percentComplete * percentComplete;

            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        // Update front health bar (healing)
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            health.lerpTimer += Time.deltaTime;

            float percentComplete = health.lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;

            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    /// <summary>
    /// Refreshes the displayed health text.
    /// </summary>
    private void UxRefreshText()
    {
        health.lerpTimer += Time.deltaTime;

        float percentComplete = health.lerpTimer / chipSpeed;
        percentComplete = percentComplete * percentComplete;
        displayedHealth = Mathf.Lerp(displayedHealth, health.healthPoint, percentComplete);

        healthTextPlayer.text = $"{Mathf.RoundToInt(displayedHealth)} / {health.healthMax} ♥";
    }
}
