using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    private Animator anim;
    private Animator fade;

    private void Start()
    {
        anim = GetComponent<Animator>();
        AudioManager.instance.StopPlaying("Drylands");
        AudioManager.instance.Play("Credits");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            anim.speed = 20;
        }
        else
        {
            anim.speed = 1;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
