using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Vitamin, Mineral, Dietary_Fiber, Time, MainHealth, EnemyHealth, SubTowerHealth }
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
                myText.text = string.Format("{0:#,###}", GameManager.instance.vitamin);
                break;

            case InfoType.Mineral:
                myText.text = string.Format("{0:#,###}", GameManager.instance.mineral);
                break;

            case InfoType.Dietary_Fiber:
                myText.text = string.Format("{0:#,###}", GameManager.instance.dietaryFiber);
                break;

            case InfoType.Time:

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
        }
    }

    private void OnEnable()
    {
        enemy = GetComponentInParent<Enemy>();
        subTower = GetComponentInParent<SubTower>();
    }
}
