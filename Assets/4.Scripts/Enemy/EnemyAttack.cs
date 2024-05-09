using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackTime = 3f;
    public Enemy enemy;
    public AnimationController anim;
    public Collider2D currentHit; // ���� �浹�� Collider2D�� �����ϴ� ����

    protected virtual void Start()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<AnimationController>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            currentHit = other; // ���� �浹�� Collider2D�� ����
            switch (other.tag)
            {
                case "SubTower":
                    if (gameObject.activeSelf)
                    {
                        StartCoroutine(AttackSubTower());
                        Debug.Log("����Ÿ�� �߰�");
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

    protected virtual void OnTriggerExit2D(Collider2D other)
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
                        StopAllCoroutines(); // �ش� ����Ÿ�� ���� �ߴ�
                        Debug.Log("����Ÿ�� �ı�");
                    }
                    break;
                case "MainTower":
                    if (gameObject.activeSelf)
                    {
                        StopAllCoroutines(); // ����Ÿ�� ���� �ߴ�
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
            Debug.Log("���ݿϷ�!");
            yield return new WaitForSeconds(attackTime);
        }
    }

    IEnumerator AttackSubTower()
    {
        while (true)
        {
            if (currentHit != null)
            {
                anim.anim.SetTrigger(anim.Attack);
                SubTower towerScript = currentHit.GetComponent<SubTower>();
                if (towerScript != null)
                {
                    towerScript.BeDamaged(enemy.damage);
                }
                Debug.Log("���ݿϷ�!");
            }
            yield return new WaitForSeconds(attackTime);
        }
    }
}
