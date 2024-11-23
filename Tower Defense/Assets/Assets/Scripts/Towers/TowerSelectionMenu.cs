using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionMenu : MonoBehaviour
{
    [Header("Tower Selection Side")]
    [SerializeField] private TowerData[] towers; // Tableau de toutes les tours disponibles

    [Space(30)]
    [SerializeField] Transform towerButtonContainer; // Conteneur pour les boutons des tours
    [SerializeField] private GameObject towerButtonPrefab; // Pr�fabriqu� pour les boutons des tours

    [Header("Info Side (Right Side)")]
    [SerializeField] private TMP_Text towerNameText; // Texte UI pour afficher le nom de la tour

    [SerializeField] private TMP_Text towerCostText; // Texte UI pour afficher le co�t de la tour
    [SerializeField] private TMP_Text towerDamageText; // Texte UI pour afficher les d�g�ts de la tour
    [SerializeField] private TMP_Text towerDamageTypeText; // Texte UI pour afficher le type de d�g�ts de la tour

    [SerializeField] private TMP_Text towerRangeText; // Texte UI pour afficher la port�e de la tour
    [SerializeField] private TMP_Text towerPlacementText; // Texte UI pour afficher le type sur lequel la tour peut se poser

    [SerializeField] private Image towerIconSprite; // Image UI pour afficher l'ic�ne de la tour

    private TowerData selectedTower; // Tour actuellement s�lectionn�e

    void Start()
    {
        PopulateTowerButtons();
    }

    void PopulateTowerButtons()
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

    void SelectTower(TowerData tower)
    {
        selectedTower = tower;
        UpdateTowerDetails();
    }

    void UpdateTowerDetails()
    {
        towerNameText.text = selectedTower.towerName;

        towerCostText.text = $"${selectedTower.baseCost}";
        towerDamageText.text = $"{selectedTower.damage}";
        towerDamageTypeText.text = $"{selectedTower.damageType}";
        towerRangeText.text = $"{selectedTower.rangeInfo}";
        towerPlacementText.text = $"{selectedTower.placement}";

        if (selectedTower.buttonSprite != null)
        {
            towerIconSprite.sprite = selectedTower.buttonSprite;
        }
    }
}