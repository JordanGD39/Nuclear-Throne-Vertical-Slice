using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    public enum type { NORMAL, FIRE, LASER, PLASMA, LIGHTNING, EXPLOSION, MISSILE, SEEKER, DISC, SLUG, SHELL, BOLT, MELEE }
    public type fireType;

    [SerializeField] private Sprite sprite;
    public Sprite SpriteOfBullet { get { return sprite; } }

    [SerializeField] private int hits = 1;
    public int Hits { get { return hits; } }

    [SerializeField] private float speed = 20;
    public float Speed { get { return speed; } }

    [SerializeField] private bool dissapear = false;
    public bool Dissapear { get { return dissapear; } }

    [SerializeField] private bool explode = false;
    public bool Explode { get { return explode; } }

    [SerializeField] private bool playerDamage = false;
    public bool PlayerDamage { get { return playerDamage; } }
}
