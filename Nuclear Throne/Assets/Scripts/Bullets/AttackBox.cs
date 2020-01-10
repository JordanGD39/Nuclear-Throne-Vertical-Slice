using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    protected Rigidbody2D rb;

    public bool Loaded { get; set; }

    [SerializeField] protected Bullet bullet;
    public Bullet BulletFired { get { return bullet; } set { bullet = value; } }

    public Weapon WeaponThatShot { get; set; }

    virtual protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (bullet.fireType != Bullet.type.NORMAL)
        {
            Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            GetComponent<BoxCollider2D>().size = S;
        }
    }
}
