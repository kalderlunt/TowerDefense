using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemData : MonoBehaviour
{
    public Button button;
    [SerializeField] private TMP_Text itemCostText;
    [SerializeField] private Image itemIcon;

    private Color opaqueColor = new Color(255, 255, 255, 255);
    private Color transparentColor = new Color(255, 255, 255, 0);

    public void SetPrice(int amount)
    {
        if (amount == -1)
        {
            itemCostText.text = "";
            return;
        }

        itemCostText.text = $"${amount}";
    }

    public void SetSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            SetColor(transparentColor);
            return;
        }

        SetColor(opaqueColor);
        itemIcon.sprite = sprite;
    }

    public void SetColor(Color color)
    {
        itemIcon.color = color;
    }
}