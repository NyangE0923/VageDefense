using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using static CreateTower;

public class SubTower : MonoBehaviour
{
    public delegate void DamageDelegate(float _damage);
    public static DamageDelegate Damage;

    Tower tower;

    [Header("TowerInfo")]
    public float damage;
    public float maxHealth;
    public float currentHealth;
    public int vitamin;
    public int mineral;
    public int dietaryFiber;

    private void Start()
    {
        currentHealth = maxHealth;
        Damage += BeDamaged;
        tower = GetComponent<Tower>();
    }
    public void BeDamaged(float _damage)
    {
        currentHealth -= _damage;

        if (currentHealth <= 0)
        {
            tower.Die();
        }
    }

    public void Init(TowerStats data)
    {
        damage = data.damage;
        maxHealth = data.maxHealth;
        vitamin = data.vitamin;
        mineral = data.mineral;
        dietaryFiber = data.dietaryFiber;
    }

    private void OnEnable()
    {
        //리스트에 여러가지 태그를 담는 방법
        currentHealth = maxHealth;

    }
}
