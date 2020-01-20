using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform cursor;

    [SerializeField] private float speed;
    public bool CantMove { get; set; } = false;

    private Vector2 movement;
    public Vector2 Movement { get { return movement; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (!CantMove)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }        
    }
}
