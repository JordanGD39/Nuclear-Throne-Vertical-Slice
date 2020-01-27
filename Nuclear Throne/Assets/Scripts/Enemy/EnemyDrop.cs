using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject miniAmmoPref;
    [SerializeField] private GameObject medkitPref;

    private StatsClass playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsClass>();
    }

    public void Drop()
    {
        float rand = Random.Range(0, 100);

        if (rand < 20)
        {
            rand = Random.Range(-100, 0);

            float health = playerStats.Health;

            Debug.Log(Mathf.Abs(rand) + " health chance " + Mathf.Abs((health / 8 - 1) * 100));

            if (Mathf.Abs(rand) < Mathf.Abs((health / 8 - 1) * 100))
            {
                GameObject miniAmmo = Instantiate(medkitPref, transform.position, Quaternion.identity);
            }
            else
            {
                GameObject miniAmmo = Instantiate(miniAmmoPref, transform.position, Quaternion.identity);
            }            
        }
    }
}
