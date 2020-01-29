using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    private UiHandler ui;

    [SerializeField] private int health = 2;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<StatsClass>().Health += health;

            if (collision.GetComponent<StatsClass>().Health > collision.GetComponent<StatsClass>().MaxHealth)
            {
                collision.GetComponent<StatsClass>().Health = collision.GetComponent<StatsClass>().MaxHealth;
            }
            ui.UpdateHealth();
            if (GetComponent<Animator>() != null)
            {
                GetComponent<Animator>().Play("MedkitOpen");                
            }
            else
            {
                Destroy(gameObject);
            }

            GameManager.instance.TextSpawn("+" + health + " health!", transform);
        }
    }
}
