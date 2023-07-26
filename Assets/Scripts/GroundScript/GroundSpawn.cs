using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    // public GameObject[] tilePrefabs;
    // private float spawnZ = 0.0f;
    // private float tileLength = 12.0f;

    public GameObject groundTile;
    Vector3 nextSpawn;
    
    private void Start()
    {

        for(int i = 0; i < 5; i++)
        {
            spawnTile();
        }
    }

    public void spawnTile()
    {
        GameObject temp = Instantiate(groundTile, nextSpawn, Quaternion.identity);
        nextSpawn = temp.transform.GetChild(1).transform.position;
    }
}
