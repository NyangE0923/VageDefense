using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isAttacking = false;
    public float attackTime = 3f;
    public Enemy enemy;
    public AnimationController anim;
    public Collider2D currentHit; // 현재 충돌한 Collider2D를 저장하는 변수

    protected virtual void Start()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<AnimationController>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            currentHit = other; // 현재 충돌한 Collider2D를 저장
            switch (other.tag)
            {
                case "SubTower":
                    if (gameObject.activeSelf)
                    {
                        if (!isAttacking)
                        {
                            isAttacking = true;
                            anim.isMoving = false;
                            anim.anim.SetBool("isMoving", anim.isMoving);

                        }

                        StartCoroutine(AttackSubTower());
                        Debug.Log("서브타워 발견");
                    }
                    break;
                case "MainTower":
                    if (gameObject.activeSelf)
                    {
                        if (!isAttacking)
                        {
                            isAttacking = true;
                            anim.isMoving = false;
                            anim.anim.SetBool("isMoving", anim.isMoving);

                        }

                        Debug.Log("이동을 멈출게요.");
                        StartCoroutine(AttackTower());
                    }
                    break;
                default:
                    break;
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other == currentHit)
        {
            currentHit = null;

            switch (other.tag)
            {
                case "SubTower":
                    if (gameObject.activeSelf)
                    {
                        if (isAttacking)
                        {
                            isAttacking = false;
                            anim.isMoving = true;
                            anim.anim.SetBool("isMoving", anim.isMoving);
                        }

                        StopAllCoroutines(); // 해당 서브타워 공격 중단
                        Debug.Log("서브타워 파괴");
                    }
                    break;
                case "MainTower":
                    if (gameObject.activeSelf)
                    {
                        if (isAttacking)
                        {
                            isAttacking = false;
                            anim.isMoving = true;
                            anim.anim.SetBool("isMoving", anim.isMoving);
                        }

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
            anim.anim.SetTrigger(anim.Attack);
            Invoke("MainTowerAttack", 1.2f);
            Debug.Log("공격완료!");
            yield return new WaitForSeconds(attackTime);
        }
    }

    private void MainTowerAttack()
    {
        GameManager.instance.health -= enemy.damage;
        AudioManager.instance.PlaySfx(AudioManager.sfx.EnemyAttack);
    }

    IEnumerator AttackSubTower()
    {
        while (true)
        {
            anim.anim.SetTrigger(anim.Attack);
            //Invoke("SubTowerAttack", 1.2f);
            if (currentHit != null)
            {
                SubTower towerScript = currentHit.GetComponent<SubTower>();
                if (towerScript != null)
                {
                    towerScript.BeDamaged(enemy.damage);
                    AudioManager.instance.PlaySfx(AudioManager.sfx.EnemyAttack);
                }
                Debug.Log("공격완료!");
            }
            yield return new WaitForSeconds(attackTime);
        }
    }

    private void SubTowerAttack()
    {

    }
}
