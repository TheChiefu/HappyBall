using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private int CheckpointIndex = 0;

    //Set checkpoint value on level manger to this checkpoint's position
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelManager.instance.SetCheckpoint(CheckpointIndex);
        }
    }
}
