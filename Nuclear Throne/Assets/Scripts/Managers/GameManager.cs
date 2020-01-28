using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject textPref;
    public int Difficulty { get; set; }
    public int Kills { get; set; }

    private Transform canvas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("WorldCanvas").transform;    
    }

    public void TextSpawn(string textToSpawn, Transform objTransform)
    {
        if (canvas == null)
        {
            canvas = GameObject.FindGameObjectWithTag("WorldCanvas").transform;
        }

        GameObject text = Instantiate(textPref, canvas, true);
        text.transform.position = new Vector3(objTransform.position.x, objTransform.position.y, 0);
        text.transform.localScale = new Vector3(1, 1, 1);
        text.transform.GetChild(0).GetComponent<Text>().text = textToSpawn;
        Destroy(text, 2.5f);
    }

    public void SavePlayer(StatsClass player)
    {
        StatsClass stats = GetComponent<StatsClass>();
        stats.Primary = player.Primary;
        stats.Secondary = player.Secondary;
        stats.Ammo = player.Ammo;
        stats.ShellAmmo = player.ShellAmmo;
        stats.BoltAmmo = player.BoltAmmo;
        stats.EnergyAmmo = player.EnergyAmmo;
        stats.ExplosiveAmmo = player.ExplosiveAmmo;
        stats.Level = player.Level;
        stats.Rads = player.Rads;
        stats.Health = player.Health;
    }

    public void LoadPlayer(StatsClass player)
    {
        StatsClass stats = GetComponent<StatsClass>();
        player.Primary = stats.Primary;
        player.Secondary = stats.Secondary;
        player.Ammo = stats.Ammo;
        player.ShellAmmo = stats.ShellAmmo;
        player.BoltAmmo = stats.BoltAmmo;
        player.EnergyAmmo = stats.EnergyAmmo;
        player.ExplosiveAmmo = stats.ExplosiveAmmo;
        player.Level = stats.Level;
        player.Rads = stats.Rads;
        player.Health = stats.Health;
    }
}
