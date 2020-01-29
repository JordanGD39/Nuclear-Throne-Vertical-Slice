using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PowerUpButtons : MonoBehaviour
{
    private Transform descriptionUI;
    private Text mutationText;
    private int index;

    private bool loading = false;

    // Start is called before the first frame update
    void Start()
    {
        mutationText = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(2).GetComponent<Text>();
        UpdateMutation();
        descriptionUI = transform.GetChild(2);
    }

    private IEnumerator LoadAsync(int scene)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);

        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            mutationText.text = "GENERATING... " + (progress * 100).ToString("F0") + "%";
            yield return null;
        }
    }

    public void GetIndex(int i)
    {
        index = i;
    }

    public void DesShow(bool enter)
    {
        if (enter)
        {
            descriptionUI.GetChild(index).gameObject.SetActive(true);

            int level = GetStat();

            descriptionUI.GetChild(index).GetChild(2).GetComponent<Text>().text = "LV." + level;
        }
        else
        {
            descriptionUI.GetChild(index).gameObject.SetActive(false);
        }
    }

    public void LevelUp()
    {
        if (GameManager.instance.LevelUps > 0)
        {
            switch (index)
            {
                case 0:
                    GameManager.instance.ScaryLevel -= 0.08f;
                    GameManager.instance.ScaryLevels++;
                    break;
                case 1:
                    GameManager.instance.GetComponent<StatsClass>().MaxHealth++;
                    GameManager.instance.GetComponent<StatsClass>().Health = GameManager.instance.GetComponent<StatsClass>().MaxHealth;
                    GameManager.instance.HealthLevels++;
                    break;
                case 2:
                    GameManager.instance.MovementLevels++;
                    GameManager.instance.MovementLevel += 0.5f;
                    break;
                case 3:
                    GameManager.instance.BulletAmmoCap += 75;
                    if (GameManager.instance.BulletAmmoCap > 999)
                    {
                        GameManager.instance.BulletAmmoCap = 999;
                    }
                    GameManager.instance.OtherAmmoCap += 4;
                    GameManager.instance.AmmoLevels++;
                    if (GameManager.instance.AmmoLevels == 10)
                    {
                        GameManager.instance.OtherAmmoCap = 99;
                    }
                    break;
            }
            GameManager.instance.LevelUps--;
            UpdateMutation();

            int level = GetStat();

            descriptionUI.GetChild(index).GetChild(2).GetComponent<Text>().text = "LV." + level;

            if (GameManager.instance.LevelUps == 0)
            {
                mutationText.transform.position = Vector3.zero;
                descriptionUI.parent.GetChild(1).gameObject.SetActive(false);
                descriptionUI.gameObject.SetActive(false);
                loading = true;
                StartCoroutine(LoadAsync(2));
            }
        }
    }

    private void UpdateMutation()
    {
        string s = "";
        if (GameManager.instance.LevelUps > 1)
        {
            s = "S";
        }
        mutationText.text = "LEVEL UP!\n SELECT " + GameManager.instance.LevelUps + " MUTATION" + s;
    }

    private int GetStat()
    {
        int level = 0;

        switch (index)
        {
            case 0:
                level = GameManager.instance.ScaryLevels;
                break;
            case 1:
                level = GameManager.instance.HealthLevels;
                break;
            case 2:
                level = GameManager.instance.MovementLevels;
                break;
            case 3:
                level = GameManager.instance.AmmoLevels;
                break;
        }

        return level;
    }
}
