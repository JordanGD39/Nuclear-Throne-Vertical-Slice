using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField]
    private RectTransform _canvas;

    [SerializeField]
    private Camera _camera;

    private bool automatic;

    void Start()
    {
        automatic = false;

        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        //Weapon Rotation with the Mouse-position
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);

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
