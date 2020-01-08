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
    private bool slide = false;
    private bool rollTrigger = false;
    private bool addForce = false;
    private bool bounce = false;
    private bool canBounce = false;
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
            rollTrigger = true;
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
        if (mov.CantMove && rollTrigger)
        {
            rolling = true;

            if (!addForce && !bounce)
            {
                rb.AddForce(movement * rollSpeed, ForceMode2D.Impulse);
                addForce = true;
            }
            else if (bounce && canBounce)
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

        if (slide)
        {
            rb.AddForce(rb.velocity * 10);
            rb.velocity *= 0.9f;
        }
    }

    private IEnumerator RollTime()
    {
        yield return new WaitForSeconds(0.1f);
        canBounce = true;
        yield return new WaitForSeconds(0.4f);
        mov.CantMove = false;
        rollTrigger = false;
        slide = true;
        yield return new WaitForSeconds(0.39f);
        rolling = false;
        addForce = false;        
        yield return new WaitForSeconds(0.2f);
        slide = false;
        canBounce = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (mov.CantMove && rollTrigger)
        {
            StopCoroutine("RollTime");            
            mov.CantMove = true;
            rolling = true;
            StartCoroutine("RollTime");
            bounce = true;
        }
    }
}
