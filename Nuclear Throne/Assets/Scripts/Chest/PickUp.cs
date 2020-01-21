using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    public Weapon WeaponOfGameObject { get { return weapon; } set { weapon = value; } }

    [SerializeField] private GameObject pickUpPref;

    private Rigidbody2D rb;

    private bool notLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (weapon != null)
        {
            SetCollider();
        }
        else
        {
            notLoaded = true;
        }
    }

    private void SetCollider()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        SpriteRenderer spr = GetComponent<SpriteRenderer>();

        spr.sprite = weapon.SpriteOfWeapon;

        col.size = spr.sprite.bounds.size + new Vector3(0.25f, 0.25f, 0);
        col.offset = spr.sprite.bounds.center;

        transform.GetChild(0).GetComponent<BoxCollider2D>().size = spr.sprite.bounds.size;
        transform.GetChild(0).GetComponent<BoxCollider2D>().offset = spr.sprite.bounds.center;
    }

    private void Update()
    {
        if (!notLoaded) return;

        SetCollider();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > 1)
        {
            rb.velocity *= 0.9f;
        }
        else
        {
            rb.velocity *= 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetButtonDown("Pickup"))
        {
            StatsClass playerStats = collision.GetComponent<StatsClass>();

            if (playerStats.Secondary != null)
            {
                GameObject oldWeapon = Instantiate(pickUpPref, transform.position, transform.rotation);
                oldWeapon.GetComponent<PickUp>().weapon = playerStats.Primary;
            }
            else
            {
                playerStats.Secondary = playerStats.Primary;
            }
            
            playerStats.Primary = weapon;

            Transform playerWeapon = playerStats.transform.parent.GetChild(1).GetChild(0);

            playerWeapon.GetComponent<SpriteRenderer>().sprite = playerStats.Primary.SpriteOfWeapon;
            playerWeapon.transform.GetChild(0).localPosition = new Vector3(0, playerStats.Primary.ShootCoords, 0);
            GameManager.instance.TextSpawn(playerStats.Primary.Name + "!", transform);
            Destroy(gameObject);
        }
    }
}
