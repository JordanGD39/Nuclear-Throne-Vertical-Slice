using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool beingHit = false;
    public bool Dead { get; set; } = false;
    private bool getKnockback = false;
    private Vector2 knockback;
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private StatsClass stats;
    private UiHandler ui;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<StatsClass>();

        spr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UiHandler>();

        if (GameManager.instance.GetComponent<StatsClass>().Primary != null)
        {
            stats.Primary = GameManager.instance.GetComponent<StatsClass>().Primary;
            stats.Secondary = GameManager.instance.GetComponent<StatsClass>().Secondary;
            stats.Ammo = GameManager.instance.GetComponent<StatsClass>().Ammo;
            stats.ShellAmmo = GameManager.instance.GetComponent<StatsClass>().ShellAmmo;
            stats.BoltAmmo = GameManager.instance.GetComponent<StatsClass>().BoltAmmo;
            stats.EnergyAmmo = GameManager.instance.GetComponent<StatsClass>().EnergyAmmo;
            stats.ExplosiveAmmo = GameManager.instance.GetComponent<StatsClass>().ExplosiveAmmo;
            stats.Level = GameManager.instance.GetComponent<StatsClass>().Level;
            stats.Rads = GameManager.instance.GetComponent<StatsClass>().Rads;
            stats.Health = GameManager.instance.GetComponent<StatsClass>().Health;
        }        
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
        if (!beingHit && !Dead)
        {
            GetComponent<Roll>().StopRolling();
            rb.velocity = Vector2.zero;
            knockback = velocity;
            stats.Health -= dmg;

            if (stats.Health < 0)
            {
                stats.Health = 0;
            }

            ui.UpdateHealth();
            if (stats.Health > 0)
            {
                getKnockback = knocked;
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
            }
            else
            {
                getKnockback = true;
            }

            beingHit = true;
            StartCoroutine("HigherLayer");
            StartCoroutine(HitCoroutine());
        }
    }

    private IEnumerator HigherLayer()
    {
        spr.sortingLayerName = "PlayerHit";
        yield return new WaitForSeconds(0.5f);
        spr.sortingLayerName = "Player";
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
            Destroy(transform.parent.GetChild(1).gameObject);
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Roll>().enabled = false;
            transform.GetChild(0).GetComponent<Animator>().SetBool("Dead", true);
            yield return new WaitForSeconds(0.5f);
            rb.velocity *= 0;

            yield return new WaitForSeconds(3);

            GameManager.instance.Difficulty = 1;
            SceneManager.LoadScene(1);
        }
        else
        {
            GetComponent<PlayerMovement>().CantMove = false;
        }
    }
}
