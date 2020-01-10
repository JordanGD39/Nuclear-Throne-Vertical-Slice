using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool beingHit = false;
    public bool Dead { get; set; } = false;
    private bool getKnockback = false;
    private Vector2 knockback;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (beingHit && getKnockback)
        {
            GetComponent<PlayerMovement>().CantMove = true;
            rb.AddForce(knockback.normalized, ForceMode2D.Impulse);
        }
    }

    public void Hit(int dmg, Vector2 velocity, bool knocked)
    {
        StatsClass stats = GetComponent<StatsClass>();        
        if (!beingHit && !Dead)
        {
            GetComponent<Roll>().StopRolling();
            rb.velocity *= 0;
            knockback = velocity;
            if (stats.Health > 0)
            {
                getKnockback = knocked;
            }
            else
            {
                getKnockback = true;
            }
            beingHit = true;
            stats.Health -= dmg;            
            StartCoroutine(HitCoroutine());          
        }
    }

    private IEnumerator HitCoroutine()
    {
        yield return new WaitForSeconds(0.08f);

        for (int i = 0; i < 9; i++)
        {
            rb.velocity *= 0.9f;
            yield return null;
        }

        beingHit = false;

        if (GetComponent<StatsClass>().Health <= 0)
        {
            Dead = true;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.grey;
            Destroy(transform.parent.GetChild(1).gameObject);
            GetComponent<PlayerMovement>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            rb.velocity *= 0;

        }
        else
        {
            GetComponent<PlayerMovement>().CantMove = false;
        }        
    }
}
