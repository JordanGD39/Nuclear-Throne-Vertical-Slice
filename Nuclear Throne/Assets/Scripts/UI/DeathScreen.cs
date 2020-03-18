using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    private Transform ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = transform.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ui.gameObject.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.ResetStats();
            if (GameManager.instance.ShowedCredits)
            {
                StartCoroutine(GameManager.instance.LoadAsync(1, "LOADING..."));
            }
            else
            {
                GameManager.instance.ShowedCredits = true;
                StartCoroutine(GameManager.instance.LoadAsync(4, "LOADING..."));
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            GameManager.instance.ResetStats();
            if (GameManager.instance.ShowedCredits)
            {
                StartCoroutine(GameManager.instance.LoadAsync(0, "LOADING..."));
            }
            else
            {
                GameManager.instance.ShowedCredits = true;
                StartCoroutine(GameManager.instance.LoadAsync(4, "LOADING..."));
            }
        }
    }

    public void ShowDeathPanel()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        ui.gameObject.SetActive(true);
        ui.GetChild(1).GetComponent<Text>().text = "KILLS: " + GameManager.instance.Kills;
        ui.GetChild(2).GetComponent<Text>().text = "LEVEL: 1-" + GameManager.instance.Difficulty;
        ui.GetChild(3).GetComponent<Text>().text = "DIFFICULTY: " + GameManager.instance.Difficulty;
    }
}
