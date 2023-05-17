using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 0.1f;

    private void Start() 
    {
        Invoke("DestroyBullet", life);
    }

    private void OnCollisionEnter(Collision hit) 
    {
        Destroy(hit.gameObject);
        Destroy(gameObject);
    }
    
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
