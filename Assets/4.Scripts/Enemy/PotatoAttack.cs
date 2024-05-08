using System.Collections;
using UnityEngine;

public class PotatoAttack : MonoBehaviour
{
    [SerializeField] private float attackTime = 3f;
    Enemy enemy;
    AnimationController anim;
    Collider2D currentHit; // ���� �浹�� Collider2D�� �����ϴ� ����

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<AnimationController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
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
                SubTower towerScript = currentHit.GetComponent<SubTower>();
                if (towerScript != null)
                {
                    towerScript.BeDamaged(enemy.damage);
                }
                anim.anim.SetTrigger(anim.Attack);
                Debug.Log("���ݿϷ�!");
            }
            yield return new WaitForSeconds(attackTime);
        }
    }
}
