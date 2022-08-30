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
         itemSlot.SetupData(item.GetSprite(), item.amount);
      }
   }
}
