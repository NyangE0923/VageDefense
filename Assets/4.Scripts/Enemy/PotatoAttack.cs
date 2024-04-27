using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PotatoAttack : MonoBehaviour
{
    //�̵��������� �̹� ����Ÿ������ �ɾ�� �ϹǷ� �����ֱ�� ���ݾִϸ��̼� ����ֱ� ������ �����ϸ� �� �� ����.
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
        if(hit != null) //��ü�� ������ Ȯ��
        {
            if(hit.CompareTag("MainTower")) //�ش� ��ü�� MainTower�� ������ �ִٸ�
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
            Debug.Log("���ݿϷ�!");
            yield return new WaitForSeconds(attackTime);
        }
    }
}
