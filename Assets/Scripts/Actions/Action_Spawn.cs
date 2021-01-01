using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic action class that spawns an item or items at a given position
/// </summary>
public class Action_Spawn : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    [Header("For Instaniated Items:")]
    [SerializeField] private bool isInstantiated = false;
    [SerializeField] private Transform[] spawnPoints;


    public void Spawn()
    {
        Debug.Log("Spawned");

        if(isInstantiated)
        {
            for(int i = 0; i < items.Length; i++)
            {
                Instantiate(items[i], spawnPoints[i]);
            }
        }
        else
        {
            for(int i = 0; i < items.Length; i++)
            {
                if(items[i] != null) items[i].SetActive(true);
            }
        }
    }
}
