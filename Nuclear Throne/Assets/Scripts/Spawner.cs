using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<GameObject> chests = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //Enemy spawn
        for (int i = 0; i < Random.Range(20, 30); i++)
        {
            float rand = Random.Range(0, 100);

            if (rand <= 60)
            {
                GameObject enemy = Instantiate(enemies[0], new Vector3(Random.Range(-8f, 19f), Random.Range(-26, 2), 0), transform.rotation);
                float health = enemy.GetComponent<StatsClass>().Health;

                health *= 0.05f * GameManager.instance.Difficulty;

                enemy.GetComponent<StatsClass>().Health += Mathf.RoundToInt(health);
            }
            else
            {
                rand = Random.Range(0, 100);

                if (rand < 30)
                {
                    GameObject enemy = Instantiate(enemies[2], new Vector3(Random.Range(-8f, 19f), Random.Range(-26f, -5), 0), transform.rotation);

                    float health = enemy.GetComponent<StatsClass>().Health;

                    health *= 0.05f * GameManager.instance.Difficulty;

                    enemy.GetComponent<StatsClass>().Health += Mathf.RoundToInt(health);
                }
                else
                {
                    GameObject enemy = Instantiate(enemies[1], new Vector3(Random.Range(-8f, 19f), Random.Range(-26f, -5), 0), transform.rotation);

                    float health = enemy.GetComponent<StatsClass>().Health;

                    health *= 0.05f * GameManager.instance.Difficulty;

                    enemy.GetComponent<StatsClass>().Health += Mathf.RoundToInt(health);
                }
            }
        }

        //Chests spawn
        for (int i = 0; i < Random.Range(3, 6); i++)
        {
            float rand = Random.Range(0, 100);

            if (rand < 60)
            {
                GameObject chest = Instantiate(chests[0], new Vector3(Random.Range(-8f, 19f), Random.Range(-26, 2), 0), transform.rotation);
            }
            else
            {
                rand = Random.Range(0, 100);

                if (rand < 20)
                {
                    float y = Random.Range(-26, 2);

                    if (y > -4 && y < 4)
                    {
                        y = 5;
                    }

                    GameObject chest = Instantiate(chests[2], new Vector3(Random.Range(-8f, 19f), Random.Range(-26, 2), 0), transform.rotation);
                }
                else
                {
                    

                    GameObject chest = Instantiate(chests[1], new Vector3(Random.Range(-8f, 19f), Random.Range(-26, 2), 0), transform.rotation);
                }
            }
        }

        GameManager.instance.GetComponent<AllEnemiesDefeated>().CheckEnemies();
    }
}
