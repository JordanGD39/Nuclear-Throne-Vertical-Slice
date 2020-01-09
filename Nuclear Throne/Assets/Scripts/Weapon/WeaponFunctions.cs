using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFunctions : MonoBehaviour
{
    private bool shooting;
    private bool playerControl;
    private float timer;

    private StatsClass holder;
    private EnemyAi ai;
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
            ai = transform.parent.parent.GetComponent<EnemyAi>();

            if (holder.GetComponent<EnemyAi>().BadAimer)
            {
                holder.transform.GetChild(2).transform.rotation = holder.transform.GetChild(3).transform.rotation;
            }
        }
        gun = GetComponent<ShootGun>();
        meleeAttack = GetComponent<MeleeAttack>();
        if (holder.Primary != null)
        {
            GetComponent<SpriteRenderer>().sprite = holder.Primary.SpriteOfWeapon;
            transform.GetChild(0).localPosition = new Vector3(0, holder.Primary.ShootCoords, 0);
        }
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
            //Debug.Log("Melee");
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
        if (!ai.BadAimer)
        {
            int tileLayer = ~(LayerMask.GetMask("Weapon") | LayerMask.GetMask("WallCheck") | LayerMask.GetMask("Enemy") | LayerMask.GetMask("EnemyWallCol"));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, ai.Range, tileLayer);

            if (hit.collider != null)
            {
                //if (transform.parent.parent.name == "Giant Maggot")
                //{
                //    Debug.Log(hit.collider);
                //}

                if (hit.collider.CompareTag("Player"))
                {
                    ai.PlayerInSight = true;
                    if (holder.Primary != null && timer >= holder.Primary.ReloadTime && !shooting)
                    {
                        StartCoroutine(Shoot());
                    }
                }
                else
                {
                    ai.PlayerInSight = false;
                }
            }
            else
            {
                ai.PlayerInSight = false;
            }
        }
        else
        {
            if (ai.PlayerInSight)
            {
                if (holder.Primary != null && timer >= holder.Primary.ReloadTime && !shooting)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
    }

    private IEnumerator Shoot()
    {
        shooting = true;

        if (holder.Primary.ShootBullets > 1)
        {
            for (int i = 0; i < holder.Primary.ShootBullets; i++)
            {
                gun.Shoot(bulletPref, holder.Primary.WeaponBullet, holder.Primary, playerControl);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            gun.Shoot(bulletPref, holder.Primary.WeaponBullet, holder.Primary, playerControl);
        }

        if (!playerControl && holder.GetComponent<EnemyAi>().BadAimer)
        {
            holder.transform.GetChild(2).transform.rotation = holder.transform.GetChild(3).transform.rotation;
            holder.GetComponent<EnemyAi>().ChangeDirection();
        }

        shooting = false;
        timer = 0;
    }
}
