using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthCheck : MonoBehaviour
{
    [SerializeField] private List<GameObject> hpImageList;

    private void Start()
    {
        RestoreHealth();
    }
    
    public void SetCurrentHealth(int health)
    {
        for (int i = 0; i < hpImageList.Count; i++)
        {
            hpImageList[i].SetActive(i < health);
        }
    }
    private void RestoreHealth()
    {
        foreach (var health in hpImageList)
        {
            health.SetActive(true);
        }
    }
}
