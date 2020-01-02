using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEdgeCheck : MonoBehaviour
{
    [SerializeField] private GameObject[] sprites;

    public bool CheckedEdge { get; set; } = false;

    private void Start()
    {
        CheckedEdge = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckedEdge)
        {
            int tileLayer = LayerMask.GetMask("Tile");

            RaycastHit2D hitUp = Physics2D.Raycast(transform.position + new Vector3(0, 0.6f, 0), Vector2.up, 0.1f, tileLayer);
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position - new Vector3(0, 0.6f, 0), -Vector2.up, 0.1f, tileLayer);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(0.6f, 0, 0), -Vector2.right, 0.1f, tileLayer);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(0.6f, 0, 0), Vector2.right, 0.1f, tileLayer);

            if (hitUp.collider == null)
            {                
                Instantiate(sprites[0], gameObject.transform, false);
            }

            if (hitDown.collider == null)
            {
                Instantiate(sprites[1], gameObject.transform, false);
            }

            if (hitLeft.collider == null)
            {
                Instantiate(sprites[2], gameObject.transform, false);
            }

            if (hitRight.collider == null)
            {
                Instantiate(sprites[3], gameObject.transform, false);
            }

            CheckedEdge = true;
            GetComponent<SpriteEdgeCheck>().enabled = false;
        }
    }
}
