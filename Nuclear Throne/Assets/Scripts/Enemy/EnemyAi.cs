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
    [SerializeField] private float damage;
    public float Damage { get { return damage; } }

    private Vector2 direction;
    private Vector2 knockback;

    private enum state { FOLLOW, STOP, RETREAT}
    private state enemyState;
    private bool beingHit = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInSight)
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

        if (!beingHit)
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
            rb.AddForce(knockback.normalized * 5, ForceMode2D.Impulse);
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
        StatsClass stats = GetComponent<StatsClass>();
        knockback = velocity;
        beingHit = true;
        stats.Health -= dmg;

        if (stats.Health <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine(HitCoroutine());
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
    }
}
