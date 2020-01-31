using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject textPref;
    [SerializeField] private GameObject loadTextPref;
    public int Difficulty { get; set; } = 1;
    public int Kills { get; set; }

    public bool ShowedCredits { get; set; } = false;

    public float MovementLevel { get; set; } = 5;
    public float ScaryLevel { get; set; } = 1;
    public int BulletAmmoCap { get; set; } = 255;
    public int OtherAmmoCap { get; set; } = 55;

    //Count of level-ups this got
    public int MovementLevels { get; set; } = 0;
    public int ScaryLevels { get; set; } = 0;
    public int AmmoLevels { get; set; } = 0;
    public int HealthLevels { get; set; } = 0;

    public int LevelUps { get; set; } = 0;

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
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            canvas = GameObject.FindGameObjectWithTag("WorldCanvas").transform;
        }            
    }

    public void TextSpawn(string textToSpawn, Transform objTransform)
    {
        if (canvas == null)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
            {
                canvas = GameObject.FindGameObjectWithTag("WorldCanvas").transform;
            }
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
        stats.MaxHealth = player.MaxHealth;
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
        player.MaxHealth = stats.MaxHealth;
        player.GetComponent<PlayerMovement>().Speed = MovementLevel;
    }

    public void ResetStats()
    {
        MovementLevels = 0;
        ScaryLevels = 0;
        AmmoLevels = 0;
        GetComponent<StatsClass>().MaxHealth = 8;
        BulletAmmoCap = 255;
        OtherAmmoCap = 55;
        ScaryLevel = 1;
        MovementLevel = 5;
        HealthLevels = 0;
        LevelUps = 0;
        Difficulty = 1;
        Kills = 0;
    }

    public IEnumerator LoadAsync(int scene, string textToShow)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);

        Transform canvasTransform;

        if (GameObject.FindGameObjectWithTag("BarCanvas") != null)
        {
            canvasTransform = GameObject.FindGameObjectWithTag("BarCanvas").transform;
        }
        else
        {
            canvasTransform = GameObject.FindGameObjectWithTag("Canvas").transform;
        }
         

        GameObject textGameObject = Instantiate(loadTextPref, canvasTransform, false);
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            textGameObject.GetComponent<Image>().enabled = false;
        }
        textGameObject.transform.position = Vector3.zero;
        Text text = textGameObject.transform.GetChild(0).GetComponent<Text>();

        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            text.text = textToShow + (progress * 100).ToString("F0") + "%";
            yield return null;
            textGameObject.transform.position = Vector3.zero;
        }
    }
}
