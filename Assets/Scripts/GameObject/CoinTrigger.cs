using UnityEngine;

/// <summary>
/// Used in visual mesh of coin to collide with player and collect coin
/// </summary>
public class CoinTrigger : MonoBehaviour
{
    //Parent property containing values
    private Coin coin;

    private void Awake()
    {
        if(coin == null) coin = GetComponentInParent<Coin>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            coin.Collect(player);
        }
    }
}
