using Managers;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class CancelPlaceFeedback : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private string messageTemplate = "Press ({0}) to Cancel place";
    [SerializeField] private string cancelKey = "Esc"; // Default, can be set dynamically
    [SerializeField] private InputActionReference cancelActionReference;

    [SerializeField] private InGameInventory inventory;
    
    private void OnEnable()
    {
        Debug.Log("CancelPlaceFeedback OnEnable");
        Hide();

        if (inventory != null)
        {
            inventory.onPlacePreviewTower += Show;
            Debug.Log("Subscribed to inventory.onPlacePreviewTower");
        }
        else
        {
            Debug.LogError("inventory is not assigned!");
        }

        if (EventManager.instance != null)
        {
            EventManager.instance.onCancelPlaceTower += Hide;
            Debug.Log("Subscribed to EventManager.onCancelPlaceTower");
        }
        else
        {
            Debug.LogError("EventManager.instance is null!");
        }
    }

    private void OnDisable()
    {
        inventory.onPlacePreviewTower -= Show;
        EventManager.instance.onCancelPlaceTower -= Hide;
    }

    private void Show()
    {   
        string cancelKey = cancelActionReference != null
            ? cancelActionReference.action.GetBindingDisplayString()
            : "Esc";
        
        feedbackText.text = string.Format(messageTemplate, cancelKey);
        feedbackText.gameObject.SetActive(true);
    }

    private void Hide()
    {
        feedbackText.gameObject.SetActive(false);
    }
}