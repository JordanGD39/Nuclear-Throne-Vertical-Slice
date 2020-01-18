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
        physMat.bounciness = 0;
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
            BulletBehaviour bBehaviour = GetComponentInParent<BulletBehaviour>();
            bBehaviour.RigBod.velocity *= 0.8f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            physMat.bounciness = 1;

            if (!wallHit)
            {
                //transform.rotation = Quaternion.Euler(transform.eulerAngles.x,
                //transform.eulerAngles.y, transform.eulerAngles.z + 180.0f);
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
