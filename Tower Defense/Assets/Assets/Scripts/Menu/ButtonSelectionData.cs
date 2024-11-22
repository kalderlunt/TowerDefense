using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectionData : MonoBehaviour
{
    public Button button { get; private set; }
    public Image imageSlot { get; private set; }
    public TMP_Text nameText { get; private set; }

    private void Awake()
    {
        button = GetComponent<Button>();
        imageSlot = GetComponentInChildren<Image>();
        nameText = GetComponentInChildren<TMP_Text>();
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetSprite(Sprite sprite)
    {
        imageSlot.sprite = sprite;
    }

/*    private void Update()
    {
        if (button.interactable)
        {
            imageSlot.color = Color.white;
            nameText.color = Color.white;
        }
        else
        {
            imageSlot.color = new Color(0.5f, 0.5f, 0.5f);
            nameText.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }*/
}