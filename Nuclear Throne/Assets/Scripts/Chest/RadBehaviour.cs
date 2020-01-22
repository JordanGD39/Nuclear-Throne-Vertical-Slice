using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadBehaviour : MonoBehaviour
{
    const float BLINKING_SPEED = 0.1f;

    private Rigidbody2D rb;
    private SpriteRenderer rendr;

    private bool pickable;
    private bool velDrop;
    private bool blink;

    private int counter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rendr = GetComponent<SpriteRenderer>();

        pickable = false;
        velDrop = false;
        blink = false;
        rendr.enabled = true;

        float randomAngle = Random.Range(0, (2 + Mathf.PI));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                                             (-90.0f + (randomAngle * (360 / (2 + Mathf.PI)))));

        StartCoroutine(PickCoroutine());
        StartCoroutine(DisappearStasisCoroutine());
        Destroy(gameObject, 10.0f); //Disappear after 10 Seconds
    }

    private void Update()
    {
        if (velDrop)
        {
            rb.velocity *= 0.95f;
        }

        if (blink)
        {
            counter++;

            if ((counter * Time.deltaTime) >= 0.0f && (counter * Time.deltaTime) < BLINKING_SPEED)
            {
                rendr.enabled = false;
            }
            else if ((counter * Time.deltaTime) >= BLINKING_SPEED && (counter * Time.deltaTime) < (BLINKING_SPEED * 2))
            {
                rendr.enabled = true;
            }
            else
            {
                counter = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Player Tag
        {
            if (pickable)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) //Tile Layer
        {
            rb.freezeRotation = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            velDrop = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            velDrop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) //Tile Layer
        {
            rb.freezeRotation = false;
        }
    }

    IEnumerator PickCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        pickable = true;
        velDrop = true;
    }

    IEnumerator DisappearStasisCoroutine()
    {
        yield return new WaitForSeconds(8.0f);

        blink = true;
    }
}
