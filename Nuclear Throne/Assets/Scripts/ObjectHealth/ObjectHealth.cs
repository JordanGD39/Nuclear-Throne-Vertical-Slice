using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] protected int health;

    protected BoxCollider2D col;
    protected bool active;

    virtual protected void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = true;
        active = true;
    }

    virtual protected void Update()
    {
        if (!active)
        {
            col.enabled = false;

            if (transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);                 
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15 && active) //Bullet Layer
        {
            health--;
        }
    }
}
