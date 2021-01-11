using UnityEngine;

public class CoinDropper : MonoBehaviour
{
    [Header("General:")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int coinCount;

    [Header("Customiziation:")]
    [SerializeField] private int timeLength = 1; //How long coins will spawn
    [SerializeField] private float spawnDelay = 0; //Time between coin spawns

    private GameObject[] coins;
    private int spawnIndex = 0;
    private float timer = 0;
    private bool isSpawning = true;
    private BoxCollider spawnArea;

    /// <summary>
    /// Spawn coin within area of CoinDropper
    /// </summary>
    private void SpawnCoin()
    {
        if (spawnIndex < coins.Length)
        {
            coins[spawnIndex].SetActive(true);
            spawnIndex++;
        }
        else Debug.LogError(this.name + ": Spawn index is above coin array limit.");
    }

    /// <summary>
    /// Spawn all coins on startup to ease memory during runtime
    /// </summary>
    private void ObjectPool()
    {
        coins = new GameObject[coinCount];

        for (int i = 0; i < coinCount; i++)
        {
            //Random spawn position
            Vector3 random = new Vector3(
                Random.Range(transform.position.x - (spawnArea.size.x / 2), transform.position.x + (spawnArea.size.x / 2)),
                Random.Range(transform.position.y - (spawnArea.size.y / 2), transform.position.y + (spawnArea.size.y / 2)),
                Random.Range(transform.position.z - (spawnArea.size.z / 2), transform.position.z + (spawnArea.size.z / 2))
                );

            coins[i] = Instantiate(coinPrefab, random, Quaternion.identity, transform);
            coins[i].SetActive(false);
        }
    }

    private void Awake()
    {
        spawnArea = GetComponent<BoxCollider>();
        ObjectPool();
    }

    private void DelaySpawn()
    {
        //Check if all items are spawned or timer is up
        if (spawnIndex < coins.Length || timer > timeLength)
        {
            timer += Time.deltaTime;

            //Check for division 0
            if (spawnDelay == 0)
            {
                SpawnCoin();
            }

            //Normal spawning delay
            else
            {
                if (timer > spawnDelay)
                {
                    SpawnCoin();
                    timer = 0;
                }
            }
        }
        else
        {
            //Stop spawning
            isSpawning = false;
        }
    }

    private void FixedUpdate()
    {
        if(isSpawning) DelaySpawn();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        spawnArea = GetComponent<BoxCollider>();
        Gizmos.DrawCube(transform.position, spawnArea.size);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        spawnArea = GetComponent<BoxCollider>();
        Gizmos.DrawCube(transform.position, spawnArea.size);
    }
}
