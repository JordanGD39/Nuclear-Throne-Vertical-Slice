using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 16)
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            float distanceX = transform.position.x - collision.transform.position.x;
            float distanceY = transform.position.y - collision.transform.position.y;

            float distance = Vector2.Distance(transform.position, collision.transform.position);

            int dmg = 10;

            if (distance > 0.6f)
            {
                dmg = 5;
            }

            collision.GetComponent<Player>().Hit(dmg, new Vector2(-distanceX, -distanceY), true);
        }
        else if (collision.CompareTag("Enemy"))
        {
            float distanceX = transform.position.x - collision.transform.position.x;
            float distanceY = transform.position.y - collision.transform.position.y;

            float distance = Vector2.Distance(transform.position, collision.transform.position);

            int dmg = 10;

            if (distance > 0.6f)
            {
                dmg = 5;
            }

            collision.GetComponent<EnemyAi>().Hit(dmg, new Vector2(-distanceX, -distanceY));
        }
    }
}
