using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllEnemiesDefeated : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject explosion;

    public List<GameObject> Enemies { get; set; }
    private LevelCheck level;

    private Vector2 mortemPlace;
    private bool enemyDeath;

    public Vector2 Place { get { return mortemPlace; } set { mortemPlace = value; } }
    public bool EnemyDeath { get { return enemyDeath; } set { enemyDeath = value; } }

    public bool Done { get; set; }

    private void Start()
    {
        enemyDeath = false;
    }

    private void Update()
    {
        CheckEnemyDeaths(Enemies, enemyDeath);
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

            if (counter >= objList.Count && !Done)
            {
                StartCoroutine(WaitCoroutine(portal, mortemPlace));
                Done = true;
            }
        }

        enemyDeath = false;
    }

    private IEnumerator WaitCoroutine(GameObject paste, Vector2 place)
    {
        yield return new WaitForSeconds(1.2f);

        GameObject explodeObject = Instantiate(explosion, place, Quaternion.identity);
        explodeObject.transform.GetChild(0).GetComponent<ExplosionScript>().PlayerDamage = false;

        yield return new WaitForSeconds(0.8f);

        GameObject hole = Instantiate(paste, place, Quaternion.identity);        
    }

    public void CheckEnemies()
    {
        level = GetComponent<LevelCheck>();
        Enemies = level.FindObjectsOnLayerWithTag(10, "Enemy"); //Enemy Layer
    }
}
