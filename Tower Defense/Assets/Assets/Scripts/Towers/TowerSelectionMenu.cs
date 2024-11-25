using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TowerSelectionMenu : PlayerInventory
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

    [Header("Inventory Side (Left Side)")]
    [SerializeField] private Transform inventoryContainer; // Conteneur pour les éléments de l'inventaire
    [SerializeField] private GameObject inventoryItemPrefab; // Préfabriqué pour les éléments de l'
    private List<GameObject> inventorySlots = new List<GameObject>(5);


    void Start()
    {
        PopulateTowerButtons();
        PopulateInventory();
    }

    private void OnDisable()
    {
        purchaseButton.onClick = null;
    }

    #region Inventory
    protected override void PopulateInventory()
    {
        for (int i = 0; i < inventoryData.towers.Count; i++) 
        {
            CreateSlotInventory(inventorySlots, inventoryItemPrefab, inventoryContainer, () => UnequipSelectedTower(i));
            // Passer l'index à la fonction de mise à jour de l'inventaire
            RefreshInventory(inventorySlots, i, inventoryData.towers[i], () => UnequipSelectedTower(i)); // i est l'index correct ici
        }
    }

    private void AddTowerInSlotInventory(TowerData tower)
    {
        /*for (int i = 0; i < playerInventory.towers.Count; i++)
        {
            if (playerInventory.towers[i] == tower)
            {
                return;
            }

            Debug.Log("Tu a deja cette troupe");
        }*/
        if (inventoryData.towers.Contains(tower))
        {
            Debug.Log("Cette tour est déjà dans l'inventaire !");
            return;
        }

        // Ajoute la tour dans un slot vide
        for (int i = 0; i < inventoryData.towers.Count; i++)
        {
            if (inventoryData.towers[i] == null)
            {
                inventoryData.towers[i] = tower;
                RefreshInventory(inventorySlots, i, tower, () => UnequipSelectedTower(i)); // Passer l'index correct ici
                return;
            }
        }

        Debug.Log("L'inventaire est plein !");
    }


    protected override void RefreshInventory(List<GameObject> inventorySlots, int index, TowerData tower, UnityAction clickAction)
    {
        Debug.Log($"Refreshing inventory at index {index} with tower {tower?.towerName}");

        if (index < 0 || index >= inventorySlots.Count)
        {
            Debug.LogWarning("Index hors des limites de la liste des slots !");
            return;
        }

        InventoryItemData itemData = inventorySlots[index].GetComponent<InventoryItemData>();

        if (tower == null)
        {
            itemData.SetSprite(null);
            itemData.SetPrice(-1);
            itemData.button.onClick.RemoveAllListeners();
            return;
        }

        itemData.SetSprite(tower.spritesLvl[0]);
        itemData.SetPrice(tower.baseCost);
        ButtonAddListener(itemData.button, () => UnequipSelectedTower(index));
    }


    private void EquipSelectedTower()
    {
        AddTowerInSlotInventory(selectedTower);
    }
    private void UnequipSelectedTower(int index)
    {
        Debug.Log("Taille de l'inventaire : " + inventoryData.towers.Count);
        Debug.Log("Index sélectionné : " + index);

        if (index >= 0 && index < inventoryData.towers.Count)
        {
            inventoryData.towers[index] = null;
            RefreshInventory(inventorySlots, index, null, null); // Mettre à jour l'inventaire après avoir retiré la tour
        }
        else
        {
            Debug.LogWarning("Index hors de portée : " + index);
        }
    }
    #endregion


    #region MarketTower
    private void PopulateTowerButtons()
    {
        foreach (TowerData tower in towers)
        {
            GameObject button = Instantiate(towerButtonPrefab, towerButtonContainer);
            ButtonSelectionData buttonData = button.GetComponent<ButtonSelectionData>();

            buttonData.SetName(tower.towerName);
            ButtonAddListener(buttonData.button, () => SelectTower(tower));

            if (tower.spritesLvl[0] != null)
            {
                buttonData.SetSprite(tower.spritesLvl[0]);
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
        if (selectedTower.spritesLvl[0] != null)
        {
            towerIconSprite.sprite = selectedTower.spritesLvl[0];
        }
    }

    private void UpdateTowerPurchased()
    {
        Image purchaseButtonColor = purchaseButton.GetComponent<Image>();

        if (CanTowerPurchased(selectedTower))
        {
            purchaseButtonText.text = "Equip";
            ButtonAddListener(purchaseButton, EquipSelectedTower);
            purchaseButtonColor.color = unlockButtonColor;
            lockCreditImage.SetActive(false);
            return;
        }
        

        purchaseButtonText.text = selectedTower.unlockCost <= 0 ? "Free" : $"{selectedTower.unlockCost} Credits";
        ButtonAddListener(purchaseButton, TowerLockedAction);
        purchaseButtonColor.color = lockButtonColor;
        lockCreditImage.SetActive(true);

    }

    private void TowerLockedAction()
    {
        if (inventoryData.credits >= selectedTower.unlockCost)
        {
            inventoryData.credits -= (uint)selectedTower.unlockCost;
            selectedTower.purchaseState = PurchaseState.Unlocked;
            UpdateTowerDetails();
            return;
        }

        Debug.Log("Tu n'as pas assez d'argent");
    }

    private bool CanTowerPurchased(TowerData tower)
    {
        return tower.purchaseState == PurchaseState.Unlocked ? true : false;
    }
    #endregion
}