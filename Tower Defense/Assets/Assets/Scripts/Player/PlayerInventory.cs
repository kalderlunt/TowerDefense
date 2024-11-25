using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public SO_Inventory inventoryData; // Référence au ScriptableObject SO_Inventory
    public TowerData selectedTower; // Tour actuellement sélectionnée

    /// <summary>
    /// Fonction virtuelle pour remplir l'inventaire (doit être implémentée par les classes enfants).
    /// </summary>
    protected virtual void PopulateInventory() 
    {

    }

    /// <summary>
    /// Crée un emplacement d'inventaire.
    /// </summary>
    /// <param name="inventorySlots">Liste des slots d'inventaire où l'emplacement sera ajouté</param>
    /// <param name="inventoryItemPrefab">Préfabriqué de l'élément d'inventaire</param>
    /// <param name="inventoryContainer">Conteneur où les éléments d'inventaire seront instanciés</param>
    /// <param name="clickAction">L'action que l'on donnera au bouton</param>
    protected void CreateSlotInventory(List<GameObject> inventorySlots, GameObject inventoryItemPrefab, Transform inventoryContainer, UnityAction clickAction)
    {
        GameObject item = Instantiate(inventoryItemPrefab, inventoryContainer);
        inventorySlots.Add(item);

        InventoryItemData itemData = item.GetComponent<InventoryItemData>();
        itemData.SetSprite(null);
        itemData.SetPrice(-1);
        ButtonAddListener(itemData.button, clickAction);
    }

    /// <summary>
    /// Met à jour un slot d'inventaire avec les données de la tour.
    /// </summary>
    /// <param name="tower">Données de la tour à afficher dans le slot</param>
    /// <param name="clickAction">Action à associer au clic du bouton</param>
    protected void RefreshInventory(List<GameObject> inventorySlots, int index, TowerData tower, UnityAction clickAction)
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

        RefreshButton(index, itemData.button, clickAction);
    }

    protected virtual void RefreshButton(int index, Button button, UnityAction clickAction)
    {
        ButtonAddListener(button, clickAction);
    }

    /// <summary>
    /// Ajoute un listener à un bouton.
    /// </summary>
    /// <param name="button">Le bouton auquel ajouter un listener</param>
    /// <param name="call">Action à appeler lors du clic sur le bouton</param>
    protected void ButtonAddListener(Button button, UnityAction call)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(call);
    }
}