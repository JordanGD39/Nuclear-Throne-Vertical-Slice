using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFunctions : MonoBehaviour
{
    private bool automatic;

    void Start()
    {
        automatic = false;
    }

    void Update()
    {
        InputButtons();
    }

    private void InputButtons()
    {
        //Left Mouse-button Click: Fire
        if (Input.GetButtonDown("Fire1") && !automatic)
        {
            Debug.Log("Pang!");
        }
        else if (Input.GetButton("Fire1") && automatic)
        {
            Debug.Log("Prrangpangang!");
        }

        //E-key Press: Weapon Switch
        if (Input.GetButtonDown("Switch"))
        {
            if (automatic)
            {
                automatic = false;
            }
            else
            {
                automatic = true;
            }
        }
    }
}
