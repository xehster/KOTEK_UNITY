using UnityEngine;

public class ItemAssets : MonoBehaviour
{
   public static ItemAssets Instance { get; private set; }

   private void Awake()
   {
      Instance = this;
      InitItemsOnScene();
   }

   private void InitItemsOnScene()
   {
       var items = FindObjectsOfType<ItemWorld>();
       foreach (var item in items)
       {
           item.SetPrefabData();
       }
   }

   public Transform pfItemWorld;
   
   public Sprite catfoodSprite;
   public Sprite pieSprite;
   public Sprite energydrinkSprite;
   public Sprite teaSprite;
   public Sprite mouseSprite;
   public Sprite knifeSprite;

   public ItemInfoData catFoodData;
   public ItemInfoData pieData;
   public ItemInfoData EnergyDrinkData;
   public ItemInfoData TeaData;
   public ItemInfoData MouseData;
   public ItemInfoData KnifeData;
}
