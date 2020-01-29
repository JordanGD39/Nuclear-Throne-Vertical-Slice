using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpButtons : MonoBehaviour
{
    private Transform descriptionUI;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        descriptionUI = transform.GetChild(2);
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

    public void LevelUp(int i)
    {
        if (GameManager.instance.LevelUps > 0)
        {
            switch (i)
            {
                case 0:
                    GameManager.instance.ScaryLevel -= 0.2f;
                    GameManager.instance.ScaryLevels++;
                    break;
                case 1:
                    GameManager.instance.GetComponent<StatsClass>().MaxHealth++;
                    GameManager.instance.HealthLevels++;
                    break;
                case 2:
                    GameManager.instance.MovementLevels++;
                    GameManager.instance.MovementLevel++;
                    break;
                case 3:
                    GameManager.instance.BulletAmmoCap += 10;
                    GameManager.instance.OtherAmmoCap += 5;
                    GameManager.instance.AmmoLevels++;
                    break;
            }
            GameManager.instance.LevelUps--;
        }
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
