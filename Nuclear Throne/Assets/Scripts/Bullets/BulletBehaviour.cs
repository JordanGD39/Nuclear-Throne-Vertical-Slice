using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject explosionPref;
    public Bullet BulletFired { get { return bullet; } set { bullet = value; } }

    public bool Loaded { get; set; }
    public bool PlayerControl { get; set; }

    public Weapon WeaponThatShot { get; set; }

    private int hits = 0;

    private bool touchingSomething = false;

    private float timer = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float percentage = 0.4f;

        if (bullet.fireType == Bullet.type.EXPLOSION || bullet.fireType == Bullet.type.LASER)
        {
            percentage = 1;
            if (bullet.fireType == Bullet.type.EXPLOSION)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                rb.drag = 3;
            }
        }

        SetAnimator(bullet.fireType);
        GetSize(percentage);

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

            Destroy(gameObject, 0.15f);
        }

        if (bullet.fireType == Bullet.type.EXPLOSION)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                Explode();
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

    private void GetSize(float percentage)
    {
        Vector2 s;
        if (bullet.fireType != Bullet.type.MELEE)
        {
            s = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size * percentage;
            GetComponent<BoxCollider2D>().size = s;
        }
        else
        {
            s = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            GetComponent<BoxCollider2D>().size = s;
            GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x,
            GetComponent<BoxCollider2D>().offset.y + (gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2));
        }

        transform.GetChild(0).GetComponent<BoxCollider2D>().size = s;
    }

    private void SetAnimator(Bullet.type type)
    {
        Animator anim = GetComponent<Animator>();

        if (type == Bullet.type.MELEE)
        {
            anim.enabled = true;
            anim.SetTrigger("SwingTrigger");
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
            if (PlayerControl && collision.CompareTag("Bullet") && bullet.fireType == Bullet.type.MELEE)
            {
                BulletBehaviour enemBulBhv = collision.GetComponent<BulletBehaviour>();

                if (WeaponThatShot.Name != "Screwdriver" && !enemBulBhv.PlayerControl)
                {
                    enemBulBhv.rb.velocity *= -1;
                }
                else if (WeaponThatShot.Name == "Screwdriver" && !enemBulBhv.PlayerControl)
                {
                    Destroy(collision.gameObject);
                }

                enemBulBhv.PlayerControl = true;
            }
            else
            {
                if (!bullet.Explode && !collision.CompareTag("Player") && !collision.CompareTag("Enemy") && !collision.CompareTag("Bullet"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void Explode()
    {
        Instantiate(explosionPref, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (bullet.Hits > hits && (collision.gameObject.name != "PhysicsMat"))
        {
            if (PlayerControl && collision.CompareTag("Enemy") && !collision.GetComponent<EnemyAi>().Dead)
            {
                if (!bullet.Explode)
                {
                    collision.GetComponent<EnemyAi>().Hit(WeaponThatShot.Damage, rb.velocity);
                    hits++;
                }
                else
                {
                    Explode();
                    hits++;
                }
            }
            else if (!PlayerControl && collision.CompareTag("Player"))
            {
                if (!bullet.Explode)
                {
                    collision.GetComponent<Player>().Hit(WeaponThatShot.Damage, rb.velocity, true);
                    hits++;
                }
                else
                {
                    Explode();
                    hits++;
                }
            }
            else
            {
                if (!collision.CompareTag("Player") && !collision.CompareTag("Enemy") &&
                    !collision.CompareTag("Bullet") && bullet.fireType != Bullet.type.MELEE && !bullet.Explode)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
