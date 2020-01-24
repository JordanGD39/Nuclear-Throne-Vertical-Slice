using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseFunctionality : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;

    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> weaponFunctions = new List<GameObject>();
    private LevelCheck level;

    private bool pause;
    private bool continueTroughMenu;

    private void Start()
    {
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
            }
            return;
        }

        if (!continueTroughMenu)
        {
            pause = GetPauseButton(pause);
        }
        PauseGame(pause);
    }

    private bool GetPauseButton(bool pauseState)
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (pauseState)
            {                
                return false;
            }
            else
            {
                pauseUI.transform.GetChild(1).GetChild(0).GetComponent<Button>().Select();
                pauseUI.transform.GetChild(1).GetChild(0).GetComponent<Button>().OnSelect(null);
                return true;
            }
        }

        return pauseState;
    }

    private void PauseGame(bool pauseState)
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

    public void Continue()
    {
        continueTroughMenu = true;
        pause = false;
    }

    public void Retry()
    {
        continueTroughMenu = true;
        pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        continueTroughMenu = true;
        pause = false;
        SceneManager.LoadScene(0);
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
