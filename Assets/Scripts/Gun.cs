// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Gun : MonoBehaviour
// {   
//     public PlayerController pC;
//     public Transform bulletSpawn;
//     public GameObject bulletPrefab;
//     public float bulletSpeed = 10;
    
//     private void Start() 
//     {
//         pC = GameObject.FindObjectOfType<PlayerController>();
//     }
//     private void Update() 
//     {
//         if (SwipeManager.tap)
//         {
//             var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
//             bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
//         }
//     }
// }
