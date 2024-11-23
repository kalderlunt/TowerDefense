using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionMenu : MonoBehaviour
{
    [Header("Tower Selection Side")]
    [SerializeField] private TowerData[] towers; // Tableau de toutes les tours disponibles

    [Space(30)]
    [SerializeField] Transform towerButtonContainer; // Conteneur pour les boutons des tours
    [SerializeField] private GameObject towerButtonPrefab; // Préfabriqué pour les boutons des tours

    [Header("Info Side (Right Side)")]
    [SerializeField] private TMP_Text towerNameText; // Texte UI pour afficher le nom de la tour

    [SerializeField] private TMP_Text towerCostText; // Texte UI pour afficher le coût de la tour
    [SerializeField] private TMP_Text towerDamageText; // Texte UI pour afficher les dégâts de la tour
    [SerializeField] private TMP_Text towerDamageTypeText; // Texte UI pour afficher le type de dégâts de la tour

    [SerializeField] private TMP_Text towerRangeText; // Texte UI pour afficher la portée de la tour
    [SerializeField] private TMP_Text towerPlacementText; // Texte UI pour afficher le type sur lequel la tour peut se poser

    [SerializeField] private Image towerIconSprite; // Image UI pour afficher l'icône de la tour

    [Header("Purchase Button")]
    [SerializeField] private Button purchaseButton; // Bouton pour acheter la tour
    [SerializeField] private TMP_Text purchaseButtonText; // Texte UI pour afficher le texte du bouton d'achat
    [SerializeField] private GameObject lockCreditImage;

    [SerializeField] private Color unlockButtonColor; // Couleur du bouton d'achat quand il l'a debloque
    [SerializeField] private Color lockButtonColor; // Couleur du bouton d'achat quand il ne la pas debloque

    private TowerData selectedTower; // Tour actuellement sélectionnée

    void Start()
    {
        PopulateTowerButtons();
    }

    private void PopulateTowerButtons()
    {
        foreach (TowerData tower in towers)
        {
            GameObject button = Instantiate(towerButtonPrefab, towerButtonContainer);
            ButtonSelectionData buttonData = button.GetComponent<ButtonSelectionData>();

            buttonData.SetName(tower.towerName);
            buttonData.button.onClick.AddListener(() => SelectTower(tower));

            if (tower.buttonSprite != null)
            {
                buttonData.SetSprite(tower.buttonSprite);
            }

            if (tower == towers[0])
            {
                SelectTower(tower);
            }
        }
    }

    private void SelectTower(TowerData tower)
    {
        selectedTower = tower;
        UpdateTowerDetails();
    }

    private void UpdateTowerDetails()
    {
        UpdateTexts();

        UpdateSprite();

        UpdateTowerPurchased();
    }

    private void UpdateTexts()
    {
        towerNameText.text = selectedTower.towerName;

        towerCostText.text = $"${selectedTower.baseCost}";
        towerDamageText.text = $"{selectedTower.damage}";
        towerDamageTypeText.text = $"{selectedTower.damageType}";
        towerRangeText.text = $"{selectedTower.rangeInfo}";
        towerPlacementText.text = $"{selectedTower.placement}";
    }

    private void UpdateSprite()
    {
        if (selectedTower.buttonSprite != null)
        {
            towerIconSprite.sprite = selectedTower.buttonSprite;
        }
    }

    private void UpdateTowerPurchased()
    {
        Image purchaseButtonColor = purchaseButton.GetComponent<Image>();

        if (CheckIfTowerPurchased(selectedTower))
        {
            Debug.Log("La tour a été achetée.");
            purchaseButtonText.text = "Equip";
            purchaseButtonColor.color = unlockButtonColor;
            lockCreditImage.SetActive(false);
            return;
        }
        

        Debug.Log("La tour n'a pas encore été achetée.");

        if (selectedTower.unlockCost <= 0)
            purchaseButtonText.text = "Free";
        else
            purchaseButtonText.text = $"{selectedTower.unlockCost} Credits";

        purchaseButtonColor.color = lockButtonColor;
        lockCreditImage.SetActive(true);

    }

    private bool CheckIfTowerPurchased(TowerData tower)
    {
        return tower.purchaseState == PurchaseState.Unlock ? true : false;
    }
}