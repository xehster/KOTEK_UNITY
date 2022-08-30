using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text counterText;

    public void SetupData(Sprite sprite, int count)
    {
        iconImage.sprite = sprite;
        counterText.text = count.ToString();
    }
}
