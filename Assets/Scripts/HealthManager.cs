using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
     public GameObject[] hearts;

    private int playerHealth = 3;

    private void Start()
    {
        UpdateHeartDisplay();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ReduceHealth();
        }
    }

    private void ReduceHealth()
    {
        playerHealth--;
        if (playerHealth < 0)
        {
            playerHealth = 0;
            return;
        }
        UpdateHeartDisplay();
    }


    private void UpdateHeartDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < playerHealth);
        }
    }
}
