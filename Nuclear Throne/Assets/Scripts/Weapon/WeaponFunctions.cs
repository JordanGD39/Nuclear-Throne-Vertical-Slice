using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFunctions : MonoBehaviour
{
    private bool automatic;

    private PlayerClass player;

    void Start()
    {
        automatic = false;
        player = transform.parent.parent.GetChild(0).GetComponent<PlayerClass>();
    }

    void Update()
    {
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
                    Debug.Log("PARARSKDMKD");
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log("PARARSKDMKD");
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
            Weapon holder = player.Primary;
            player.Primary = player.Secondary;
            player.Secondary = holder;
            GetComponent<SpriteRenderer>().sprite = player.Primary.SpriteOfWeapon;
        }
    }
}
