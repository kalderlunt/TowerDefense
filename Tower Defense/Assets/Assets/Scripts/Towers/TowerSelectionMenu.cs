using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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


    [Header("Inventory Side (Left Side)")]
    [SerializeField] private Transform inventoryContainer; // Conteneur pour les éléments de l'inventaire
    [SerializeField] private GameObject inventoryItemPrefab; // Préfabriqué pour les éléments de l'inventaire

    [SerializeField] private PlayerInventory playerInventory; // Référence à l'inventaire du joueur
    private List<GameObject> inventorySlots = new List<GameObject>();


    void Start()
    {
        PopulateTowerButtons();
        PopulateInventory();
    }

    private void OnDisable()
    {
        purchaseButton.onClick = null;
    }

    private void PopulateInventory()
    {

        for (int i = 0; i < playerInventory.towers.Count; i++) 
        {
            CreateSlotInventory(playerInventory.towers[i]);
            RefreshInventory(i, playerInventory.towers[i]);
        }
    }

    private void CreateSlotInventory(TowerData tower)
    {
        GameObject item = Instantiate(inventoryItemPrefab, inventoryContainer);
        inventorySlots.Add(item);
        
        InventoryItemData itemData = item.GetComponent<InventoryItemData>();
        itemData.SetSprite(null);
        itemData.SetPrice(-1);
    }

    private void AddTowerInSlotInventory(TowerData tower)
    {
        for (int i = 0; i < playerInventory.towers.Count; i++)
        {
            // check si le premier est deja pris
            // sinon tu passes au deuxieme
            // et si ils sont tous pris (debug.log())

            if (playerInventory.towers[i] != null)
            {
                Debug.Log("Ton inventaire est deja plein");
                continue;
            }

            if (playerInventory.towers[i] != tower)
            {
                Debug.Log("Tu a deja cette troupe");
                continue;
            }

            playerInventory.towers[i] = tower;
            RefreshInventory(i, tower);
            return;
        }
    }

    private void RefreshInventory(int index, TowerData tower)
    {
        InventoryItemData itemData = inventorySlots[index].GetComponent<InventoryItemData>();

        if (tower == null)
        {
            itemData.SetSprite(null);
            itemData.SetPrice(-1);
            itemData.button.onClick.RemoveAllListeners();
            return;
        }

        itemData.SetSprite(tower.sprite);
        itemData.SetPrice(tower.baseCost);
        itemData.button.onClick.AddListener(() => UnequipSelectedTower(index, tower));
    }



    private void EquipSelectedTower()
    {
        AddTowerInSlotInventory(selectedTower);
    }
    private void UnequipSelectedTower(int index, TowerData tower)
    {
        playerInventory.towers[index] = null;
        RefreshInventory(index, null);
    }




    private void PopulateTowerButtons()
    {
        foreach (TowerData tower in towers)
        {
            GameObject button = Instantiate(towerButtonPrefab, towerButtonContainer);
            ButtonSelectionData buttonData = button.GetComponent<ButtonSelectionData>();

            buttonData.SetName(tower.towerName);
            buttonData.button.onClick.AddListener(() => SelectTower(tower));

            if (tower.sprite != null)
            {
                buttonData.SetSprite(tower.sprite);
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
        if (selectedTower.sprite != null)
        {
            towerIconSprite.sprite = selectedTower.sprite;
        }
    }

    private void UpdateTowerPurchased()
    {
        Image purchaseButtonColor = purchaseButton.GetComponent<Image>();

        if (CheckIfTowerPurchased(selectedTower))
        {
            purchaseButtonText.text = "Equip";
            PurchaseButtonAddListener(EquipSelectedTower);
            purchaseButtonColor.color = unlockButtonColor;
            lockCreditImage.SetActive(false);
            return;
        }
        

        purchaseButtonText.text = selectedTower.unlockCost <= 0 ? "Free" : $"{selectedTower.unlockCost} Credits";
        PurchaseButtonAddListener(TowerLockedAction);
        purchaseButtonColor.color = lockButtonColor;
        lockCreditImage.SetActive(true);

    }

    private void TowerLockedAction()
    {
        if (playerInventory.credits >= selectedTower.unlockCost)
        {
            playerInventory.credits -= (uint)selectedTower.unlockCost;
            selectedTower.purchaseState = PurchaseState.Unlocked;
            UpdateTowerDetails();
            return;
        }

        Debug.Log("Tu n'as pas assez d'argent");
    }

    private bool CheckIfTowerPurchased(TowerData tower)
    {
        return tower.purchaseState == PurchaseState.Unlocked ? true : false;
    }

    private void PurchaseButtonAddListener(UnityAction call)
    {
        purchaseButton.onClick.RemoveAllListeners();

        if (call == null) return;
        purchaseButton.onClick.AddListener(call);
    }
}