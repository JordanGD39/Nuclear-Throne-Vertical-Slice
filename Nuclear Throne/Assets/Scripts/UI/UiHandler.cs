using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    private Transform healthUI;
    private Transform ammoUI;
    private Transform weaponUI;
    private Transform radUI;
    private StatsClass playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatsClass>();
        Transform main = transform.GetChild(0);
        weaponUI = main.GetChild(2);
        radUI = main.GetChild(0);
        healthUI = main.GetChild(1);
        ammoUI = main.GetChild(3);

        UpdateWeapon();
        UpdateAmmo();
        UpdateHealth();
        UpdateRads();
    }

    public void UpdateHealth()
    {
        float health = playerStats.Health;

        healthUI.GetChild(1).GetComponent<Image>().fillAmount = health / 8;
        healthUI.GetChild(2).GetComponent<Text>().text = playerStats.Health + "/8";
    }

    public void UpdateWeapon()
    {
        if (weaponUI == null)
        {
            return;
        }

        weaponUI.GetChild(0).GetComponent<Image>().sprite = playerStats.Primary.UiSprite;
        if (playerStats.Secondary != null)
        {
            weaponUI.GetChild(2).gameObject.SetActive(true);
            weaponUI.GetChild(3).gameObject.SetActive(true);
            weaponUI.GetChild(2).GetComponent<Image>().sprite = playerStats.Secondary.UiSprite;
        }
        else
        {
            weaponUI.GetChild(2).gameObject.SetActive(false);
            weaponUI.GetChild(3).gameObject.SetActive(false);
        }
    }

    public void UpdateAmmo()
    {
        if (ammoUI == null)
        {
            return;
        }
        //255, 196, 0 RGB
        int ammo = 0;

        for (int i = 0; i < 5; i++)
        {
            ammoUI.GetChild(i).GetChild(3).gameObject.SetActive(false);
        }

        switch (playerStats.Primary.WeaponBullet.bulletType)
        {
            case Bullet.ammoType.NORMAL:
                ammo = playerStats.Ammo;
                ammoUI.GetChild(0).GetChild(3).gameObject.SetActive(true);
                break;
            case Bullet.ammoType.SHELL:
                ammo = playerStats.ShellAmmo;
                ammoUI.GetChild(1).GetChild(3).gameObject.SetActive(true);
                break;
            case Bullet.ammoType.ENERGY:
                ammo = playerStats.EnergyAmmo;
                ammoUI.GetChild(4).GetChild(3).gameObject.SetActive(true);
                break;
            case Bullet.ammoType.EXPLOSION:
                ammo = playerStats.ExplosiveAmmo;
                ammoUI.GetChild(3).GetChild(3).gameObject.SetActive(true);
                break;
            case Bullet.ammoType.BOLT:
                ammo = playerStats.BoltAmmo;
                ammoUI.GetChild(2).GetChild(4).gameObject.SetActive(true);
                break;
        }

        weaponUI.GetChild(1).GetComponent<Text>().text = ammo.ToString();

        if (playerStats.Secondary != null)
        {
            switch (playerStats.Secondary.WeaponBullet.bulletType)
            {
                case Bullet.ammoType.NORMAL:
                    ammo = playerStats.Ammo;
                    break;
                case Bullet.ammoType.SHELL:
                    ammo = playerStats.ShellAmmo;
                    break;
                case Bullet.ammoType.ENERGY:
                    ammo = playerStats.EnergyAmmo;
                    break;
                case Bullet.ammoType.EXPLOSION:
                    ammo = playerStats.ExplosiveAmmo;
                    break;
                case Bullet.ammoType.BOLT:
                    ammo = playerStats.BoltAmmo;
                    break;
            }
        }

        weaponUI.GetChild(3).GetComponent<Text>().text = ammo.ToString();

        for (int i = 0; i < 5; i++)
        {
            float ammoFloat = 0;

            Transform currAmmo = ammoUI.GetChild(i);

            switch (i)
            {
                case 0:
                    ammoFloat = playerStats.Ammo;
                    break;
                case 1:
                    ammoFloat = playerStats.ShellAmmo;
                    break;
                case 2:
                    ammoFloat = playerStats.BoltAmmo;                    
                    break;
                case 3:
                    ammoFloat = playerStats.ExplosiveAmmo;
                    break;
                case 4:
                    ammoFloat = playerStats.EnergyAmmo;
                    break;
            }

            for (int j = 0; j < ammoUI.GetChild(i).GetChild(0).childCount; j++)
            {
                if (ammoFloat <= 0)
                {
                    currAmmo.GetChild(0).GetChild(j).GetComponent<Image>().color = new Color32(50, 57, 62, 255);
                }
                else
                {
                    currAmmo.GetChild(0).GetChild(j).GetComponent<Image>().color = new Color32(155, 155, 155, 255);
                }
            }

            currAmmo.GetChild(1).GetComponent<Image>().fillAmount = ammoFloat / 120;

            if (ammoFloat < 60)
            {
                currAmmo.GetChild(1).GetComponent<Image>().color = Color.red;
            }
            else
            {
                currAmmo.GetChild(1).GetComponent<Image>().color = new Color32(255, 196, 0, 255);
            }
        }
    }

    public void UpdateRads()
    {        
        if (playerStats.Rads > 60 * playerStats.Level)
        {
            playerStats.Rads = 0;
            playerStats.Level++;
        }

        float rads = playerStats.Rads;

        radUI.GetChild(2).GetComponent<Image>().fillAmount = rads / 60;
        radUI.GetChild(4).GetComponent<Text>().text = playerStats.Level.ToString();
    }
}
