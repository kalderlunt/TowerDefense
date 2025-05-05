using TMPro;
using UnityEngine;

public class CreditsDisplay : MonoBehaviour
{
    [SerializeField] private SO_Inventory inventory;
    [SerializeField] private TMP_Text credits;

    private void OnEnable()
    {
        RefreshText();
    }
    public void RefreshText()
    {
        credits.text = $"{inventory.credits.ToString()} CR";
    }
}