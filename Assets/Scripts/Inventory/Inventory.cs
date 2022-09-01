using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{

    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
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
    

    public void UseItem(Item item)
    {
        var existedItem = itemList.FirstOrDefault(x => x.itemType == item.itemType);
        if (existedItem != null)
        {
            existedItem.amount -= 1;
            
            if (existedItem.amount <= 0)
            {
                itemList.Remove(existedItem);
            }
        }
    }
    
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
