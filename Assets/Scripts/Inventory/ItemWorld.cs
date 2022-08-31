using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public static ItemWorld SpawnItemWorld(Vector2 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    public void SetPrefabData()
    {
        SetItem(item);
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
