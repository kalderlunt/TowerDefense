using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameInventory : PlayerInventory
{
    [SerializeField] private GameObject inventoryItemPrefab; // Pr�fabriqu� des slots d'inventaire
    [SerializeField] private Transform inventoryContainer; // Conteneur des slots d'inventaire
    [SerializeField] private Transform parentStorage;
    private List<GameObject> inventorySlots = new List<GameObject>();
    private GameObject previewTower; // Objet temporaire pour la pr�visualisation

    private void Start()
    {
        PopulateInventory();
    }

    /// <summary>
    /// Remplit l'inventaire avec les donn�es des tours.
    /// </summary>
    protected override void PopulateInventory()
    {
        for (int i = 0; i < inventoryData.towers.Count; i++)
        {
            CreateSlotInventory(inventorySlots, inventoryItemPrefab, inventoryContainer, () => PlacePreviewTower(i));
            RefreshInventory(inventorySlots, i, inventoryData.towers[i], () => PlacePreviewTower(i));
        }
    }

    protected override void RefreshButton(int index, Button button, UnityAction clickAction)
    {
        ButtonAddListener(button, () => PlacePreviewTower(index));
    }

    private void Update()
    {
        if (previewTower != null)
        {
            previewTower.transform.position = GetMouseWorldPosition();

            if (Input.GetMouseButtonDown(0))
            {
                TryPlaceTower();
            }
        }
    }

    private void TryPlaceTower()
    {
        if (IsValidPlacement(previewTower.transform.position))
        {
            TowerSelectable towerSelected = previewTower.GetComponent<TowerSelectable>();
            towerSelected.PlaceTower();
            towerSelected.Deselect();
            previewTower = null;
            Debug.Log($"Tour placee : {towerSelected.tower.data.towerName}");
        }
        else
        {
            Debug.LogWarning("Position invalide pour le placement !");
        }
    }

    private bool IsValidPlacement(Vector3 position)
    {
        // Ajouter des v�rifications comme : zone accessible, pas d'objet bloquant, etc.
        return true;
    }

    /// <summary>
    /// Cr�e une tour en mode pr�visualisation.
    /// </summary>
    /// <param name="indexClicked">Donn�es de la tour � pr�visualiser</param>
    protected void PlacePreviewTower(int indexClicked)
    {
        if (previewTower = null)
        {
            return;
        }

        TowerData selectedTower = inventoryData.towers[indexClicked];
        previewTower = Instantiate(selectedTower.objPrefabs, parentStorage);
        previewTower.transform.position = GetMouseWorldPosition();
        TowerSelectable tower = previewTower.GetComponent<TowerSelectable>();
        tower.UnPlacedTower();
        tower.Select();

        Debug.Log($"Previsualisation de la tour : {selectedTower.towerName}");
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == previewTower || hit.collider.GetComponent<Tower>())
            {
                return previewTower.transform.position;
            }

            return hit.point + Vector3.up;
        }
        return Vector3.zero;
    }
}