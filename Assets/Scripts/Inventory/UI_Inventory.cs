using System;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
   public Inventory uiInventory;
   [SerializeField] private Transform itemSlotContainer;
   [SerializeField] private ItemSlot itemSlotTemplate;

   public Transform canvas;
   public GameObject itemInfoPrefab;
   private GameObject currentItemInfo = null;
   

   public void SetInventory(Inventory inventory)
   {
      uiInventory = inventory;
      RefreshInventoryItems();
   }

   private void DestroyAll()
   {
      foreach (Transform item in itemSlotContainer)
      {
         Destroy(item.gameObject);
      }
   }

   public void RefreshInventoryItems()
   {
      DestroyAll();
      foreach (Item item in uiInventory.GetItemList())
      {
         ItemSlot itemSlot = Instantiate(itemSlotTemplate, itemSlotContainer);
         itemSlot.gameObject.SetActive(true);
         itemSlot.SetupData(item, UseItem(item));
      }
   }

   private Action UseItem(Item item)
   {
      switch (item.itemType)
      {
         case Item.ItemType.Catfood:
            return UseCatFood;
            break;
         case Item.ItemType.Pie:
            return IncreaseHealth;
      }

      return null;
   }

   private void IncreaseHealthDouble()
   {
      PlayerManager.Instance.playerLife.IncreaseHealth(PlayerLife.smallHeart * 2);
   }
   private void IncreaseHealth()
   {
      PlayerManager.Instance.playerLife.IncreaseHealth(PlayerLife.smallHeart);
      uiInventory.UseItem(new Item()
      {
         itemType = Item.ItemType.Pie
      });
      RefreshInventoryItems();
   }

   private void UseCatFood()
   {
      IncreaseHealthDouble();
      uiInventory.UseItem(new Item()
      {
         itemType = Item.ItemType.Catfood
      });
      RefreshInventoryItems();
   }
   
   private void UsePie()
   {
      IncreaseHealth();
      uiInventory.UseItem(new Item()
      {
         itemType = Item.ItemType.Pie
      });
      RefreshInventoryItems();
   }
   
   private void UseKnife()
   {
      IncreaseHealth();
      uiInventory.UseItem(new Item()
      {
         itemType = Item.ItemType.Knife
      });
      RefreshInventoryItems();
   }
   
   

}
