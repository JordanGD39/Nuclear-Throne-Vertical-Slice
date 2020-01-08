using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public void Shoot(GameObject bulletPrefab, Bullet bullet, Weapon weapon, bool playerControl)
    {
        GameObject bulletObj = Instantiate(bulletPrefab, transform.GetChild(0).position, transform.parent.rotation);
        bulletObj.transform.Rotate(0, 0, Random.Range(-weapon.SpreadAngle, weapon.SpreadAngle + 1));
        BulletBehaviour bulletScript = bulletObj.GetComponent<BulletBehaviour>();
        bulletScript.WeaponThatShot = weapon;
        bulletScript.Speed = bullet.Speed;
        bulletScript.Loaded = true;
        bulletScript.PlayerControl = playerControl;
        bulletObj.GetComponent<SpriteRenderer>().enabled = true;
        bulletObj.GetComponent<SpriteRenderer>().sprite = bullet.SpriteOfBullet;
    }
}
