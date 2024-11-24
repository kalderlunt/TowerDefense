using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower Defense/Tower")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public TowerLevel level;

    [Header("Price")]
    public int unlockCost; // Co�t pour d�bloquer dans le menu
    public int baseCost; // Co�t initial dans le jeu 
    public int baseCellingPrice; // Prix de vente de base

    [Header("Placement")]
    public TowerType type; // Type de la tour (offensive ou support)
    public PlacementType placement; // Placement possible sur le terrain (sol, montagne, air)

    [Header("Parameters Damage")]
    public float baseDamage; // D�g�ts de base
    public float bulletsOfNumber = 1f;
    public Damage damage; // Niveau de degat
    public DamageType damageType; // Type de d�g�ts
    public float baseFirerate; // Cadence de tir de base
    public RangeInfo rangeInfo;
    public float baseRange; // Port�e de base

    [Header("Minimum Detection")]
    public MinimumDetection hiddenDetection; // D�tection d'ennemis invisibles (apr�s niveau 2)
    public MinimumDetection leadDetection; // D�tection d'ennemis blind�s
    public MinimumDetection flyingDetection; // D�tection d'ennemis volants
    public Immunities immunities; // Immunit� aux alt�rations d'�tat

    public FootPrint placementFootprint; // Taille approximative pour le placement

    [Header("Locked / Unlock")]
    public PurchaseState purchaseState; // si le joueur peut acheter 

    [Header("Menu Selection")]
    public Sprite sprite; // Optionnel, pour afficher l'ic�ne sur le bouton
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