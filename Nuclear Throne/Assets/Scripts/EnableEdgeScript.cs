using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEdgeScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (!collision.GetComponent<SpriteEdgeCheck>().enabled)
            {
                collision.GetComponent<SpriteEdgeCheck>().enabled = true;
                collision.GetComponent<SpriteEdgeCheck>().CheckedEdge = false;
            }
        }
    }
}
