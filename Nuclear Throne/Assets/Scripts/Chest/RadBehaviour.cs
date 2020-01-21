using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadBehaviour : MonoBehaviour
{
    const float VEL_THRESHOLD = 0.2f;

    private Rigidbody2D rb;

    private bool pickable;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pickable = false;

        StartCoroutine(PickCorountine());
    }

    private void Update()
    {
        rb.velocity *= 0.95f;
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

    IEnumerator PickCorountine()
    {
        yield return new WaitForSeconds(0.5f);

        pickable = true;
    }
}
