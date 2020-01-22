using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannister : MonoBehaviour
{
    const float MAX_RADIUS = 5.0f;

    [SerializeField] private GameObject pickUpItem;
    [SerializeField] private int itemAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 15) //Player or Bullet Layer
        {
            for (int i = 0; i < itemAmount; i++)
            {
                GameObject obj = Instantiate(pickUpItem, transform.position, transform.rotation);
                float randomAngle = Random.Range(0, (2 + Mathf.PI));

                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) *
                                                         Random.Range(-MAX_RADIUS, MAX_RADIUS) * 80.0f);
            }

            Destroy(gameObject);
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 15) //Bullet Layer
        {
            for (int i = 0; i < itemAmount; i++)
            {
                GameObject obj = Instantiate(pickUpItem, transform.position, transform.rotation);
                float randomAngle = Random.Range(0, (2 + Mathf.PI));

                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) *
                                                         Random.Range(-MAX_RADIUS, MAX_RADIUS) * 80.0f);
            }

            Destroy(gameObject);
        }
    }*/
}
