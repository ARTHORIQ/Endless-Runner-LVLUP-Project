using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerController pc;
    private void Start()
    {
        pc = GameObject.FindObjectOfType<PlayerController>();
    }
}
