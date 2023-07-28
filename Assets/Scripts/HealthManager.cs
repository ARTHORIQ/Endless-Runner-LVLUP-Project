using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public GameObject[] hearts;

    [SerializeField] private int playerHealth = 3;

    private void Start()
    {
        UpdateHeartDisplay();
    }

    public void ReduceHealth()
    {
        playerHealth--;
        UpdateHeartDisplay();
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            SceneManager.LoadScene("Menu");
        }
        //UpdateHeartDisplay();
    }


    private void UpdateHeartDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < playerHealth);
        }
    }
}
