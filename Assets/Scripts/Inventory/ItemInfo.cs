using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemDescription;

    public void Setup(ItemInfoData itemInfoData)
    {
        itemName.text = itemInfoData.name;
        itemDescription.text = itemInfoData.description;
    }
}

[Serializable]
public class ItemInfoData
{
    public string name;
    public string description;
}
