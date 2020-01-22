using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaggotSpawn : MonoBehaviour
{
    [SerializeField] private GameObject maggotPref;

    public void SpawnMaggots()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject maggot = Instantiate(maggotPref, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), Quaternion.identity);
            StartCoroutine(TurnOnHitbox(maggot));
        }
    }

    private IEnumerator TurnOnHitbox(GameObject maggot)
    {
        yield return new WaitForSeconds(0.25f);
        maggot.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
