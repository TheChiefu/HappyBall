using UnityEngine;

/// <summary>
/// This class updates the stars display on the 'End of Level' screen
/// </summary>
public class HUD_Stars : MonoBehaviour
{
    [SerializeField] private GameObject Star = null;

    public void SpawnStars(int amount)
    {
        //Spawn Hearts when amount is over 0
        if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(Star, this.transform);
            }
        }

        //Remove hearts when amount is less than zero
        if (amount < 0)
        {
            for (int i = 0; i < -amount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
