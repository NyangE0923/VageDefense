using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PotatoAttack : MonoBehaviour
{
    //이동로직으로 이미 메인타워까지 걸어가긴 하므로 공격주기와 공격애니메이션 재생주기 공격을 구현하면 될 것 같다.
    [SerializeField] private float attackTime = 3f;
    Enemy enemy;

    AnimationController anim;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<AnimationController>();
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit != null) //객체가 담겼는지 확인
        {
            if(hit.CompareTag("MainTower")) //해당 객체가 MainTower를 가지고 있다면
            {
                if(gameObject.activeSelf)
                {
                    StartCoroutine("AttackTower");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.GetComponent<MainTower>() != null)
            {
                StopCoroutine("AttackTower");
                anim.isMoving = true;
                anim.anim.SetBool(anim.Moving, anim.isMoving);
            }
        }
    }

    IEnumerator AttackTower()
    {
        anim.isAttacking = true;
        anim.isMoving = false;
        while (anim.isAttacking)
        {
            GameManager.Instance.health -= enemy.damage;
            anim.anim.SetTrigger(anim.Attack);
            Debug.Log("공격완료!");
            yield return new WaitForSeconds(attackTime);
        }
    }
}
