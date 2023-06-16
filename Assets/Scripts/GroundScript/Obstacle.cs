using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerController pc;
    private void Start() 
    {
        pc = GameObject.FindObjectOfType<PlayerController>();
    }
    // private void OnCollisionEnter(Collision collision) 
    // {
    //     if (collision.gameObject.name == "Player")
    //         pc.Die();
    // }
}
