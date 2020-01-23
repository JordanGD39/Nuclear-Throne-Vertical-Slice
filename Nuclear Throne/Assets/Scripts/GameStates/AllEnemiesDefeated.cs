using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemiesDefeated : MonoBehaviour
{
    [SerializeField] private GameObject portal;

    private List<GameObject> enemies;
    private LevelCheck level;

    private Vector2 mortemPlace;
    private bool enemyDeath;

    public Vector2 Place { get { return mortemPlace; } set { mortemPlace = value; } }
    public bool EnemyDeath { get { return enemyDeath; } set { enemyDeath = value; } }

    private bool done = false;

    private void Start()
    {
        level = GetComponent<LevelCheck>();
        enemies = level.FindObjectsOnLayerWithTag(10, "Enemy"); //Enemy Layer
        enemyDeath = false;
    }

    private void Update()
    {
        CheckEnemyDeaths(enemies, enemyDeath);
    }

    private void CheckEnemyDeaths(List<GameObject> objList, bool died)
    {
        if (died)
        {
            int counter = 0;

            for (int i = 0; i < objList.Count; i++)
            {
                if (objList[i] != null && objList[i].GetComponent<EnemyAi>() != null)
                {
                    if (objList[i].GetComponent<EnemyAi>().Dead)
                    {
                        counter++;
                    }
                }
            }

            if (counter >= objList.Count && !done)
            {
                StartCoroutine(WaitCoroutine(portal, mortemPlace));
                done = true;
            }
        }

        enemyDeath = false;
    }

    IEnumerator WaitCoroutine(GameObject paste, Vector2 place)
    {
        yield return new WaitForSeconds(3.0f);

        GameObject hole = Instantiate(paste, place, Quaternion.Euler(0.0f, 0.0f, 0.0f));        
    }
}
