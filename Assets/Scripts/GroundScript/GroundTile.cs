using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawn groundSpawn;
    public float xOffset = 3f;
    public Transform[] allitems;

    public GameObject coinPrefab;
    public int maxSpawnedObjects = 10;
    public int maxObs = 2;
    public int minCoinToSpawn = 3;
    public int maxCoinToSpawn = 6;
    public float coinStackOffset = 1f;
    public float coinHeightOffset = 0.1f;

    private float zDistance = 1f;
    private float zOffset = 1f;
    private int spawnedObs;

    private void Start()
    {
        groundSpawn = GameObject.FindObjectOfType<GroundSpawn>();
        // SpawnCoin();
        // SpawnObs();
        SpawnObjects();
    }
    private void Update()
    {

    }
    private void OnTriggerExit(Collider other)
    {
        groundSpawn.spawnTile();
        Destroy(gameObject, 1);
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < maxSpawnedObjects; i++)
        {
            int random = Random.Range(0, maxSpawnedObjects);

            int whichItem = Random.Range(0, allitems.Length);
            float xPos = whichItem <= 1 || spawnedObs >= maxObs ? Random.Range(-1, 2) * xOffset : 0f;
            Vector3 pos = new Vector3(xPos, 0.25f, i * zDistance);
            pos.z += zOffset;

            if (random <= maxObs || spawnedObs >= maxObs)
            {
                // spawn coins
                GameObject coin = Instantiate(coinPrefab, Vector3.zero, Quaternion.identity);
                coin.transform.parent = transform;
                coin.transform.localPosition = pos;
            }
            else
            {
                // spawn obstacles
                spawnedObs++;
                Transform obs = Instantiate(allitems[whichItem], Vector3.zero, allitems[whichItem].rotation);
                obs.parent = transform;
                obs.localPosition = pos;
            }
        }
    }

    // public GameObject coinPrefab;
    // void SpawnCoin()
    // {
    //     for(int i = 0; i < coinToSpawn; i++)
    //     {
    //         GameObject temp = Instantiate(coinPrefab, transform);
    //         temp.transform.position = RandomPointCollider(GetComponent<Collider>());
    //     }
    // }

    void SpawnObs()
    {
        int whichItem = Random.Range(0, allitems.Length);
        //nilai terbesar random range itu max-exclusive artinya tidak ikut. ex.(kalau 2 berarti random terbesar 1)
        //random integer = random.range(minInclusive, maxExclusive)
        //random float = random.range(minInclusive, maxInclusive)
        float xPos = whichItem <= 1 ? Random.Range(-1, 2) * xOffset : 0f;
        Transform obs = Instantiate(allitems[whichItem], Vector3.zero, allitems[whichItem].rotation);
        obs.parent = transform;
        obs.localPosition = new Vector3(xPos, 0, 5f);

        float laneOffset = xOffset;
        float[] lanePositions = { -laneOffset, 0f, laneOffset };

        // Spawn coins
        // int coinToSpawn = Random.Range(minCoinToSpawn, maxCoinToSpawn + 1);
        // Vector3 spawnPosition = obs.position + Vector3.up * coinHeightOffset;
        // float stackOffset = coinStackOffset / coinToSpawn; // Calculate the horizontal offset between coins
        // for (int i = 0; i < coinToSpawn; i++)
        // {
        //     int laneIndex = Random.Range(0, lanePositions.Length);
        //     Vector3 coinSpawnPosition = spawnPosition + new Vector3(lanePositions[laneIndex], 0f, 0f);
        //     GameObject coin = Instantiate(coinPrefab, coinSpawnPosition, Quaternion.identity);
        //     coin.transform.parent = transform;
        //     spawnPosition.y += stackOffset;
        // }
    }
    Vector3 RandomPointCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        if (point != collider.ClosestPoint(point))
        {
            point = RandomPointCollider(collider);
        }
        point.y = 1;
        return point;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

    }
}
