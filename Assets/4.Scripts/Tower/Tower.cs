using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    AnimationController anim;

    private void Awake()
    {
        anim = GetComponent<AnimationController>();
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

    public void Attacking()
    {
        anim.anim.SetTrigger(anim.Attack);
    }

    public void Idle()
    {
        anim.anim.SetTrigger(anim.Idle);
    }

    public void Die()
    {
        AudioManager.instance.PlaySfx(AudioManager.sfx.TowerDisable);
        gameObject.SetActive(false); // 타워를 비활성화
    }

}
