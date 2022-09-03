using System.Collections.Generic;
using Models;
using UnityEngine;

[CreateAssetMenu(fileName = "Memories", menuName = "KOTEK/Memories", order = 1)]
public class MemoriesScriptableObject : ScriptableObject
{
    public List<Memory> GameMemories;
}

