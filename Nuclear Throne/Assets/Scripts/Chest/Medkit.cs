using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    private UiHandler ui;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<StatsClass>().Health += 2;

            if (collision.GetComponent<StatsClass>().Health > 8)
            {
                collision.GetComponent<StatsClass>().Health = 8;
            }
            ui.UpdateHealth();
            Destroy(gameObject);
        }
    }
}
