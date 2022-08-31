using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private Button button;

    public void SetupData(Sprite sprite, int count, Action buttonAction = null)
    {
        iconImage.sprite = sprite;
        if (count == 1)
        {
            counterText.text = "";
        }
        else
        {
            counterText.text = count.ToString();
        }

        if (buttonAction != null)
        {
            button.onClick.AddListener(buttonAction.Invoke);
        }
    }
}
