using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndShowInventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public bool isVisible;

    public void hideAndShowPanel()
    {
        if (inventoryPanel.gameObject.activeSelf)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        else
        {
            inventoryPanel.gameObject.SetActive(true);
        }
    }
}

