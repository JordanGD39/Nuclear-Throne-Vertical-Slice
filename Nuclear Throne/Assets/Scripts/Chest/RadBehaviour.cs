using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool pickable;
    private bool velDrop;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pickable = false;
        velDrop = false;

        StartCoroutine(PickCorountine());
        velDrop = true;
    }

    private void Update()
    {
        if (velDrop)
        {
            rb.velocity *= 0.95f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Player Layer
        {
            if (pickable)
            {
                Destroy(gameObject);
            }
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

    IEnumerator PickCorountine()
    {
        yield return new WaitForSeconds(0.5f);

        pickable = true;
    }
}
