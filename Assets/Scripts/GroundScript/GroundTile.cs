using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawn groundSpawn;

    private void Start()
    {
        groundSpawn = GameObject.FindObjectOfType<GroundSpawn>();
        SpawnCoin();
    }
    private void Update()
    {
        
    }
    private void OnTriggerExit(Collider other) 
    {
        groundSpawn.spawnTile();
        Destroy(gameObject, 1);
    }

    public GameObject coinPrefab;
    void SpawnCoin()
    {
        int coinToSpawn = 5;
        for(int i = 0; i < coinToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = RandomPointCollider(GetComponent<Collider>());
        }
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
}
