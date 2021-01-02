using UnityEngine;

/// <summary>
/// Used in visual part of coin to collide with player and collect coin
/// </summary>
public class CoinTrigger : MonoBehaviour
{
    //Parent property containing values
    private Coin coin;

    private void Awake()
    {
        coin = GetComponentInParent<Coin>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") coin.Collect();
    }
}
