using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
[SerializeField]
public class Bullet : ScriptableObject
{
    public enum type { NORMAL, FIRE, LASER, PLASMA, LIGHTNING, EXPLOSION, MISSILE, SEEKER, DISC }
    public type fireType;

    private int hits = 1;
    public int Hits { get { return hits; } }

    private bool dissapear = false;
    public bool Dissapear { get { return dissapear; } }
}
