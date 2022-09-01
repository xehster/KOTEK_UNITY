using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private Button button;
    [SerializeField] private ItemInfo itemInfo;

    public void SetupData(Item item, Action buttonAction = null)
    {
        iconImage.sprite = item.GetSprite();
        if (item.amount == 1)
        {
            counterText.text = "";
        }
        else
        {
            counterText.text = item.amount.ToString();
        }

        if (buttonAction != null)
        {
            button.onClick.AddListener(buttonAction.Invoke);
        }
        itemInfo.Setup(item.GetTextData());
    }

    private void Awake()
    {
        itemInfo.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfo.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfo.gameObject.SetActive(false);
    }
}
