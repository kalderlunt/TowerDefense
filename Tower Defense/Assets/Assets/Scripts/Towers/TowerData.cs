using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower Defense/Tower")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public int unlockCost; // Coût pour débloquer dans le menu
    public int baseCost; // Coût initial
    public int baseCellingPrice; // Prix de vente de base

    public enum TowerType { Offensive, Support }
    public TowerType type; // Type de la tour (offensive ou support)

    public enum PlacementType { Ground, Air }
    public PlacementType placement; // Placement possible

    public float baseDamage; // Dégâts de base
    public enum DamageType { Physical, Magical, True }
    public DamageType typeDamage; // Type de dégâts

    public enum AttackMode { Single, Multiple, Random, Closest, Farthest }
    public AttackMode Mode; // Mode d'attaque de la tour

    public float baseFirerate; // Cadence de tir de base
    public float baseRange; // Portée de base

    public bool hiddenDetection; // Détection d'ennemis invisibles (après niveau 2)
    public bool leadDetection; // Détection d'ennemis blindés
    public bool flyingDetection; // Détection d'ennemis volants
    public bool immunities; // Immunité aux altérations d'état

    public float placementFootprint; // Taille approximative pour le placement
}