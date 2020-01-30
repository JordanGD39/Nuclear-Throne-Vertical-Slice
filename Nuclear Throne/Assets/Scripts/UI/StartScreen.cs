using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1);

        if (AudioManager.instance.CurrSound != null)
        {
            AudioManager.instance.StopPlaying(AudioManager.instance.CurrSound.name);
        }
        AudioManager.instance.Play("Main");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {            
            SceneManager.LoadScene(1);
            AudioManager.instance.StopPlaying("Main");
            AudioManager.instance.Play("Drylands");
        }
        else if (Input.GetButtonDown("Pause"))
        {
            Application.Quit();
        }
    }
}
