using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsClass : MonoBehaviour
{
    [SerializeField] private int health = 8;
    public int Health { get { return health; } set { health = value; } }
    [SerializeField] private Weapon primary;
    [SerializeField] private Weapon secondary;
    public Weapon Primary { get { return primary; } set { primary = value; } }
    public Weapon Secondary { get { return secondary; } set { secondary = value; } }

    public int Ammo { get; set; } = 90;
    public int ShellAmmo { get; set; } = 90;
    public int BoltAmmo { get; set; } = 0;
    public int ExplosiveAmmo { get; set; } = 0;
    public int EnergyAmmo { get; set; } = 0;
}
