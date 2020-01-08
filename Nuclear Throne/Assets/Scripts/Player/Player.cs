using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool beingHit = false;
    private Vector2 knockback;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (beingHit)
        {
            rb.AddForce(knockback.normalized * 5, ForceMode2D.Impulse);
        }
    }

    public void Hit(int dmg, Vector2 velocity)
    {
        StatsClass stats = GetComponent<StatsClass>();
        if (!beingHit)
        {
            knockback = velocity;
            beingHit = true;
            stats.Health -= dmg;
            GetComponent<PlayerMovement>().CantMove = true;
            if (stats.Health <= 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(HitCoroutine());
            }            
        }
    }

    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.08f);

        for (int i = 0; i < 9; i++)
        {
            rb.velocity *= 0.9f;
        }
        yield return new WaitForSeconds(0.05f);

        beingHit = false;
        GetComponent<PlayerMovement>().CantMove = false;
    }
}
