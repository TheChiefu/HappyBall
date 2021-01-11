using UnityEngine;

/// <summary>
/// Sends any object with a non-trigger 3D collider back to a spawn point
/// </summary>
public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Reset velocity if it has it (it should have)
        Rigidbody rg = other.gameObject.GetComponent<Rigidbody>();
        Transform ts = other.gameObject.transform;
        if (rg != null) rg.velocity = Vector3.zero;

        //If object is a tagged physics object set back to it's original spawn
        if(other.gameObject.tag == "Physics")
        {
            PhysicsObject PO = other.gameObject.GetComponent<PhysicsObject>();
            ts.position = PO.respawnPosition;
            ts.rotation = PO.respawnRotation;
        }

        //If object is the player set their position at spawn/checkpoint
        if(other.gameObject.tag == "Player")
        {
            PlayerController player = other.gameObject.GetComponentInParent<PlayerController>();
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (player != null) player.Damage(1);

            LevelManager LM = LevelManager.instance;

            //Respawn at checkpoint spawn
            if(LM.GetTotalRespawnPoints() > 0)
            {
                Transform respawn = LM.GetRespawnPointByIndex(LM.GetCheckpoint());
                if (rb != null) rb.velocity.Set(0, 0, 0);
                ts.position = respawn.position;
                ts.rotation = respawn.rotation;
            }
            
            //If no respawn point given spawn a point (0,0,0)
            else
            {
                Debug.LogError("No respawn points given!");
                ts.position = Vector3.zero;
                ts.rotation = Quaternion.identity;
            }
        }
    }
}
