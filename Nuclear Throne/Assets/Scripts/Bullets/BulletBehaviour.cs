using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] private Bullet bullet;
    public Bullet BulletFired { get { return bullet; } set { bullet = value; } }

    public bool Loaded { get; set; }
    public bool PlayerControl { get; set; }

    public Weapon WeaponThatShot { get; set; }

    private int hits = 0;

    private int wallHitCounter;
    private bool wallHit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (bullet.fireType != Bullet.type.NORMAL)
        {
            Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            GetComponent<BoxCollider2D>().size = S;
        }

        wallHitCounter = 0;
        wallHit = false;

        if (Loaded)
        {
            rb.velocity = transform.TransformVector(Vector3.up * speed);
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bullet.Hits == 1)
        {
            if (collision.gameObject.layer == 8)
            {
                WallHitSequence(collision, wallHitCounter, wallHit);
                wallHit = true;
                StartCoroutine(WallHitCoroutine());
            }
            else if (PlayerControl && collision.CompareTag("Enemy") && !collision.GetComponent<EnemyAi>().Dead)
            {
                collision.GetComponent<EnemyAi>().Hit(WeaponThatShot.Damage, rb.velocity);
                if (bullet.Dissapear)
                {
                    Destroy(gameObject);
                }
            }
            else if (!PlayerControl && collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().Hit(WeaponThatShot.Damage, rb.velocity, true);
                if (bullet.Dissapear)
                {
                    Debug.Log("WHYYY");
                    Destroy(gameObject);
                }
            }
            else
            {
                if (!collision.CompareTag("Bullet") && !collision.CompareTag("Player") && !collision.CompareTag("Enemy"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void WallHitSequence(Collider2D col, int counter, bool hit)
    {
        if (counter < bullet.WallHits && !hit)
        {
            float getRectX = 0.5f;// col.gameObject.GetComponent<SpriteRenderer>().sprite.rect.x / 2;
            float getRectY = 0.5f;// col.gameObject.GetComponent<SpriteRenderer>().sprite.rect.y / 2;

            if ((transform.position.x > (col.gameObject.transform.position.x + getRectX)) ||
                (transform.position.x < (col.gameObject.transform.position.x - getRectX)))
            {
                rb.velocity *= new Vector2(-1.0f, 1.0f);
            }
            else if ((transform.position.y > (col.gameObject.transform.position.y + getRectY)) ||
                     (transform.position.y < (col.gameObject.transform.position.y - getRectY)))
            {
                rb.velocity *= new Vector2(1.0f, -1.0f);
            }

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -transform.rotation.eulerAngles.z);
            wallHitCounter++;
        }
        else if (counter >= bullet.WallHits && !hit)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator WallHitCoroutine()
    {
        yield return new WaitForSeconds(0.0005f);

        wallHit = false;
    }
}
