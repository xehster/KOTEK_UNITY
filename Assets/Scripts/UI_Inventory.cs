using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
   private Inventory inventory;
   [SerializeField] private Transform itemSlotContainer;
   [SerializeField] private ItemSlot itemSlotTemplate;

   public void SetInventory(Inventory inventory)
   {
      this.inventory = inventory;
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
      int x = 0;
      int y = 0;
      float itemSlotCellSize = 50f;
      foreach (Item item in inventory.GetItemList())
      {
         ItemSlot itemSlot = Instantiate(itemSlotTemplate, itemSlotContainer);
         RectTransform itemSlotRectTransform = itemSlot.GetComponent<RectTransform>();
         itemSlotRectTransform.gameObject.SetActive(true);
         itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
         itemSlot.SetupData(item.GetSprite(), item.amount);
         x++;
         if (x > 4)
         {
            x = 0;
            y++;
         }
      }
   }
}
