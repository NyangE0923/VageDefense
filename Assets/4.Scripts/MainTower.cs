using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    AnimationController anim;

    private void Awake()
    {
        anim = GetComponent<AnimationController>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
    #region Tower List Delete
    public event Action<Transform> OnTowerDisabled;
    private void HandleTowerDisabled()
    {
        OnTowerDisabled?.Invoke(transform);
    }
    private void OnDisable()
    {
        HandleTowerDisabled();
    }
    #endregion
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Attacking()
    {
        anim.anim.SetTrigger(anim.Attack);
    }

    public void Idle()
    {
        anim.anim.SetTrigger(anim.Idle);
    }

    private void Die()
    {
        gameObject.SetActive(false); // 타워를 비활성화
    }
}
