using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysicsMat : MonoBehaviour
{
    private PhysicsMaterial2D physMat;
    private BoxCollider2D bCol;

    private Bullet bullet;
    private BulletBehaviour bBehaviour;

    private bool wallHit;

    private void Start()
    {
        physMat = new PhysicsMaterial2D();
        physMat.bounciness = 1;
        physMat.friction = 0;

        bCol = transform.GetComponent<BoxCollider2D>();
        bCol.isTrigger = true;
        bCol.sharedMaterial = physMat;

        wallHit = false;
        bullet = GetComponentInParent<BulletBehaviour>().BulletFired;

        if (bullet.fireType != Bullet.type.MELEE)
        {
            bCol.enabled = true;
        }
        else
        {
            bCol.enabled = false;
        }
    }

    private void Update()
    {
        if (wallHit)
        {
            Rigidbody2D rigidbody = GetComponentInParent<Rigidbody2D>();
            rigidbody.velocity *= 0.98f;

            transform.parent.rotation = Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y,
            (-90.0f + Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * (180 / Mathf.PI)));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (!wallHit && bullet.fireType != Bullet.type.MELEE)
            {
                Destroy(transform.parent.gameObject, 1.0f);
            }

            wallHit = true;
            bCol.isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            physMat.bounciness = 0;
            bCol.isTrigger = true;
        }
    }
}
