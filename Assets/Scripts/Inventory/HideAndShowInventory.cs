using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndShowInventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public bool isVisible;

    public void hideInventoryPanel()
    {
        inventoryPanel.gameObject.SetActive(false);
        isVisible = false;
    }
    
    public void showInventoryPanel()
    {
        inventoryPanel.gameObject.SetActive(true);
        isVisible = true;
    }
}

