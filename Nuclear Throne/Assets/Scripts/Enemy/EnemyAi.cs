using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;

    [SerializeField] private bool playerInSight = false;
    public bool PlayerInSight { get { return playerInSight; } set { playerInSight = value; } }

    [SerializeField] private float range = 1;
    public float Range { get { return range; } }

    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreatingDistance;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private bool touchDamage;
    public bool TouchDamage { get { return touchDamage; } }

    public bool WallHori { get; set; }
    public bool WallVert { get; set; }

    private Vector2 direction;
    private Vector2 knockback;
    private float knockbackForce;
    [SerializeField] private float deathKnockback = 4;

    private enum state { FOLLOW, STOP, RETREAT}
    private state enemyState;
    private bool beingHit = false;
    private bool bouncing = false;
    public bool Dead { get; set; }

    private PhysicsMaterial2D physMat;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        physMat = new PhysicsMaterial2D();
        physMat.bounciness = 0;
        physMat.friction = 0;
        CapsuleCollider2D col = transform.GetChild(0).GetComponent<CapsuleCollider2D>();
        col.sharedMaterial = physMat;
        col.enabled = false;
        col.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInSight && !Dead)
        {
            direction = player.position - transform.position;
            direction.Normalize();

            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                enemyState = state.FOLLOW;
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatingDistance)
            {
                enemyState = state.STOP;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatingDistance)
            {
                enemyState = state.RETREAT;
            }                       
        }
        else
        {
            enemyState = state.STOP;
        }

        if (!beingHit && !Dead)
        {
            ChangeDirection();
        }
    }

    private void FixedUpdate()
    {
        if (PlayerInSight && !beingHit)
        {
            switch (enemyState)
            {
                case state.FOLLOW:
                    rb.velocity = direction * speed;
                    break;
                case state.STOP:
                    rb.velocity *= 0.9f;
                    break;
                case state.RETREAT:
                    rb.velocity = direction * -speed;
                    break;
            }
        }
        else if(beingHit)
        {
            rb.AddForce(knockback.normalized * knockbackForce, ForceMode2D.Impulse);
        }

        if (Dead && !beingHit)
        {
            rb.velocity *= 0.9f;
        }
    }

    private void ChangeDirection()
    {
        if (damage > 0)
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            }
            else if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            }
        }
        else
        {
            if ((transform.position.x - player.position.x) < -0.2f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            }
            else if ((transform.position.x - player.position.x) > 0.2f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            }
        }
    }

    public void Hit(int dmg, Vector2 velocity)
    {
        if (!Dead && !beingHit)
        {
            rb.velocity *= 0;
            StatsClass stats = GetComponent<StatsClass>();
            knockback = velocity;
            knockbackForce = 1;
            beingHit = true;
            stats.Health -= dmg;

            if (stats.Health <= 0)
            {
                CapsuleCollider2D col = transform.GetChild(0).GetComponent<CapsuleCollider2D>();
                col.sharedMaterial.bounciness = 1;
                col.enabled = false;
                col.enabled = true;

                transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Corpses";

                knockbackForce = deathKnockback;
                Dead = true;
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.grey;
                Destroy(transform.GetChild(1).gameObject);                
            }

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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (touchDamage && collision.CompareTag("Player") && !Dead)
        {
            collision.GetComponent<Player>().Hit(damage, rb.velocity, false);
        }

        if (Dead && collision.CompareTag("Enemy") && rb.velocity.magnitude > 1)
        {
            collision.GetComponent<EnemyAi>().Hit(1, rb.velocity);
        }
    }
}
