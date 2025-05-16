using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public SO_Inventory inventoryData; // R�f�rence au ScriptableObject SO_Inventory
    public TowerData selectedTower; // Tour actuellement s�lectionn�e

    /// <summary>
    /// Fonction virtuelle pour remplir l'inventaire (doit �tre impl�ment�e par les classes enfants).
    /// </summary>
    protected virtual void PopulateInventory() 
    {

    }

    /// <summary>
    /// Cr�e un emplacement d'inventaire.
    /// </summary>
    /// <param name="inventorySlots">Liste des slots d'inventaire o� l'emplacement sera ajout�</param>
    /// <param name="inventoryItemPrefab">Pr�fabriqu� de l'�l�ment d'inventaire</param>
    /// <param name="inventoryContainer">Conteneur o� les �l�ments d'inventaire seront instanci�s</param>
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
    /// Met � jour un slot d'inventaire avec les donn�es de la tour.
    /// </summary>
    /// <param name="tower">Donn�es de la tour � afficher dans le slot</param>
    /// <param name="clickAction">Action � associer au clic du bouton</param>
    protected void RefreshInventory(List<GameObject> inventorySlots, int index, TowerData tower, UnityAction clickAction)
    {
        //Debug.Log($"Refreshing inventory at index {index} with tower {tower?.towerName}");

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
    /// Ajoute un listener � un bouton.
    /// </summary>
    /// <param name="button">Le bouton auquel ajouter un listener</param>
    /// <param name="call">Action � appeler lors du clic sur le bouton</param>
    protected void ButtonAddListener(Button button, UnityAction call)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(call);
    }
}