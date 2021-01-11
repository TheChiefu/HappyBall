using UnityEngine;

/// <summary>
/// Generic action class that spawns toggles disabled items to enabled
/// </summary>
public class ActionSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] items;

    /// <summary>
    /// Set state of items
    /// </summary>
    /// <param name="state"></param>
    private void SetState(bool state)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null) items[i].SetActive(state);
        }
    }

    private void Awake()
    {
        SetState(false);
    }

    public void Spawn()
    {
        SetState(true);
    }
}
