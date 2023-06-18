using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    PlayerController pc;
    private void Start() 
    {
        pc = GameObject.FindObjectOfType<PlayerController>();
    }
    void OnCollisionEnter(Collision player) {
        if (player.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
