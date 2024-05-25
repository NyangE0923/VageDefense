using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CreateTower;

public class HUD : MonoBehaviour
{
    public enum InfoType { Vitamin, Mineral, Dietary_Fiber, Time, MainHealth, EnemyHealth, 
    SubTowerHealth, SpamPriceVitamin, SpamPriceMineral, CornPriceVitamin, CornPriceMineral,
    Level}
    public InfoType type;

    TMP_Text myText;
    Slider mySlider;
    Enemy enemy;
    SubTower subTower;

    private void Awake()
    {
        myText = GetComponent<TMP_Text>();
        mySlider = GetComponent<Slider>();
    }


    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Vitamin:
                int vitaminValue = GameManager.instance.vitamin;
                myText.text = (vitaminValue <= 0) ? "0" : string.Format("{0:#,###}", vitaminValue);
                break;

            case InfoType.Mineral:
                int mineralValue = GameManager.instance.mineral;
                myText.text = (mineralValue <= 0) ? "0" : string.Format("{0:#,###}", mineralValue);
                break;

            case InfoType.Dietary_Fiber:
                int fiberValue = GameManager.instance.dietaryFiber;
                myText.text = (fiberValue <= 0) ? "0" : string.Format("{0:#,###}", fiberValue);
                break;

            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("남은 시간 : " + "{0:D2}:{1:D2}", min, sec);

                break;

            case InfoType.MainHealth:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;

            case InfoType.EnemyHealth:
                float EnemycurHealth = enemy.health;
                float EnemymaxHealth = enemy.maxHealth;
                mySlider.value = EnemycurHealth / EnemymaxHealth;
                break;

            case InfoType.SubTowerHealth:
                float SubTowercurHealth = subTower.currentHealth;
                float SubTowermaxHealth = subTower.maxHealth;
                mySlider.value = SubTowercurHealth / SubTowermaxHealth;
                break;
            case InfoType.SpamPriceVitamin:
                PriceVitamin(0);
                break;
            case InfoType.SpamPriceMineral:
                PriceMineral(0);
                break;
            case InfoType.CornPriceVitamin:
                PriceVitamin(1);
                break;
            case InfoType.CornPriceMineral:
                PriceMineral(1);
                break;
            case InfoType.Level:
                int _Level = GameManager.instance.spawner.level;
                myText.text = (_Level <= 0) ? "Lv : " + "0" : string.Format("Lv : "+"{0:F0}", _Level);
                break;
        }
    }

    private void PriceMineral(int TowerStatsNumber)
    {
        int mineralValue = GameManager.instance.towerStats[TowerStatsNumber].mineral;
        string formattedMineral;

        if (mineralValue <= 0)
        {
            formattedMineral = "0";
        }
        else
        {
            formattedMineral = string.Format("{0:#,###}", mineralValue);
        }
        myText.text = formattedMineral;
    }
    private void PriceVitamin(int TowerStatsNumber)
    {
        int vitaminValue = GameManager.instance.towerStats[TowerStatsNumber].vitamin;
        string formattedVitamin;

        if (vitaminValue <= 0)
        {
            formattedVitamin = "0";
        }
        else
        {
            formattedVitamin = string.Format("{0:#,###}", vitaminValue);
        }
        myText.text = formattedVitamin;
    }

    private void OnEnable()
    {
        enemy = GetComponentInParent<Enemy>();
        subTower = GetComponentInParent<SubTower>();
    }
}
