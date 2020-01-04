using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEdgeCheck : MonoBehaviour
{
    [SerializeField] private GameObject[] sprites;
    [SerializeField] private GameObject[] edges;

    private void Start()
    {
        CheckEdges();
    }

    // Update is called once per frame
    public void CheckEdges()
    {
        int tileLayer = LayerMask.GetMask("Tile");

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position + new Vector3(0, 0.6f, 0), Vector2.up, 0.1f, tileLayer);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position - new Vector3(0, 0.6f, 0), -Vector2.up, 0.1f, tileLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(0.6f, 0, 0), -Vector2.right, 0.1f, tileLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(0.6f, 0, 0), Vector2.right, 0.1f, tileLayer);

        if (hitUp.collider == null && edges[0] == null)
        {                
            edges[0] = Instantiate(sprites[0], gameObject.transform, false);
        }

        if (hitDown.collider == null && edges[1] == null)
        {
            edges[1] = Instantiate(sprites[1], gameObject.transform, false);
        }

        if (hitLeft.collider == null && edges[2] == null)
        {
            edges[2] = Instantiate(sprites[2], gameObject.transform, false);
        }

        if (hitRight.collider == null && edges[3] == null)
        {
            edges[3] = Instantiate(sprites[3], gameObject.transform, false);
        }
    }
}
