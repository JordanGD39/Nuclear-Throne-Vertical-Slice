using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
[SerializeField]
public class Weapon : ScriptableObject
{
    private string weaponName = "";
    public string Name { get { return weaponName; } }

    private Sprite sprite;

    public enum type { SINGLE, BURST, AUTO, AUTOBURST}
    public type weaponType;

    private Bullet bullet;
    public Bullet WeaponBullet { get { return bullet; } }

    private int damage;
    public int Damage { get { return damage; } }

    private float reloadTime = 0.5f;
    public float ReloadTime { get { return reloadTime; } }

    private int startAmmo = 20;
    public int StartAmmo { get { return startAmmo; } }

    private int ammoWaste = 1;
    public int AmmoWaste { get { return ammoWaste; } }

    private int spreadAngle = 0;
    public int SpreadAngle { get { return spreadAngle; } }
}
