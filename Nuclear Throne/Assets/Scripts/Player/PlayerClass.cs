using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerClass : MonoBehaviour
{
    private int health = 8;
    private int Health { get { return health; } set { health = value; } }
    private Weapon primary;
    private Weapon secondary;
    public Weapon Primary { get { return primary; } set { primary = value; } }
    public Weapon Secondary { get { return secondary; } set { secondary = value; } }

    public int Ammo { get; set; } = 0;
}
