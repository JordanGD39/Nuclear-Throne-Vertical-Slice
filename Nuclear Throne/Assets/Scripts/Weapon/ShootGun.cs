using System.Collections;
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

    public void Shoot(GameObject bulletPrefab, Bullet bullet, Weapon weapon, bool playerControl)
    {
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

            if (playerControl)
            {
                AudioManager.instance.Play("Shoot");
            }
            else
            {
                AudioManager.instance.Play("EnemyShoot");
            }
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
