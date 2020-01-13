﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<Weapon> weaponDrops = new List<Weapon>();
    public List<Weapon> WeaponDrops { get { return weaponDrops; } set { weaponDrops = value; } }

    [SerializeField] private int weaponsNumber = 1;
    [SerializeField] private float shootingForce = 1;

    [SerializeField] private GameObject pickUpPref;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetButtonDown("Pickup"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            for (int i = 0; i < weaponsNumber; i++)
            {
                GameObject weaponGameObject = Instantiate(pickUpPref, transform.position, transform.rotation);
                PickUp weapon = weaponGameObject.GetComponent<PickUp>();

                weapon.WeaponOfGameObject = weaponDrops[Random.Range(0, weaponDrops.Count)];

                if (i > 1)
                {
                    Vector2 dir = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));

                    if (dir.magnitude == 0)
                    {
                        dir.x = 1;
                    }

                    weaponGameObject.GetComponent<Rigidbody2D>().AddForce(dir * shootingForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
