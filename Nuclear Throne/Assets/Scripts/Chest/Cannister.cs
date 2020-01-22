using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannister : ObjectHealth
{
    const float MAX_RADIUS = 5.0f;

    [SerializeField] private GameObject pickUpItem;
    [SerializeField] private int itemAmount;

    private Animator anim;

    protected override void Start()
    {
        anim = GetComponent<Animator>();

        base.Start();
    }

    protected override void Update()
    {
        if (health <= 0 && active)
        {
            for (int i = 0; i < itemAmount; i++)
            {
                GameObject obj = Instantiate(pickUpItem, transform.position, transform.rotation);
                float randomAngle = Random.Range(0, (2 + Mathf.PI));

                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) *
                                                         Random.Range(-MAX_RADIUS, MAX_RADIUS) * 80.0f);
            }

            active = false;
        }

        base.Update();

        if (!active)
        {
            anim.SetTrigger("Broken");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13 && active) //Player Layer
        {
            health = 0;
        }
    }
}
