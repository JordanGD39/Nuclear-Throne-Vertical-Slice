using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool beingHit = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Hit(int dmg, Vector2 velocity)
    {
        StatsClass stats = GetComponent<StatsClass>();

        if (!beingHit)
        {
            beingHit = true;
            
        }
    }
}
