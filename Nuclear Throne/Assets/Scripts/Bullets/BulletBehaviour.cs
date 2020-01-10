using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : AttackBox
{
    [SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    public bool PlayerControl { get; set; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (Loaded)
        {
            rb.velocity = transform.TransformVector(Vector3.up * speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bullet.Hits == 1)
        {
            if (PlayerControl && collision.CompareTag("Enemy") && !collision.GetComponent<EnemyAi>().Dead)
            {
                collision.GetComponent<EnemyAi>().Hit(WeaponThatShot.Damage, rb.velocity);
                if (bullet.Dissapear)
                {
                    Destroy(gameObject);
                }
            }
            else if (!PlayerControl && collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().Hit(WeaponThatShot.Damage, rb.velocity, true);
                if (bullet.Dissapear)
                {
                    Debug.Log("WHYYY");
                    Destroy(gameObject);
                }
            }
            else
            {
                if (!collision.CompareTag("Bullet") && !collision.CompareTag("Player") && !collision.CompareTag("Enemy"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
