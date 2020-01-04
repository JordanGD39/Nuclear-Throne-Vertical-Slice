using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEdgeScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SpriteEdgeCheck>() != null)
        {
            StartCoroutine(DelayCheck(collision));
        }
    }

    private IEnumerator DelayCheck(Collider2D collision)
    {
        yield return null;
        if (collision != null)
        {
            collision.GetComponent<SpriteEdgeCheck>().CheckEdges();
        }
    }
}
