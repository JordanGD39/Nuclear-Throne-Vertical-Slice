using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadBehaviour : MonoBehaviour
{
    const float VEL_THRESHOLD = 0.2f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*if ((rb.velocity.x < VEL_THRESHOLD && rb.velocity.x > -VEL_THRESHOLD) &&
            (rb.velocity.y < VEL_THRESHOLD && rb.velocity.y > -VEL_THRESHOLD))
        {
            rb.velocity *= 0.0f;
        }
        else
        {*/
            rb.velocity *= 0.95f;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Player Layer
        {
            Destroy(gameObject);
        }
    }
}
