using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsClass : MonoBehaviour
{
    [SerializeField] private int health = 8;
    private int Health { get { return health; } set { health = value; } }
    [SerializeField] private Weapon primary;
    [SerializeField] private Weapon secondary;
    public Weapon Primary { get { return primary; } set { primary = value; } }
    public Weapon Secondary { get { return secondary; } set { secondary = value; } }

    public int Ammo { get; set; } = 0;
}
