using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class TowerSelectable : MonoBehaviour, ISelectable
{
    [SerializeField] private Collider colliderYouCanClickOn;
    public bool isPlaced { get; private set; }
    public bool isSelected { get; private set; }

    public Tower tower { get; private set; }

    private void OnEnable()
    {
        tower = GetComponent<Tower>();
        Assert.IsNotNull(tower, "Le script Tower n'est pas attach� � cet objet.");

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

        //Debug.Log($"Tour selectionnee : {gameObject.name}");
    }

    public void Deselect()
    {
        if (!isSelected)
        {
            return;
        }

        isSelected = false;
        DisplayTowerRange();

        //Debug.Log($"Tour {gameObject.name} deselectionnee.");
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

    public void PlaceTower()
    {
        isPlaced = true;
        colliderYouCanClickOn.enabled = true;
    }

    public void UnPlacedTower()
    {
        // remettre dans le pool system
        isPlaced = false;
        colliderYouCanClickOn.enabled = false;
    }
}