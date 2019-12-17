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

    void Start()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        //Weapon Rotation with the Mouse-position
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
    }
}
