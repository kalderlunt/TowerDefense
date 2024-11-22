using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower Defense/Tower")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public int unlockCost; // Co�t pour d�bloquer dans le menu
    public int baseCost; // Co�t initial
    public int baseCellingPrice; // Prix de vente de base

    public enum TowerType { Offensive, Support }
    public TowerType type; // Type de la tour (offensive ou support)

    public enum PlacementType { Ground, Air }
    public PlacementType placement; // Placement possible

    public float baseDamage; // D�g�ts de base
    public enum DamageType { Physical, Magical, True }
    public DamageType typeDamage; // Type de d�g�ts

    public enum AttackMode { Single, Multiple, Random, Closest, Farthest }
    public AttackMode Mode; // Mode d'attaque de la tour

    public float baseFirerate; // Cadence de tir de base
    public float baseRange; // Port�e de base

    public bool hiddenDetection; // D�tection d'ennemis invisibles (apr�s niveau 2)
    public bool leadDetection; // D�tection d'ennemis blind�s
    public bool flyingDetection; // D�tection d'ennemis volants
    public bool immunities; // Immunit� aux alt�rations d'�tat

    public float placementFootprint; // Taille approximative pour le placement
}