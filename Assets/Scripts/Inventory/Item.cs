using System;
using UnityEngine;

[Serializable]
public class Item {

    public enum ItemType
    {
        Catfood,
        Pie,
        EnergyDrink,
        Tea,
        Mouse,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Catfood: return ItemAssets.Instance.catfoodSprite;
            case ItemType.Pie: return ItemAssets.Instance.pieSprite;
            case ItemType.EnergyDrink: return ItemAssets.Instance.energydrinkSprite;
            case ItemType.Tea: return ItemAssets.Instance.teaSprite;
            case ItemType.Mouse: return ItemAssets.Instance.mouseSprite;
        }
        return null;
    }
}
