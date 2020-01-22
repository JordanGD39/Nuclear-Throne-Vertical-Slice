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
        }
        else if (!mov.CantMove && !rolling && (mov.Movement.x != 0 || mov.Movement.y != 0))
        {
            movement = mov.Movement;           
        }

        if (Input.GetButtonDown("Roll") && !rolling)
        {            
            mov.CantMove = true;
            rollTrigger = true;
            CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
            col.sharedMaterial.bounciness = 1;
            col.enabled = false;
            col.enabled = true;
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
            
            if (!addForce)
            {
                rb.AddForce(movement * rollSpeed, ForceMode2D.Impulse);
                addForce = true;
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

    public void StopRolling()
    {
        transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
        rolling = false;
        rollTrigger = false;
        StopCoroutine("RollTime");
        CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
        col.sharedMaterial.bounciness = 0;
        col.enabled = false;
        col.enabled = true;
    }

    private IEnumerator RollTime()
    {
        yield return new WaitForSeconds(0.5f);
        mov.CantMove = false;
        rollTrigger = false;
        slide = true;
        CapsuleCollider2D col = GetComponent<CapsuleCollider2D>();
        col.sharedMaterial.bounciness = 0;
        col.enabled = false;
        col.enabled = true;
        yield return new WaitForSeconds(0.39f);
        rolling = false;
        addForce = false;        
        yield return new WaitForSeconds(0.2f);
        slide = false;        
    }
}
