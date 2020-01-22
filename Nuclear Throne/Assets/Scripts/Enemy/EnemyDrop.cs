using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject miniAmmoPref; 

    public void Drop()
    {
        float rand = Random.Range(0, 100);

        if (rand < 20)
        {
            GameObject miniAmmo = Instantiate(miniAmmoPref, transform.position, Quaternion.identity);
        }
    }
}
