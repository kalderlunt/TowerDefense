using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameInventory : PlayerInventory
{
    [SerializeField] private GameObject inventoryItemPrefab; // Préfabriqué des slots d'inventaire
    [SerializeField] private Transform inventoryContainer; // Conteneur des slots d'inventaire
    private List<GameObject> inventorySlots = new List<GameObject>();

    private void Start()
    {
        PopulateInventory();
    }

    /// <summary>
    /// Remplit l'inventaire avec les données des tours.
    /// </summary>
    protected override void PopulateInventory()
    {
        for (int i = 0; i < inventoryData.towers.Count; i++)
        {
            CreateSlotInventory(inventorySlots, inventoryItemPrefab, inventoryContainer, () => PlacePreviewTower(i));
            RefreshInventory(inventorySlots, i, inventoryData.towers[i], () => PlacePreviewTower(i));
        }
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

        ButtonAddListener(itemData.button, () => PlacePreviewTower(index));
    }


    /// <summary>
    /// Crée une tour en mode prévisualisation.
    /// </summary>
    /// <param name="tower">Données de la tour à prévisualiser</param>
    protected void PlacePreviewTower(int indexClicked)
    {
        Debug.Log($"Prévisualisation de la tour : {inventoryData.towers[indexClicked].towerName}");
    }
}