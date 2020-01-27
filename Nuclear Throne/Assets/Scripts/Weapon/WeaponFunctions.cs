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
    private UiHandler ui;

    [SerializeField] private GameObject bulletPref;

    void Start()
    {
        holder = transform.parent.parent.GetChild(0).GetComponent<StatsClass>();

        if (holder != null)
        {
            ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiHandler>();
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
        if (holder.Primary.weaponType == Weapon.type.AUTO)
        {
            if (Input.GetButton("Fire1"))
            {
                float reload = holder.Primary.ReloadTime - (holder.Level - 1) / 10;

                if (reload < 0.1f)
                {
                    reload = 0.1f;
                }
                

                if (timer >= reload && !shooting)
                {
                    StartCoroutine(Shoot());
                }                    
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                float reload = holder.Primary.ReloadTime - (holder.Level - 1) / 10;

                if (reload < 0.1f)
                {
                    reload = 0.1f;
                }

                Debug.Log(reload);

                if (timer >= reload && !shooting)
                {
                    StartCoroutine(Shoot());
                }
            }
        }

        //E-key Press: Weapon Switch
        if (Input.GetButtonDown("Switch"))
        {
            if (holder.Secondary != null)
            {
                Weapon weaponHolder = holder.Primary;
                holder.Primary = holder.Secondary;
                holder.Secondary = weaponHolder;
                GetComponent<SpriteRenderer>().sprite = holder.Primary.SpriteOfWeapon;
                transform.GetChild(0).localPosition = new Vector3(0, holder.Primary.ShootCoords, 0);
                ui.UpdateWeapon();
                ui.UpdateAmmo();
            }
        }
    }

    private void AiShooting()
    {
        if (!ai.BadAimer)
        {
            int tileLayer = ~(LayerMask.GetMask("Weapon") | LayerMask.GetMask("WallCheck") | LayerMask.GetMask("Enemy") | LayerMask.GetMask("EnemyWallCol")| LayerMask.GetMask("PlayerPointEffect"));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, ai.Range, tileLayer);

            //Debug.DrawRay(transform.position, transform.up, Color.red);

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
                        holder.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
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
                if (holder.Primary != null && timer >= holder.Primary.ReloadTime + Random.Range(0,1) && !shooting)
                {
                    StartCoroutine(Shoot());
                }
            }
        }
    }

    private IEnumerator Shoot()
    {
        shooting = true;

        bool norm = false;
        bool shell = false;
        bool bolt = false;
        bool explosive = false;
        bool energy = false;

        bool canShoot = true;

        switch (holder.Primary.WeaponBullet.fireType)
        {
            case Bullet.type.NORMAL:
                norm = true;
                break;
            case Bullet.type.FIRE:
                norm = true;
                break;
            case Bullet.type.LASER:
                energy = true;
                break;
            case Bullet.type.PLASMA:
                energy = true;
                break;
            case Bullet.type.LIGHTNING:
                energy = true;
                break;
            case Bullet.type.EXPLOSION:
                explosive = true;
                break;
            case Bullet.type.MISSILE:
                explosive = true;
                break;
            case Bullet.type.SEEKER:
                explosive = true;
                break;
            case Bullet.type.DISC:
                norm = true;
                break;
            case Bullet.type.SLUG:
                shell = true;
                break;
            case Bullet.type.SHELL:
                shell = true;
                break;
            case Bullet.type.BOLT:
                bolt = true;
                break;
        }

        if (norm)
        {
            if (holder.Ammo >= holder.Primary.AmmoWaste)
            {
                holder.Ammo -= holder.Primary.AmmoWaste;
            }
            else
            {
                canShoot = false;
            }
        }
        else if (shell)
        {
            if (holder.ShellAmmo >= holder.Primary.AmmoWaste)
            {
                holder.ShellAmmo -= holder.Primary.AmmoWaste;
            }
            else
            {
                canShoot = false;
            }
        }
        else if (bolt)
        {
            if (holder.BoltAmmo >= holder.Primary.AmmoWaste)
            {
                holder.BoltAmmo -= holder.Primary.AmmoWaste;
            }
            else
            {
                canShoot = false;
            }
        }
        else if (explosive)
        {
            if (holder.ExplosiveAmmo >= holder.Primary.AmmoWaste)
            {
                holder.ExplosiveAmmo -= holder.Primary.AmmoWaste;
            }
            else
            {
                canShoot = false;
            }
        }
        else if (energy)
        {
            if (holder.EnergyAmmo >= holder.Primary.AmmoWaste)
            {
                holder.EnergyAmmo -= holder.Primary.AmmoWaste;
            }
            else
            {
                canShoot = false;
            }
        }

        if (playerControl)
        {
            ui.UpdateAmmo();            
        }

        if (canShoot)
        {
            if (holder.Primary.ShootBullets > 1)
            {
                for (int i = 0; i < holder.Primary.ShootBullets; i++)
                {
                    gun.Shoot(bulletPref, holder.Primary.WeaponBullet, holder.Primary, playerControl, i);

                    if (holder.Primary.FixedAngle == 0)
                    {
                        yield return new WaitForSeconds(0.05f);
                    }                    
                }
            }
            else
            {
                gun.Shoot(bulletPref, holder.Primary.WeaponBullet, holder.Primary, playerControl, 0);
            }

            if (!playerControl && holder.GetComponent<EnemyAi>().BadAimer)
            {
                holder.transform.GetChild(2).transform.rotation = holder.transform.GetChild(3).transform.rotation;
                holder.GetComponent<EnemyAi>().ChangeDirection();
            }            
            timer = 0;
        }

        shooting = false;
    }
}
