using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : AttackBox
{
    [SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] private Bullet bullet;

    private int wallHitCounter;

    protected override void Start()
    {
        base.Start();
        wallHitCounter = 0;

        if (Loaded)
        {
            rb.velocity = transform.TransformVector(Vector3.up * speed);
        }
    }

    void Update()
    {

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
        else if (collision.gameObject.layer == 8)
        {
            rb.velocity = transform.InverseTransformVector(rb.velocity);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 180.0f);

            /*if (wallHitCounter <= bullet.WallHits)
            {
                wallHitCounter++;

            }
            else
            {
                Destroy(gameObject);
            }*/
        }
        else
        {
            if (!collision.CompareTag("Bullet") && !collision.CompareTag("Player"))
            {
                Destroy(gameObject);
            }            
        }
    }
}
