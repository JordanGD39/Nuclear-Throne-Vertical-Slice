using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField]
    private RectTransform _canvas;

    private GameObject player;
    private SpriteRenderer childObj;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        childObj = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Weapon Rotation with the Mouse-position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);

        transform.position = player.transform.position;

        ChangeSortingLayer(player.transform.localScale.x, transform.rotation.eulerAngles.z);
    }

    private void ChangeSortingLayer(float xScale, float zAngle)
    {
        if (xScale < 0)
        {
            childObj.flipX = true;
        }
        else if (xScale > 0)
        {
            childObj.flipX = false;
        }

        if (zAngle < 90 || zAngle > 270)
        {
            childObj.sortingOrder = -1;
        }
        else
        {
            childObj.sortingOrder = 1;
        }
    }
}
