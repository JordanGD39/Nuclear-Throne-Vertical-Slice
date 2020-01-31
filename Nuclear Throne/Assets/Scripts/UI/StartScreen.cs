using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    private GameObject controlsPanel;

    private bool loading = false;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1);

        controlsPanel = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).gameObject;

        if (AudioManager.instance.CurrSound != null)
        {
            AudioManager.instance.StopPlaying(AudioManager.instance.CurrSound.name);
        }
        AudioManager.instance.Play("Main");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetButtonDown("Pause") && !controlsPanel.activeSelf)
            {
                Application.Quit();
            }
            else
            {
                if (controlsPanel.activeSelf && !loading)
                {
                    loading = true;
                    StartCoroutine(GameManager.instance.LoadAsync(1, "LOADING..."));
                    AudioManager.instance.StopPlaying("Main");
                    AudioManager.instance.Play("Drylands");
                }
                else
                {
                    controlsPanel.SetActive(true);
                }
            }            
        }        
    }
}
