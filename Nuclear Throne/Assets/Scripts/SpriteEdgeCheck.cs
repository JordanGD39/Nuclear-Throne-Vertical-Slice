using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEdgeCheck : MonoBehaviour
{
    [SerializeField] private GameObject[] sprites;
    [SerializeField] private Sprite[] extraSprites;
    [SerializeField] private GameObject[] edges;
    public GameObject[] Edges { get { return edges; } }

    private enum downSpriteDir { NORMAL, LEFT, RIGHT, BOTH}
    private downSpriteDir spriteDir = downSpriteDir.NORMAL;

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

        if (hitLeft.collider == null && edges[2] == null)
        {
            edges[2] = Instantiate(sprites[2], gameObject.transform, false);           
        }
        else if (hitLeft.collider != null && hitLeft.collider.GetComponent<SpriteEdgeCheck>().sprites[1] != null)
        {
            spriteDir = downSpriteDir.LEFT;
        }

        if (hitRight.collider == null && edges[3] == null)
        {
            edges[3] = Instantiate(sprites[3], gameObject.transform, false);            
        }
        else if (hitRight.collider != null && hitRight.collider.GetComponent<SpriteEdgeCheck>().sprites[1] != null)
        {
            if (spriteDir == downSpriteDir.LEFT)
            {
                spriteDir = downSpriteDir.BOTH;
            }
            else
            {
                spriteDir = downSpriteDir.RIGHT;
            }
        }

        if (hitDown.collider == null && edges[1] == null)
        {
            edges[1] = Instantiate(sprites[1], gameObject.transform, false);
            if (spriteDir != downSpriteDir.NORMAL)
            {
                edges[1].GetComponent<SpriteRenderer>().sprite = extraSprites[(int)spriteDir - 1];
            }
        }

        if (edges[0] != null || edges[1] != null|| edges[2] != null|| edges[3] != null)
        {
            GetComponent<SpriteRenderer>().sprite = extraSprites[3];
            if (edges[4] == null)
            {
                edges[4] = Instantiate(sprites[4], new Vector3(transform.position.x, transform.position.y - transform.parent.position.y, transform.position.z), transform.rotation);
            }
        }
    }
}
