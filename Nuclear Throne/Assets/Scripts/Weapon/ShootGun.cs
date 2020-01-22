﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletShotPref;
    private CameraShake camShake;

    //private bool laser = false;

    //private Weapon weaponSave;

    private void Start()
    {
        camShake = Camera.main.GetComponent<CameraShake>();
    }

    //private void Update()
    //{
    //    if (laser)
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);

    //        if (hit.collider != null)
    //        {
    //            if (hit.collider.CompareTag("Enemy"))
    //            {
    //                Debug.Log("Gamer");
    //            }
    //        }
    //    }        
    //}

    public void Shoot(GameObject bulletPrefab, Bullet bullet, Weapon weapon, bool playerControl)
    {
        //if (bullet.fireType == Bullet.type.LASER)
        //{
        //    laser = true;
        //    weaponSave = weapon;
        //    return;
        //}
        GameObject bulletObj = Instantiate(bulletPrefab, transform.GetChild(0).position, transform.parent.rotation);
        bulletObj.transform.Rotate(0, 0, Random.Range(-weapon.SpreadAngle, weapon.SpreadAngle + 1));

        if (weapon.Melee)
        {
            bulletObj.transform.parent = transform.parent.parent.GetChild(0);
        }

        BulletBehaviour bulletScript = bulletObj.GetComponent<BulletBehaviour>();
        bulletScript.WeaponThatShot = weapon;
        bulletScript.Speed = bullet.Speed;
        bulletScript.BulletFired = bullet;
        bulletScript.Loaded = true;
        bulletScript.PlayerControl = playerControl;
        bulletObj.GetComponent<SpriteRenderer>().enabled = true;
        bulletObj.GetComponent<SpriteRenderer>().sprite = bullet.SpriteOfBullet;

        if (!weapon.Melee && !bullet.Explode && playerControl)
        {
            StartCoroutine(camShake.Shake(0.1f, 0.1f));
        }

        if (playerControl && (bullet.fireType == Bullet.type.NORMAL || bullet.fireType == Bullet.type.SHELL))
        {
            GameObject groundBullet = Instantiate(bulletShotPref, transform.parent.parent.GetChild(0).position, transform.rotation);
            groundBullet.transform.Rotate(0, 0, Random.Range(-360, 361));
            groundBullet.transform.GetChild(0).rotation = transform.rotation;
            groundBullet.transform.GetChild(0).Rotate(0, 0, Random.Range(-70, 71));
            groundBullet.GetComponent<Rigidbody2D>().AddForce(-groundBullet.transform.GetChild(0).up * 800);
            Destroy(groundBullet, 6);
        }
    }
}
