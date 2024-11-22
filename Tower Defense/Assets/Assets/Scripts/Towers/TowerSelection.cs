using NUnit.Framework;
using UnityEngine;

public class TowerSelection : MonoBehaviour, ISelectable
{
    public bool isPlaced { get; private set; }
    public bool isSelected { get; private set; }
    private Tower tower;

    private void OnEnable()
    {
        tower = GetComponent<Tower>();
        Assert.IsNotNull(tower, "Le script Tower n'est pas attaché à cet objet.");

        isPlaced = false;
        isSelected = false;
    }


    public void Select()
    {
        if (isSelected)
        {
            return;
        }

        isSelected = true;
        DisplayTowerRange();

        Debug.Log($"Tour sélectionnée : {gameObject.name}");
    }

    public void Deselect()
    {
        if (!isSelected)
        {
            return;
        }

        isSelected = false;
        DisplayTowerRange();

        Debug.Log($"Tour {gameObject.name} désélectionnée.");
    }

    private void DisplayTowerRange()
    {
        Assert.IsNotNull(tower, $"Tower {tower.data.towerName} n'a le script Tower");

        if (isSelected)
        {
            tower.zoneDisplay.ShowZone(tower.data);
        }
        else
        {
            tower.zoneDisplay.HideZone();
        }
    }


    private void MoveTower(Vector3 mouseWorldPosition)
    {
        if (isPlaced) { return; }
        if (!isSelected) { return; }

        //Debug.Log($"Position de la souris : {mouseWorldPosition}");
        transform.position = mouseWorldPosition;
    }

    private void PlaceTower()
    {


        isPlaced = true;
    }
}