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

        float randomAngle = Random.Range(0, (2 + Mathf.PI));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                                             (-90.0f + (randomAngle * (360 / (2 + Mathf.PI)))));

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
            Debug.Log("Wall.");
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

    IEnumerator PickCorountine()
    {
        yield return new WaitForSeconds(0.5f);

        pickable = true;
    }
}
