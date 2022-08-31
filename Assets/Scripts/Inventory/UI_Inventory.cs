using System;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
   private Inventory uiInventory;
   [SerializeField] private Transform itemSlotContainer;
   [SerializeField] private ItemSlot itemSlotTemplate;

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

   private void RefreshInventoryItems()
   {
      DestroyAll();
      foreach (Item item in uiInventory.GetItemList())
      {
         ItemSlot itemSlot = Instantiate(itemSlotTemplate, itemSlotContainer);
         itemSlot.gameObject.SetActive(true);
         itemSlot.SetupData(item.GetSprite(), item.amount, UseItem(item));
      }
   }

   private Action UseItem(Item item)
   {
      switch (item.itemType)
      {
         case Item.ItemType.Catfood:
            return IncreaseHealthDouble;
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
   }

   private void TeleportPlayerToSpawn()
   {
      PlayerManager.Instance.TeleportPlayerTo(new Vector2(0, 0));
      uiInventory.UseItem(new Item()
      {
         itemType = Item.ItemType.Catfood
      });
   }
}
