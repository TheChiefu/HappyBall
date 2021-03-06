using UnityEngine;

/// <summary>
/// This class updates the hearts display on the player HUD
/// </summary>
public class HUD_Hearts : MonoBehaviour
{
    public static HUD_Hearts instance;
    [SerializeField] private GameObject heart;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        //Spawn necessary amount of hearts
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        UpdateHearts(player.GetHealth());
    }

    /// <summary>
    /// Update the heart HUD elements by adding or removing hearts based of update amount
    /// </summary>
    /// <param name="amount"></param>
    public void UpdateHearts(int amount)
    {
        //Spawn Hearts when amount is over 0
        if(amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(heart, this.transform);
            }
        }

        //Remove hearts when amount is less than zero
        if (amount < 0)
        {
            for (int i = 0; i < -amount; i++)
            {
                if(transform.childCount > 0)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }  
            }
        }
    }
}
