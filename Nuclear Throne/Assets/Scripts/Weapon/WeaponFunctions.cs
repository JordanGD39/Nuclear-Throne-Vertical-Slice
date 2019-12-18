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
        player = transform.parent.GetChild(0).GetComponent<PlayerClass>();
    }

    void Update()
    {
        InputButtons();
    }

    private void InputButtons()
    {
        //Left Mouse-button Click: Fire
        if (Input.GetButtonDown("Fire1"))
        {
            
        }

        //E-key Press: Weapon Switch
        if (Input.GetButtonDown("Switch"))
        {
            Weapon holder = player.Primary;
            player.Primary = player.Secondary;
            player.Secondary = holder;
        }
    }
}
