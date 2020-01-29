using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private List<Weapon> weaponDrops = new List<Weapon>();
    public List<Weapon> WeaponDrops { get { return weaponDrops; } set { weaponDrops = value; } }

    [SerializeField] private int weaponsNumber = 1;
    [SerializeField] private bool weaponsChest = false;
    [SerializeField] private int multiplier = 1;

    [SerializeField] private GameObject pickUpPref;

    private UiHandler ui;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            if (GetComponent<Animator>() != null)
            {
                GetComponent<Animator>().SetTrigger("Opened");
            }
            else
            {
                Destroy(gameObject);
            }

            if (weaponsChest)
            {                
                for (int i = 0; i < weaponsNumber; i++)
                {
                    GameObject weaponGameObject = Instantiate(pickUpPref, transform.position, transform.rotation);
                    PickUp weapon = weaponGameObject.GetComponent<PickUp>();

                    weapon.WeaponOfGameObject = weaponDrops[Random.Range(0, weaponDrops.Count)];

                    //weapon ammo per weapon

                    if (i > 0)
                    {
                        Vector2 dir = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));

                        if (dir.x == 0 && dir.y == 0)
                        {
                            dir.x = 1;
                        }

                        Debug.Log(dir + " Weapon: " + weapon.WeaponOfGameObject.Name);

                        dir.Normalize();

                        weaponGameObject.GetComponent<Rigidbody2D>().AddForce(dir * 10, ForceMode2D.Impulse);                        
                    }

                    GiveAmmo(collision, weapon.WeaponOfGameObject);
                }
            }
            else
            {
                StatsClass player = collision.GetComponent<StatsClass>();

                Weapon weapon = null;

                int rand = Random.Range(0, 2);

                if (multiplier == 1)
                {
                    if (rand == 0)
                    {
                        weapon = player.Primary;
                    }
                    else
                    {
                        if (player.Secondary != null)
                        {
                            weapon = player.Secondary;
                        }
                        else
                        {
                            weapon = player.Primary;
                        }
                    }
                }
                else
                {
                    weapon = player.Primary;

                    if (weapon.Melee && player.Secondary != null)
                    {
                        weapon = player.Secondary;
                    }
                }

                switch (weapon.WeaponBullet.bulletType)
                {
                    case Bullet.ammoType.NORMAL:
                        if (player.Ammo == 120 && player.GetComponent<Roll>() != null || player.Ammo == 99 && player.GetComponent<Roll>() == null || weapon.Melee)
                        {
                            weapon = new Weapon();
                            Bullet bullet = new Bullet();

                            bullet.bulletType = (Bullet.ammoType)Random.Range(0, 4);

                            weapon.WeaponBullet = bullet;
                        }
                        break;
                    case Bullet.ammoType.SHELL:
                        if (player.ShellAmmo == 120 && player.GetComponent<Roll>() != null || player.ShellAmmo == 99 && player.GetComponent<Roll>() == null || weapon.Melee)
                        {
                            weapon = new Weapon();
                            Bullet bullet = new Bullet();

                            bullet.bulletType = (Bullet.ammoType)Random.Range(0, 4);

                            weapon.WeaponBullet = bullet;
                        }
                        break;
                    case Bullet.ammoType.ENERGY:
                        if (player.EnergyAmmo == 120 && player.GetComponent<Roll>() != null || player.EnergyAmmo == 99 && player.GetComponent<Roll>() == null || weapon.Melee)
                        {
                            weapon = new Weapon();
                            Bullet bullet = new Bullet();

                            bullet.bulletType = (Bullet.ammoType)Random.Range(0, 4);

                            weapon.WeaponBullet = bullet;
                        }
                        break;
                    case Bullet.ammoType.EXPLOSION:
                        if (player.ExplosiveAmmo == 120 && player.GetComponent<Roll>() != null || player.ExplosiveAmmo == 99 && player.GetComponent<Roll>() == null || weapon.Melee)
                        {
                            weapon = new Weapon();
                            Bullet bullet = new Bullet();

                            bullet.bulletType = (Bullet.ammoType)Random.Range(0, 4);

                            weapon.WeaponBullet = bullet;
                        }
                        break;
                    case Bullet.ammoType.BOLT:
                        if (player.BoltAmmo == 120 && player.GetComponent<Roll>() != null || player.BoltAmmo == 99 && player.GetComponent<Roll>() == null || weapon.Melee)
                        {
                            weapon = new Weapon();
                            Bullet bullet = new Bullet();                            

                            bullet.bulletType = (Bullet.ammoType)Random.Range(0, 4);

                            weapon.WeaponBullet = bullet;
                        }
                        break;

                }

                GiveAmmo(collision, weapon);
            }
        }
    }

    private void GiveAmmo(Collider2D collision, Weapon weapon)
    {
        StatsClass player = collision.GetComponent<StatsClass>();
        if (player.GetComponent<Roll>() != null)
        {
            switch (weapon.WeaponBullet.bulletType)
            {
                case Bullet.ammoType.NORMAL:
                    player.Ammo += 40 * multiplier;
                    GameManager.instance.TextSpawn("+" + 40 * multiplier + " BULLETS", transform);

                    if (player.Ammo > GameManager.instance.BulletAmmoCap)
                    {
                        player.Ammo = GameManager.instance.BulletAmmoCap;
                    }
                    break;
                case Bullet.ammoType.SHELL:
                    player.ShellAmmo += 10 * multiplier;
                    GameManager.instance.TextSpawn("+" + 10 * multiplier + " BULLETS", transform);
                    if (player.ShellAmmo > GameManager.instance.OtherAmmoCap)
                    {
                        player.ShellAmmo = GameManager.instance.OtherAmmoCap;
                    }
                    break;
                case Bullet.ammoType.ENERGY:
                    player.EnergyAmmo += 10 * multiplier;
                    GameManager.instance.TextSpawn("+" + 10 * multiplier + " BULLETS", transform);

                    if (player.EnergyAmmo > GameManager.instance.OtherAmmoCap)
                    {
                        player.EnergyAmmo = GameManager.instance.OtherAmmoCap;
                    }
                    break;
                case Bullet.ammoType.EXPLOSION:
                    player.ExplosiveAmmo += 8 * multiplier;
                    GameManager.instance.TextSpawn("+" + 8 * multiplier + " BULLETS", transform);

                    if (player.ExplosiveAmmo > GameManager.instance.OtherAmmoCap)
                    {
                        player.ExplosiveAmmo = GameManager.instance.OtherAmmoCap;
                    }
                    break;
                case Bullet.ammoType.BOLT:
                    player.BoltAmmo += 7 * multiplier;
                    GameManager.instance.TextSpawn("+" + 7 * multiplier + " BULLETS", transform);

                    if (player.BoltAmmo > GameManager.instance.OtherAmmoCap)
                    {
                        player.BoltAmmo = GameManager.instance.OtherAmmoCap;
                    }
                    break;
            }
        }
        //Other characters ammo
        else
        {
            switch (weapon.WeaponBullet.bulletType)
            {
                case Bullet.ammoType.NORMAL:
                    player.Ammo += 32 * multiplier;
                    GameManager.instance.TextSpawn("+" + 32 * multiplier+ " BULLETS", transform);

                    if (player.Ammo > 120)
                    {
                        player.Ammo = 120;
                    }
                    break;
                case Bullet.ammoType.SHELL:
                    player.ShellAmmo += 8 * multiplier;
                    GameManager.instance.TextSpawn("+" + 8 * multiplier + " BULLETS", transform);

                    if (player.ShellAmmo > 120)
                    {
                        player.ShellAmmo = 120;
                    }
                    break;
                case Bullet.ammoType.ENERGY:
                    player.EnergyAmmo += 10 * multiplier;
                    GameManager.instance.TextSpawn("+" + 10 * multiplier + " BULLETS", transform);

                    if (player.EnergyAmmo > 120)
                    {
                        player.EnergyAmmo = 120;
                    }

                    break;
                case Bullet.ammoType.EXPLOSION:
                    player.ExplosiveAmmo += 6 * multiplier;
                    GameManager.instance.TextSpawn("+" + 6 * multiplier + " BULLETS", transform);

                    if (player.ExplosiveAmmo > 120)
                    {
                        player.ExplosiveAmmo = 120;
                    }
                    break;
                case Bullet.ammoType.BOLT:
                    player.BoltAmmo += 7 * multiplier;
                    GameManager.instance.TextSpawn("+" + 7 * multiplier + " BULLETS", transform);

                    if (player.BoltAmmo > 120)
                    {
                        player.BoltAmmo = 120;
                    }
                    break;
            }
        }

        ui.UpdateAmmo();
        AudioManager.instance.Play("Reload");
    }
}
