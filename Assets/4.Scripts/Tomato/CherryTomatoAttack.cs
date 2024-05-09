using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryTomatoAttack : EnemyAttack
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            currentHit = other; // 현재 충돌한 Collider2D를 저장
            switch (other.tag)
            {
                case "SubTower":
                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(AttackSubTower());
                        Debug.Log("서브타워 발견");
                    }
                    break;
                case "MainTower":
                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(AttackTower());
                    }
                    break;
                default:
                    break;
            }
        }
    }
    protected override void OnTriggerExit2D(Collider2D other)
    {
    }



    IEnumerator AttackTower()
    {
        anim.anim.SetTrigger(anim.Attack);
        yield return new WaitForSeconds(attackTime);
        GameManager.instance.health -= enemy.damage;
        gameObject.SetActive(false);
    }

    IEnumerator AttackSubTower()
    {
        if (currentHit != null)
        {
            anim.anim.SetTrigger(anim.Attack);
            yield return new WaitForSeconds(attackTime);

            SubTower towerScript = currentHit.GetComponent<SubTower>();
            if (towerScript != null)
            {
                towerScript.BeDamaged(enemy.damage);
            }
            gameObject.SetActive(false);
        }
    }
}
