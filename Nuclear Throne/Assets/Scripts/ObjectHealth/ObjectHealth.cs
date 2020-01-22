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
