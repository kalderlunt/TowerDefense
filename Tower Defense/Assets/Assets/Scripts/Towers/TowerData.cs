using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower Defense/Tower")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public TowerLevel level;

    [Header("Price")]
    public int unlockCost; // Coût pour débloquer dans le menu
    public int baseCost; // Coût initial dans le jeu 
    public int baseCellingPrice; // Prix de vente de base

    [Header("Placement")]
    public TowerType type; // Type de la tour (offensive ou support)
    public PlacementType placement; // Placement possible sur le terrain (sol, montagne, air)

    [Header("Parameters Damage")]
    public float baseDamage; // Dégâts de base
    public float bulletsOfNumber = 1f;
    public Damage damage; // Niveau de degat
    public DamageType damageType; // Type de dégâts
    public float baseFirerate; // Cadence de tir de base
    public RangeInfo rangeInfo;
    public float baseRange; // Portée de base

    [Header("Minimum Detection")]
    public MinimumDetection hiddenDetection; // Détection d'ennemis invisibles (après niveau 2)
    public MinimumDetection leadDetection; // Détection d'ennemis blindés
    public MinimumDetection flyingDetection; // Détection d'ennemis volants
    public Immunities immunities; // Immunité aux altérations d'état

    public FootPrint placementFootprint; // Taille approximative pour le placement

    [Header("Locked / Unlock")]
    public PurchaseState purchaseState; // si le joueur peut acheter 

    [Header("Menu Selection")]
    public Sprite sprite; // Optionnel, pour afficher l'icône sur le bouton
}


public enum PlacementType 
{ 
    Ground,
    Cliff,
    Air,
}

public enum TowerType 
{ 
    Offensive, 
    Support 
}

public enum Damage
{
    Low,
    Medium,
    High,
}

public enum DamageType 
{
    Single,
    Burst,
    PierceSpread,
    Splash,
}

public enum RangeInfo
{
    Low,
    Medium,
    High,
    VeryHigh,
}

public enum TowerLevel
{
    level1,
    level2,
    level3,
    level4,
    level5,
}

public enum MinimumDetection
{
    NotApplicable,
    level1,
    level2,
    level3,
    level4,
    level5,
}

public enum Immunities
{
    None,
    Stun,
}

public enum FootPrint
{
    NotApplicable,
    Average,
}

public enum PurchaseState
{
    Locked,
    Unlocked,
}