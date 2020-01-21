using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject textPref;
    private Transform canvas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("WorldCanvas").transform;
    }

    public void TextSpawn(string weaponName, Transform objTransform)
    {
        GameObject text = Instantiate(textPref, canvas, true);
        text.transform.position = new Vector3(objTransform.position.x, objTransform.position.y, 0);
        text.transform.localScale = new Vector3(1, 1, 1);
        text.transform.GetChild(0).GetComponent<Text>().text = weaponName;
        Destroy(text, 2.5f);
    }
}
