using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.PlayerSaved = collision.GetComponent<StatsClass>();
            GameManager.instance.GetComponent<StatsClass>().Primary = GameManager.instance.PlayerSaved.Primary;
            GameManager.instance.GetComponent<StatsClass>().Secondary = GameManager.instance.PlayerSaved.Secondary;
            GameManager.instance.GetComponent<StatsClass>().Ammo = GameManager.instance.PlayerSaved.Ammo;
            GameManager.instance.GetComponent<StatsClass>().ShellAmmo = GameManager.instance.PlayerSaved.ShellAmmo;
            GameManager.instance.GetComponent<StatsClass>().BoltAmmo = GameManager.instance.PlayerSaved.BoltAmmo;
            GameManager.instance.GetComponent<StatsClass>().EnergyAmmo = GameManager.instance.PlayerSaved.EnergyAmmo;
            GameManager.instance.GetComponent<StatsClass>().ExplosiveAmmo = GameManager.instance.PlayerSaved.ExplosiveAmmo;
            GameManager.instance.GetComponent<StatsClass>().Level = GameManager.instance.PlayerSaved.Level;
            GameManager.instance.GetComponent<StatsClass>().Rads = GameManager.instance.PlayerSaved.Rads;
            GameManager.instance.GetComponent<StatsClass>().Health = GameManager.instance.PlayerSaved.Health;
            GameManager.instance.Difficulty++;
            GameManager.instance.GetComponent<AllEnemiesDefeated>().Done = false;
            SceneManager.LoadScene(2);
        }
    }
}
