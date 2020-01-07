using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    private PlayerMovement mov;
    private Rigidbody2D rb;
    private Vector3 mousePos;
    [SerializeField] private Vector2 movement;
    [SerializeField] private float rollSpeed;
    private bool rolling = false;
    private bool addForce = false;
    private bool bounce = false;
    public bool WallHori { get; set; }
    public bool WallVert { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        mov = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!mov.CantMove && !rolling && mov.Movement.x == 0 && mov.Movement.y == 0)
        {
            movement = (mousePos - transform.position).normalized;
            movement.Normalize();
        }
        else if (!mov.CantMove && !rolling && (mov.Movement.x != 0 || mov.Movement.y != 0))
        {
            movement = mov.Movement;           
        }

        if (Input.GetButtonDown("Active") && !rolling)
        {
            mov.CantMove = true;
        }

        if (mov.CantMove && rolling)
        {
            if (transform.localScale.x > 0)
            {
                transform.GetChild(0).Rotate(0, 0, -30);
            }
            else
            {
                transform.GetChild(0).Rotate(0, 0, 30);
            }            
        }
        else
        {
            transform.GetChild(0).rotation = new Quaternion(0, 0, 0, transform.rotation.w);
        }
    }

    private void FixedUpdate()
    {

        if (mov.CantMove)
        {
            rolling = true;
            if (!addForce && !bounce)
            {
                rb.AddForce(movement * rollSpeed, ForceMode2D.Impulse);
                addForce = true;
            }
            else if (bounce)
            {
                if (WallHori)
                {
                    rb.velocity *= 0;
                    rb.AddForce(new Vector2(-movement.x, movement.y) * rollSpeed, ForceMode2D.Impulse);
                }

                if (WallVert)
                {
                    rb.velocity *= 0;
                    rb.AddForce(new Vector2(movement.x, -movement.y) * rollSpeed, ForceMode2D.Impulse);
                }

                bounce = false;
            }

            StartCoroutine(RollTime());            

            if (rb.velocity.magnitude > 10)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, 10);
            }
        }
    }

    private IEnumerator RollTime()
    {
        yield return new WaitForSeconds(0.3f);
        mov.CantMove = false;
        yield return new WaitForSeconds(0.39f);
        rolling = false;
        rb.velocity *= 0;
        addForce = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (mov.CantMove)
        {
            StopCoroutine("RollTime");
            StartCoroutine("RollTime");
            bounce = true;
        }
    }
}
