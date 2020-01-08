using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : AttackBox
{
    [SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] private Bullet bullet;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (Loaded)
        {
            rb.velocity = transform.TransformVector(Vector3.up * speed);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<StatsClass>().Health -= WeaponThatShot.Damage;
            if (bullet.Dissapear)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (!collision.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }            
        }
    }
}
