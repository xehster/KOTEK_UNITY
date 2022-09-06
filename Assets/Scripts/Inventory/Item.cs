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
        Knife,
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
            case ItemType.Knife: return ItemAssets.Instance.knifeSprite;
        }
        return null;
    }

    public ItemInfoData GetTextData()
    {
        switch (itemType)
        {
            case ItemType.Catfood: return ItemAssets.Instance.catFoodData;
            case ItemType.Pie: return ItemAssets.Instance.pieData;
            case ItemType.EnergyDrink: return ItemAssets.Instance.EnergyDrinkData;
            case ItemType.Tea: return ItemAssets.Instance.TeaData;
            case ItemType.Mouse: return ItemAssets.Instance.MouseData;
            case ItemType.Knife: return ItemAssets.Instance.KnifeData;
        }

        return null;
    }


}
