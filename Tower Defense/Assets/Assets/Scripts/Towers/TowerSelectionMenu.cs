using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionMenu : MonoBehaviour
{
    [SerializeField] private TowerData[] towers; // Tableau de toutes les tours disponibles
    [SerializeField] Transform towerButtonContainer; // Conteneur pour les boutons des tours
    [SerializeField] private GameObject towerButtonPrefab; // Préfabriqué pour les boutons des tours

    [SerializeField] private TMP_Text towerNameText; // Texte UI pour afficher le nom de la tour
    [SerializeField] private TMP_Text towerCostText; // Texte UI pour afficher le coût de la tour
    [SerializeField] private TMP_Text towerDamageText; // Texte UI pour afficher les dégâts de la tour
    [SerializeField] private TMP_Text towerRangeText; // Texte UI pour afficher la portée de la tour
    [SerializeField] private Image towerIconImage; // Image UI pour afficher l'icône de la tour

    private TowerData selectedTower; // Tour actuellement sélectionnée

    void Start()
    {
        PopulateTowerButtons();
    }

    void PopulateTowerButtons()
    {
        foreach (TowerData tower in towers)
        {
            GameObject button = Instantiate(towerButtonPrefab, towerButtonContainer);
            button.GetComponentInChildren<Text>().text = tower.towerName;
            button.GetComponent<Button>().onClick.AddListener(() => SelectTower(tower));
            if (tower.buttonIcon != null)
            {
                button.transform.Find("Icon").GetComponent<Image>().sprite = tower.buttonIcon;
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
        towerCostText.text = $"Cost: $ {selectedTower.baseCost}";
        towerDamageText.text = $"Damage: {selectedTower.baseDamage}";
        towerRangeText.text = $"Range: {selectedTower.baseRange}";

        if (selectedTower.buttonIcon != null)
        {
            towerIconImage.sprite = selectedTower.buttonIcon;
        }
    }
}
