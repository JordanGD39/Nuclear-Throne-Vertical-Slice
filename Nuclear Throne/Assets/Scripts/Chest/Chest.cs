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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetButtonDown("Pickup"))
        {
            GetComponent<BoxCollider2D>().enabled = false;

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
        }
    }

    private void GiveAmmo(Collider2D collision, Weapon weapon)
    {
        bool norm = false;
        bool shell = false;
        bool bolt = false;
        bool explosive = false;
        bool energy = false;

        StatsClass player = collision.GetComponent<StatsClass>();

        switch (weapon.WeaponBullet.fireType)
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

        if (player.GetComponent<Roll>() != null)
        {
            if (norm)
            {
                player.Ammo += 40 * multiplier;
            }
            else if (shell)
            {
                player.ShellAmmo += 10 * multiplier;
            }
            else if (bolt)
            {
                player.BoltAmmo += 9 * multiplier;
            }
            else if (explosive)
            {
                player.ExplosiveAmmo += 8 * multiplier;
            }
            else if (energy)
            {
                player.EnergyAmmo += 13 * multiplier;
            }
        }
        else
        {
            if (norm)
            {
                player.Ammo += 32 * multiplier;
            }
            else if (shell)
            {
                player.ShellAmmo += 8 * multiplier;
            }
            else if (bolt)
            {
                player.BoltAmmo += 7 * multiplier;
            }
            else if (explosive)
            {
                player.ExplosiveAmmo += 6 * multiplier;
            }
            else if (energy)
            {
                player.EnergyAmmo += 10 * multiplier;
            }
        }
    }
}
