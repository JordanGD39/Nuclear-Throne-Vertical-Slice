using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public void Shoot(GameObject bulletPrefab, Bullet bullet, Weapon weapon)
    {
        GameObject bulletObj = Instantiate(bulletPrefab, transform.GetChild(0).position, transform.parent.rotation);    
        BulletBehaviour bulletScript = bulletObj.GetComponent<BulletBehaviour>();
        bulletScript.WeaponThatShot = weapon;
        bulletScript.Loaded = true;
        bulletObj.GetComponent<SpriteRenderer>().enabled = true;
        bulletObj.GetComponent<SpriteRenderer>().sprite = bullet.SpriteOfBullet;
    }
}
