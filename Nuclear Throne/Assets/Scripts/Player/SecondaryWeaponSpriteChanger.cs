﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeaponSpriteChanger : MonoBehaviour
{
    private Sprite secondaryWeapon;
    private SpriteRenderer rendr;
    private void Start()
    {
        secondaryWeapon = GetComponentInParent<StatsClass>().Secondary.SpriteOfWeapon;
        rendr = GetComponent<SpriteRenderer>();

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 10.0f);
        rendr.sprite = secondaryWeapon;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            secondaryWeapon = GetComponentInParent<StatsClass>().Primary.SpriteOfWeapon;
            rendr.sprite = secondaryWeapon;
        }
    }
}
