using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Tower Defense/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    public uint credits = 0;

    [Space(20)]
    public List<TowerData> towers = new List<TowerData>();
}
