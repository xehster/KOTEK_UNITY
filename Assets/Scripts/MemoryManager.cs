using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogueEditor;
using Models;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance { get; private set; }
    public List<Memory> CachedMemories;
    public List<NPCConversation> NPCConversations;
    [SerializeField] private MemoriesScriptableObject memories;

    private void Awake()
    {
        Instance = this;

        StartCoroutine(InitWithTime());
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
            ConversationManager.Instance.SetBool(memoryObject.name, memoryObject.state);
            CachedMemories.Add(memoryObject);
        }
    }

    private IEnumerator InitWithTime()
    {
        yield return new WaitForSeconds(.1f);
        InitSavedMemories();
        ConversationManager.OnConversationStarted += OnConversationStarted;
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
        InitSavedMemories();
    }

    private void GetAllConverstaions()
    {
        NPCConversation[] allConversations = FindObjectsOfType<NPCConversation>() ;
        foreach (NPCConversation npcConversation in allConversations)
        {
            NPCConversations.Add(npcConversation);
        }
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
        var state = PlayerPrefs.GetInt(name.ToString());
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
