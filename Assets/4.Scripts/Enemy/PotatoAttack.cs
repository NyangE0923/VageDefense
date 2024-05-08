using System.Collections;
using UnityEngine;

public class PotatoAttack : MonoBehaviour
{
    [SerializeField] private float attackTime = 3f;
    Enemy enemy;
    AnimationController anim;
    Collider2D currentHit; // 현재 충돌한 Collider2D를 저장하는 변수

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<AnimationController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
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
            anim.isMoving = false;
            anim.anim.SetBool(anim.Moving, anim.isMoving);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other == currentHit)
        {
            anim.isMoving = true;
            anim.anim.SetBool(anim.Moving, anim.isMoving);
            currentHit = null;

            switch (other.tag)
            {
                case "SubTower":
                    if (gameObject.activeSelf)
                    {
                        StopAllCoroutines(); // 해당 서브타워 공격 중단
                        Debug.Log("서브타워 파괴");
                    }
                    break;
                case "MainTower":
                    if (gameObject.activeSelf)
                    {
                        StopAllCoroutines(); // 메인타워 공격 중단
                    }
                    break;
                default:
                    break;
            }

        }
    }

    IEnumerator AttackTower()
    {
        while (true)
        {
            GameManager.instance.health -= enemy.damage;
            anim.anim.SetTrigger(anim.Attack);
            Debug.Log("공격완료!");
            yield return new WaitForSeconds(attackTime);
        }
    }

    IEnumerator AttackSubTower()
    {
        while (true)
        {
            if (currentHit != null)
            {
                SubTower towerScript = currentHit.GetComponent<SubTower>();
                if (towerScript != null)
                {
                    towerScript.BeDamaged(enemy.damage);
                }
                anim.anim.SetTrigger(anim.Attack);
                Debug.Log("공격완료!");
            }
            yield return new WaitForSeconds(attackTime);
        }
    }
}
