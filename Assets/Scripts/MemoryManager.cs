using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogueEditor;
using Models;
using Newtonsoft.Json;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance { get; private set; }
    public List<Memory> CachedMemories;
    public int Health;
    public int KittenSouls;
    [SerializeField] private MemoriesScriptableObject memories;

    private void Start()
    {
        Instance = this;
        InitSavedMemories();
        ConversationManager.OnConversationStarted += OnConversationStarted;
        LoadPlayer();
    }

    private void InitSavedMemories()
    {
        foreach (var memory in memories.GameMemories)
        {
            var memoryObject = new Memory()
                {
                    name = memory.name,
                    state = LoadStateByName(memory.name)
                };
            CachedMemories.Add(memoryObject);
        }
    }

    public void LoadPlayer()
    {
        Health = PlayerPrefs.GetInt("Health");
        KittenSouls = PlayerPrefs.GetInt("KittenSouls");
        if (Health != 0)
        {
            PlayerManager.Instance.playerLife.SetHealthPoints(Health);
        }

        if (KittenSouls != 0)
        {
            PlayerManager.Instance.itemCollector.SetKittenSouls(KittenSouls);
        }
        LoadInventory();
    }

    public void SavePlayer()
    {
        PlayerPrefs.SetInt("Health", PlayerManager.Instance.playerLife.kotekHealthPoints);
        PlayerPrefs.SetInt("KittenSouls", PlayerManager.Instance.itemCollector.kittensouls);
        SaveInventory();
        PlayerPrefs.Save();
    }


    private void SaveInventory()
    {
        List<Item> itemList = PlayerManager.Instance.playerMovement.uiInventory.uiInventory.GetItemList();
        var json = JsonConvert.SerializeObject(itemList);
        PlayerPrefs.SetString("Inventory", json);
        Debug.Log(json);
    }

    public void LoadInventory()
    {
        var inventoryJson = PlayerPrefs.GetString("Inventory", String.Empty);
        if (inventoryJson != String.Empty)
        {
            List<Item> jsonList = JsonConvert.DeserializeObject<List<Item>>(inventoryJson);
            foreach (var jsonObject in jsonList)
            {
                Debug.Log($"{jsonObject.itemType} and {jsonObject.amount}");
                PlayerManager.Instance.playerMovement.uiInventory.uiInventory.AddItem(jsonObject);
            }
            PlayerManager.Instance.playerMovement.uiInventory.RefreshInventoryItems();
        }
    }

    private void SetToConversationCachedMemories()
    {
        foreach (var memory in CachedMemories)
        {
            ConversationManager.Instance.SetBool(memory.name, memory.state);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void OnConversationStarted()
    {
        SetToConversationCachedMemories();
    }

    private Memory GetMemoryByName(string name)
    {
        return CachedMemories.FirstOrDefault(x => x.name == name);
    }

    public void SetMemory(string name)
    {
        var successInteraction = 1;
        var memory = GetMemoryByName(name);
        if (memory != null)
        {
            PlayerPrefs.SetInt(name, successInteraction);
            PlayerPrefs.Save();
            memory.state = true;
        }
    }

    private bool LoadStateByName(string name)
    {
        var state = PlayerPrefs.GetInt(name);
        if (state == 1)
        {
           return  true;
        }
        else
        {
            return false;
        }
    }
}
