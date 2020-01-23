using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.PlayerSaved = collision.GetComponent<StatsClass>();
            GameManager.instance.Difficulty++;
            SceneManager.LoadScene(2);
        }
    }
}
