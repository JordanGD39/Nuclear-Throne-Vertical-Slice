using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFunctions : MonoBehaviour
{
    private bool shooting;
    private bool playerControl;
    private float timer;

    private StatsClass holder;
    private ShootGun gun;
    private MeleeAttack meleeAttack;

    [SerializeField] private GameObject bulletPref;

    void Start()
    {
        holder = transform.parent.parent.GetChild(0).GetComponent<StatsClass>();

        if (holder != null)
        {
            playerControl = true;
        }
        else
        {
            holder = transform.parent.parent.GetComponent<StatsClass>();
            playerControl = false;
        }
        gun = GetComponent<ShootGun>();
        meleeAttack = GetComponent<MeleeAttack>();

        GetComponent<SpriteRenderer>().sprite = holder.Primary.SpriteOfWeapon;
        transform.GetChild(0).localPosition = new Vector3(0, holder.Primary.ShootCoords, 0);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (playerControl)
        {
            InputButtons();
        }
        else
        {
            AiShooting();
        }
    }

    private void InputButtons()
    {
        //Left Mouse-button Click: Fire        

        if (!holder.Primary.Melee)
        {
            if (holder.Primary.weaponType == Weapon.type.AUTO || holder.Primary.weaponType == Weapon.type.AUTOBURST)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (timer >= holder.Primary.ReloadTime && !shooting)
                    {
                        StartCoroutine(Shoot());
                    }                    
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (timer >= holder.Primary.ReloadTime && !shooting)
                    {
                        StartCoroutine(Shoot());
                    }
                }
            }
        }
        else
        {
            Debug.Log("Melee");
        }

        //E-key Press: Weapon Switch
        if (Input.GetButtonDown("Switch"))
        {
            Weapon weaponHolder = holder.Primary;
            holder.Primary = holder.Secondary;
            holder.Secondary = weaponHolder;
            GetComponent<SpriteRenderer>().sprite = holder.Primary.SpriteOfWeapon;
            transform.GetChild(0).localPosition = new Vector3(0, holder.Primary.ShootCoords, 0);
        }
    }

    private void AiShooting()
    {
        int tileLayer = ~(LayerMask.GetMask("Weapon") | LayerMask.GetMask("WallCheck"));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 20, tileLayer);

        Debug.DrawRay(transform.position, transform.up, Color.red);

        Debug.Log(hit.collider);

        if (timer >= holder.Primary.ReloadTime && !shooting)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        shooting = true;

        if (holder.Primary.ShootBullets > 1)
        {
            for (int i = 0; i < holder.Primary.ShootBullets; i++)
            {
                gun.Shoot(bulletPref, holder.Primary.WeaponBullet, holder.Primary);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            gun.Shoot(bulletPref, holder.Primary.WeaponBullet, holder.Primary);
        }

        shooting = false;
        timer = 0;
    }
}
