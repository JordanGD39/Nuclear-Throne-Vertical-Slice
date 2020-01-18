using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public Rigidbody2D RigBod { get { return rb; } set { rb = value; } }

    [SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] private Bullet bullet;
    public Bullet BulletFired { get { return bullet; } set { bullet = value; } }

    public bool Loaded { get; set; }
    public bool PlayerControl { get; set; }

    public Weapon WeaponThatShot { get; set; }

    private int hits = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (bullet.fireType != Bullet.type.NORMAL)
        {
            Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            GetComponent<BoxCollider2D>().size = S;
        }

        if (Loaded && bullet.fireType != Bullet.type.MELEE)
        {
            rb.velocity = transform.TransformVector(Vector3.up * speed);
        }
    }

    private void Update()
    {
        if (bullet.fireType == Bullet.type.MELEE)
        {
          transform.position = transform.parent.position + transform.TransformVector(new Vector3(0.0f, 1.0f, 0.0f));

            if (WeaponThatShot.Name == "Screwdriver")
            {
                Destroy(gameObject, 0.17f);
            }
            else
            {
                Destroy(gameObject, 0.37f);
            }
        }

        if (bullet.Hits <= hits)
        {
            if (bullet.Dissapear)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
        {
            hits = 0;
        }

        if (collision.gameObject.name != "PhysicsMat")
        {
            if (PlayerControl && collision.CompareTag("Bullet") && (bullet.fireType == Bullet.type.MELEE))
            {
                //Debug.Log(collision.gameObject.name);

                BulletBehaviour enemBulBhv = collision.GetComponent<BulletBehaviour>();

                if (WeaponThatShot.Name != "Screwdriver" && !enemBulBhv.PlayerControl)
                {
                    enemBulBhv.rb.velocity *= -1;
                    enemBulBhv.transform.rotation = transform.rotation;
                }
                else if (WeaponThatShot.Name == "Screwdriver" && !enemBulBhv.PlayerControl)
                {
                    Destroy(collision.gameObject);
                }

                enemBulBhv.PlayerControl = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (bullet.Hits > hits && (collision.gameObject.name != "PhysicsMat"))
        {
            if (PlayerControl && collision.CompareTag("Enemy") && !collision.GetComponent<EnemyAi>().Dead)
            {
                collision.GetComponent<EnemyAi>().Hit(WeaponThatShot.Damage, rb.velocity);
                hits++;
            }
            else if (!PlayerControl && collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().Hit(WeaponThatShot.Damage, rb.velocity, true);
                hits++;
            }
            else
            {
                if (!collision.CompareTag("Player") && !collision.CompareTag("Enemy") && !collision.CompareTag("Bullet"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
