using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject transition;
    private StatsClass player;

    private bool makingPortal = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Roll>().CantRoll = true;
            if (!makingPortal)
            {
                StartCoroutine(WaitBeforeNextLevel(collision));
            }            
        }
    }

    private IEnumerator WaitBeforeNextLevel(Collider2D collision)
    {
        makingPortal = true;
        yield return new WaitForSeconds(1);
        GameObject portal = Instantiate(transition, transform.position, Quaternion.identity);
        portal.GetComponent<Animator>().Play("PortalTransition");
        yield return new WaitForSeconds(1);
        GameManager.instance.SavePlayer(collision.GetComponent<StatsClass>());
        GameManager.instance.Difficulty++;
        GameManager.instance.GetComponent<AllEnemiesDefeated>().Done = false;
        if (GameManager.instance.LevelUps > 0)
        {
            AudioManager.instance.Volume("Drylands", 0.2f);
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(2);
        }        
    }
}
