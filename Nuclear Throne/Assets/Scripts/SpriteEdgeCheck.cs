using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEdgeCheck : MonoBehaviour
{
    [SerializeField] private GameObject[] sprites;

    private bool checkedEdge = false;

    private void Start()
    {
        checkedEdge = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkedEdge)
        {
            RaycastHit2D hitUp = Physics2D.Raycast(transform.position + new Vector3(0, 0.6f, 0), Vector2.up, 0.1f);
            RaycastHit2D hitDown = Physics2D.Raycast(transform.position - new Vector3(0, 0.6f, 0), -Vector2.up, 0.1f);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - new Vector3(0.6f, 0, 0), -Vector2.right, 0.1f);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + new Vector3(0.6f, 0, 0), Vector2.right, 0.1f);

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

            checkedEdge = true;

            GetComponent<SpriteEdgeCheck>().enabled = false;
        }
    }
}
