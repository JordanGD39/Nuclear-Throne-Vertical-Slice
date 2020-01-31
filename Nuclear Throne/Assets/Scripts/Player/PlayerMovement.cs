using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform cursor;
    private Player player;

    [SerializeField] private float speed;
    public float Speed { get{ return speed; } set { speed = value; } }
    public bool CantMove { get; set; } = false;

    private Vector2 movement;
    public Vector2 Movement { get { return movement; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Dead) return;

        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movement = movement.normalized;
        if (!CantMove)
        {
            anim.SetFloat("Speed", movement.magnitude);

            if ((transform.position.x - cursor.position.x) < -0.2f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            }
            else if ((transform.position.x - cursor.position.x) > 0.2f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!CantMove && !player.Dead)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }        
    }
}
