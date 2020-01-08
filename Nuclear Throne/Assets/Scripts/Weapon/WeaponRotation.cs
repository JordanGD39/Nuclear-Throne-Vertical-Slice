using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField]
    private RectTransform _canvas;

    private GameObject holder;
    private Transform player;
    private SpriteRenderer childObj;
    private bool playerControl = true;

    private void Start()
    {
        holder = transform.parent.GetChild(0).gameObject;
        if (holder.CompareTag("Player"))
        {
            playerControl = true;
        }
        else
        {
            holder = transform.parent.gameObject;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerControl = false;
        }

        childObj = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Weapon Rotation with the Mouse-position
        Vector2 aimPos;

        if (playerControl)
        {
            aimPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        else
        {
            aimPos = player.position - transform.position;
        }
        
        float rotationZ = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);

        transform.position = holder.transform.position;

        ChangeSortingLayer(holder.transform.localScale.x, transform.rotation.eulerAngles.z);        
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
