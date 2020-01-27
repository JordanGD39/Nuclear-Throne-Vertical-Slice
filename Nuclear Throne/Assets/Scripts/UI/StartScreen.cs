using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    private void Start()
    {
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
            AudioManager.instance.StopPlaying(AudioManager.instance.CurrSound.name);
            AudioManager.instance.Play("Drylands");
        }
    }
}
