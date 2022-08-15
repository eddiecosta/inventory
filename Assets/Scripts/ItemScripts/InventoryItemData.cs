using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is ascriptable object that defines an item is in our game.
/// It could be inherited from to have branched version of items, for example potions and equipment.
/// </summary>

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
    public int Value;
}
