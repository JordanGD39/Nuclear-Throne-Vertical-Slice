using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private bool playerInSight = false;
    public bool PlayerInSight { get { return playerInSight; } set { playerInSight = value; } }

    [SerializeField] private float range = 1;
    public float Range { get { return range; } }

    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreatingDistance;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private bool follower = true;
    [SerializeField] private bool touchDamage;    
    public bool TouchDamage { get { return touchDamage; } }

    public bool WallHori { get; set; }
    public bool WallVert { get; set; }

    private Vector2 direction;
    private Vector2 knockback;
    private float knockbackForce;
    [SerializeField] private float deathKnockback = 4;

    private enum state { FOLLOW, PATROL, RETREAT}
    private state enemyState;
    private bool beingHit = false;
    private bool patrolling = false;
    private bool bouncing = false;
    public bool Dead { get; set; }

    private PhysicsMaterial2D physMat;

    [SerializeField] private bool badAimer;
    public bool BadAimer { get { return badAimer; } }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
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

            if (follower)
            {
                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    enemyState = state.FOLLOW;
                }
                else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatingDistance)
                {
                    enemyState = state.PATROL;
                }
                else if (Vector2.Distance(transform.position, player.position) < retreatingDistance)
                {
                    enemyState = state.RETREAT;
                }
            }
        }
        else
        {
            enemyState = state.PATROL;
        }

        if (!beingHit && !Dead && !badAimer)
        {
            ChangeDirection();
        }

        if (badAimer && !Dead)
        {
            PlayerDetect();
        }
    }

    private void FixedUpdate()
    {
        if (playerInSight && !beingHit)
        {
            switch (enemyState)
            {
                case state.FOLLOW:
                    rb.velocity = direction * speed;
                    break;
                case state.PATROL:
                    if (!patrolling && !Dead)
                    {                        
                        rb.velocity *= 0.9f;                        

                        if (rb.velocity.magnitude <= 0)
                        {
                            SetPatrolState();                            
                        }
                    }
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
        else
        {
            if (enemyState == state.PATROL)
            {
                if (!patrolling && !Dead)
                {
                    rb.velocity *= 0.9f;

                    if (rb.velocity.magnitude <= 0)
                    {
                        SetPatrolState();
                    }
                }
            }
        }

        if (Dead && !beingHit)
        {
            rb.velocity *= 0.9f;
        }
    }

    private void SetPatrolState()
    {
        patrolling = true;
        StartCoroutine("Patrol");
    }

    private IEnumerator Patrol()
    {
        yield return new WaitForSeconds(0.5f);

        while (enemyState == state.PATROL && !Dead)
        {
            rb.velocity *= 0;

            yield return new WaitForSeconds(2);

            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);

            rb.velocity = new Vector2(x, y) * speed;

            yield return new WaitForSeconds(1);
        }

        patrolling = false;
    }

    private void PlayerDetect()
    {
        int tileLayer = ~(LayerMask.GetMask("Weapon") | LayerMask.GetMask("WallCheck") | LayerMask.GetMask("Enemy") | LayerMask.GetMask("EnemyWallCol"));

        RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(3).position, transform.GetChild(3).up, range, tileLayer);

        Vector2 aimPos = player.position - transform.position;

        float rotationZ = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg;
        transform.GetChild(3).rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                playerInSight = true;
            }
            else
            {
                playerInSight = false;
            }
        }
        else
        {
            playerInSight = false;
        }
    }

    public void ChangeDirection()
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
            knockbackForce = 0.5f;
            beingHit = true;
            stats.Health -= dmg;

            if (stats.Health <= 0)
            {
                CapsuleCollider2D col = transform.GetChild(0).GetComponent<CapsuleCollider2D>();
                col.sharedMaterial.bounciness = 1;
                col.enabled = false;
                col.enabled = true;

                transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Corpses";

                if (stats.Primary == null)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.gray;
                }                

                knockbackForce = deathKnockback;
                Dead = true;
                Destroy(transform.GetChild(1).gameObject);
                Destroy(transform.GetChild(2).gameObject);
                anim.SetBool("Dead", true);
            }
            else
            {
                anim.SetTrigger("Hit");
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
