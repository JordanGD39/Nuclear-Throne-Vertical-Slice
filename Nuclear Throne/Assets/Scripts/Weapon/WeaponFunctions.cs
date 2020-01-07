using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFunctions : MonoBehaviour
{
    private bool shooting;
    private float timer;

    private PlayerClass player;
    private ShootGun gun;
    private MeleeAttack meleeAttack;

    [SerializeField] private GameObject bulletPref;

    void Start()
    {
        player = transform.parent.parent.GetChild(0).GetComponent<PlayerClass>();
        gun = GetComponent<ShootGun>();
        meleeAttack = GetComponent<MeleeAttack>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        InputButtons();
    }

    private void InputButtons()
    {
        //Left Mouse-button Click: Fire        

        if (!player.Primary.Melee)
        {
            if (player.Primary.weaponType == Weapon.type.AUTO || player.Primary.weaponType == Weapon.type.AUTOBURST)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (timer >= player.Primary.ReloadTime && !shooting)
                    {
                        StartCoroutine(Shoot());
                    }                    
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (timer >= player.Primary.ReloadTime && !shooting)
                    {
                        StartCoroutine(Shoot());
                    }
                }
            }
        }
        else
        {
            //Debug.Log("Melee");
        }

        //E-key Press: Weapon Switch
        if (Input.GetButtonDown("Switch"))
        {
            Weapon holder = player.Primary;
            player.Primary = player.Secondary;
            player.Secondary = holder;
            GetComponent<SpriteRenderer>().sprite = player.Primary.SpriteOfWeapon;
            transform.GetChild(0).localPosition = new Vector3(0, player.Primary.ShootCoords, 0);
        }
    }

    private IEnumerator Shoot()
    {
        shooting = true;

        if (player.Primary.ShootBullets > 1)
        {
            for (int i = 0; i < player.Primary.ShootBullets; i++)
            {
                gun.Shoot(bulletPref, player.Primary.WeaponBullet, player.Primary);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            gun.Shoot(bulletPref, player.Primary.WeaponBullet, player.Primary);
        }

        shooting = false;
        timer = 0;
    }
}
