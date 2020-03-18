using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseFunctionality : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    private SpriteRenderer portalTransition;

    private GameObject settingsUI;

    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> weaponFunctions = new List<GameObject>();
    private LevelCheck level;

    private bool pause;
    private bool continueTroughMenu;

    private void Start()
    {
        if (pauseUI != null)
        {
            settingsUI = pauseUI.transform.GetChild(3).gameObject;
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            portalTransition = GameObject.FindGameObjectWithTag("PortalTransition").GetComponent<SpriteRenderer>();
        }        
        level = GameManager.instance.GetComponent<LevelCheck>();
        players = level.FindObjectsOnLayer(13); //Player Layer
        weaponFunctions = level.FindObjectsWithNameWithoutLayer("WeaponRotation", 10);        
    }

    private void Update()
    {
        if (pauseUI == null)
        {
            if (SceneManager.GetActiveScene().buildIndex > 0)
            {
                pauseUI = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).gameObject;
                settingsUI = pauseUI.transform.GetChild(3).gameObject;
            }
            return;
        }

        if (!continueTroughMenu)
        {
            if (portalTransition != null && !portalTransition.enabled || portalTransition == null)
            {
                pause = GetPauseButton(pause);
            }            
        }
        PauseGame(pause);

        if (settingsUI.activeSelf && Input.GetButtonDown("Pause"))
        {
            settingsUI.SetActive(false);
            pauseUI.transform.GetChild(1).GetChild(3).GetComponent<Button>().Select();
            pauseUI.transform.GetChild(1).GetChild(3).GetComponent<Button>().OnSelect(null);
        }
    }

    private bool GetPauseButton(bool pauseState)
    {
        if (Input.GetButtonDown("Pause") && !settingsUI.activeSelf)
        {
            if (pauseState)
            {                
                return false;
            }
            else
            {
                pauseUI.transform.GetChild(1).GetChild(0).GetComponent<Button>().Select();
                pauseUI.transform.GetChild(1).GetChild(0).GetComponent<Button>().OnSelect(null);
                pauseUI.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "1-" + GameManager.instance.Difficulty;
                pauseUI.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = GameManager.instance.Kills.ToString();
                return true;
            }
        }

        return pauseState;
    }

    private void PauseGame(bool pauseState)
    {
        if (!settingsUI.activeSelf)
        {
            if (pauseState)
            {
                Time.timeScale = 0;
                ChangeComponentAvailability(false);
            }
            else
            {
                Time.timeScale = 1;
                ChangeComponentAvailability(true);
                continueTroughMenu = false;
            }
        }
    }

    public void Continue()
    {
        continueTroughMenu = true;
        pause = false;
    }

    public void Retry()
    {
        continueTroughMenu = true;
        pause = false;
        GameManager.instance.ResetStats();
        StartCoroutine(GameManager.instance.LoadAsync(1, "LOADING..."));
    }

    public void Menu()
    {
        continueTroughMenu = true;
        pause = false;
        GameManager.instance.ResetStats();
        StartCoroutine(GameManager.instance.LoadAsync(0, "LOADING..."));
    }

    public void Settings(Slider slider)
    {
        slider.value = PlayerPrefs.GetFloat("volume", 1);
        settingsUI.SetActive(true);
    }

    public void VolumeChange(Slider slider)
    {
        PlayerPrefs.SetFloat("volume", slider.value);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    private void ChangeComponentAvailability(bool set)
    {
        pauseUI.SetActive(!set);

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                if (players[i].transform.parent.gameObject.layer == 0) //Default Layer
                {
                    players[i].GetComponent<PlayerMovement>().enabled = set;
                    players[i].GetComponent<Roll>().enabled = set;
                }
                else
                {
                    if (players[i].GetComponent<SecondaryWeaponSpriteChanger>() != null)
                    {
                        players[i].GetComponent<SecondaryWeaponSpriteChanger>().enabled = set;
                    }
                }
            }
        }

        for (int i = 0; i < weaponFunctions.Count; i++)
        {
            if (weaponFunctions[i] != null)
            {
                weaponFunctions[i].GetComponentInChildren<WeaponFunctions>().enabled = set;
                weaponFunctions[i].GetComponentInChildren<ShootGun>().enabled = set;
                weaponFunctions[i].GetComponent<WeaponRotation>().enabled = set;
            }
        }
    }
}
