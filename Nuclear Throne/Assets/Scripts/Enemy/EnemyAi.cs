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

    private Vector2 direction;

    private enum state { FOLLOW, STOP, RETREAT}
    private state enemyState;

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
    }

    private void FixedUpdate()
    {
        if (PlayerInSight)
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
    }
}
