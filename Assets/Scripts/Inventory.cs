using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        AddItem(new Item { itemType = Item.ItemType.Catfood, amount = 2});
        AddItem(new Item { itemType = Item.ItemType.Pie, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Pie, amount = 1});
        AddItem(new Item { itemType = Item.ItemType.Tea, amount = 1});
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        var existedItem = itemList.FirstOrDefault(x => x.itemType == item.itemType);
        if (existedItem != null)
        {
            existedItem.amount += 1;
        }
        else
        {
            itemList.Add(item);  
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
