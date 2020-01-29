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
                EnemyHPreduce(enemy);
            }
            else
            {
                rand = Random.Range(0, 100);

                if (rand < 30)
                {
                    GameObject enemy = Instantiate(enemies[2], new Vector3(Random.Range(-8f, 19f), Random.Range(-26f, -5), 0), transform.rotation);
                    EnemyHPreduce(enemy);
                }
                else
                {
                    GameObject enemy = Instantiate(enemies[1], new Vector3(Random.Range(-8f, 19f), Random.Range(-26f, -5), 0), transform.rotation);
                    EnemyHPreduce(enemy);
                }
            }
        }

        //Chests spawn
        for (int i = 0; i < Random.Range(5, 8); i++)
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
                    GameObject presentChest = Instantiate(chests[2], new Vector3(Random.Range(-8f, 19f), Random.Range(-26, 2), 0), transform.rotation);
                }
                else
                {
                    rand = Random.Range(0, 100);

                    if (rand < 20)
                    {
                        rand = Random.Range(0, 100);

                        if (rand < 40)
                        {
                            GameObject largeChest = Instantiate(chests[3], new Vector3(Random.Range(-8f, 19f), Random.Range(-26, 2), 0), transform.rotation);
                        }
                        else
                        {
                            if (GameManager.instance.GetComponent<StatsClass>().Health <= GameManager.instance.GetComponent<StatsClass>().MaxHealth / 2)
                            {
                                GameObject medKit = Instantiate(chests[5], new Vector3(Random.Range(-8f, 19f), Random.Range(-26, 2), 0), transform.rotation);
                            }
                            else
                            {
                                GameObject radCanister = Instantiate(chests[4], new Vector3(Random.Range(-8f, 19f), Random.Range(-26, 2), 0), transform.rotation);
                            }
                            
                        }
                        
                    }
                    else
                    {
                        GameObject weaponChest = Instantiate(chests[1], new Vector3(Random.Range(-8f, 19f), Random.Range(-26, 2), 0), transform.rotation);
                    }                    
                }
            }
        }

        GameManager.instance.GetComponent<AllEnemiesDefeated>().CheckEnemies();
    }

    private void EnemyHPreduce(GameObject enemy)
    {
        float health = enemy.GetComponent<StatsClass>().Health;

        health *= 0.05f * GameManager.instance.Difficulty;
        health *= GameManager.instance.ScaryLevel;
        enemy.GetComponent<StatsClass>().Health += Mathf.RoundToInt(health);
    }
}
