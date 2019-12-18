﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class Bullet : ScriptableObject
{
    public enum type { NORMAL, FIRE, LASER, PLASMA, LIGHTNING, EXPLOSION, MISSILE, SEEKER, DISC, SLUG, SHELL, BOLT }
    public type fireType;

    [SerializeField] private int hits = 1;
    public int Hits { get { return hits; } set { hits = value; } }

    [SerializeField] private bool dissapear = false;
    public bool Dissapear { get { return dissapear; } }
}
