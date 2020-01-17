using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysicsMat : MonoBehaviour
{
    private PhysicsMaterial2D physMat;
    private BoxCollider2D bCol;

    void Start()
    {
        physMat = new PhysicsMaterial2D();
        physMat.bounciness = 0;
        physMat.friction = 0;
        bCol = transform.GetComponent<BoxCollider2D>();
        bCol.sharedMaterial = physMat;
        bCol.enabled = false;
        bCol.enabled = true;
    }

    void Update()
    {
        
    }
}
