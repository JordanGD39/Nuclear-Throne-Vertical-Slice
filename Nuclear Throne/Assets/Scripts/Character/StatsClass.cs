﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsClass : MonoBehaviour
{
    [SerializeField] private int health = 8;
    public int Health { get { return health; } set { health = value; } }

    [SerializeField] private int maxHealth = 8;
    public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    [SerializeField] private int level = 1;
    public int Level { get { return level; } set { level = value; } }
    public int Rads { get; set; } = 0;
    [SerializeField] private Weapon primary;
    [SerializeField] private Weapon secondary;
    public Weapon Primary { get { return primary; } set { primary = value; } }
    public Weapon Secondary { get { return secondary; } set { secondary = value; } }

    public int Ammo { get; set; } = 120;
    public int ShellAmmo { get; set; } = 0;
    public int BoltAmmo { get; set; } = 0;
    public int ExplosiveAmmo { get; set; } = 0;
    public int EnergyAmmo { get; set; } = 0;
}
