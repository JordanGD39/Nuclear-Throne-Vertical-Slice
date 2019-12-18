using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;

    [SerializeField] private Bullet bullet;

    public bool Loaded { get; set; }

    public Weapon WeaponThatShot { get; set; }

    // Start is called before the first frame update
    void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Loaded)
        {
            rb.velocity = transform.TransformVector(Vector3.up * speed);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
