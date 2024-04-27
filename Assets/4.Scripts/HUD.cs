using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Vitamin, Mineral, Dietary_Fiber, Time, MainHealth, EnemyHealth, TowerHealth }
    public InfoType type;

    TMP_Text myText;
    Slider mySlider;
    Enemy enemy;

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
                myText.text = string.Format("{0:#,###}", GameManager.Instance.vitamin);
                break;

            case InfoType.Mineral:
                myText.text = string.Format("{0:#,###}", GameManager.Instance.mineral);
                break;

            case InfoType.Dietary_Fiber:
                myText.text = string.Format("{0:#,###}", GameManager.Instance.dietaryFiber);
                break;

            case InfoType.Time:

                break;

            case InfoType.MainHealth:
                float curHealth = GameManager.Instance.health;
                float maxHealth = GameManager.Instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;

            case InfoType.EnemyHealth:
                float EnemycurHealth = enemy.health;
                float EnemymaxHealth = enemy.maxHealth;
                mySlider.value = EnemycurHealth / EnemymaxHealth;
                break;
        }
    }

    private void OnEnable()
    {
        enemy = GetComponentInParent<Enemy>();
    }
}
