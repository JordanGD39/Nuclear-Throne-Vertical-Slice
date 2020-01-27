using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject transition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(WaitBeforeNextLevel(collision));
        }
    }

    private IEnumerator WaitBeforeNextLevel(Collider2D collision)
    {
        yield return new WaitForSeconds(1);
        GameObject portal = Instantiate(transition, transform.position, Quaternion.identity);
        portal.GetComponent<Animator>().Play("PortalTransition");
        yield return new WaitForSeconds(1);
        GameManager.instance.SavePlayer(collision.GetComponent<StatsClass>());
        GameManager.instance.Difficulty++;
        GameManager.instance.GetComponent<AllEnemiesDefeated>().Done = false;
        SceneManager.LoadScene(2);
    }
}
