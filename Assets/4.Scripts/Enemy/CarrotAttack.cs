using System.Collections;
using UnityEngine;

public class CarrotAttack : MonoBehaviour
{

    AnimationController anim;
    public float moveSpeed = 5;
    public bool detection = false;
    public bool attacking = false;
    public bool attack = false;
    public float attackTimer = 0.1f;
    public float waitingTime = 0.5f;
    Enemy enemy;

    Collider2D currentHit;

    private void Start()
    {
        anim = GetComponent<AnimationController>();
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (detection)
        {
            transform.position = Vector2.Lerp(transform.position, enemy.currentTargetTower.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit != null) // ��ü�� ������ Ȯ��
        {
            currentHit = hit;
            if (hit.CompareTag("MainTower") && !attacking) // ����Ÿ���� �浹���� ��
            {
                enemy.currentTargetTower = hit.transform;
                anim.anim.SetTrigger(anim.Attack);
                anim.anim.SetTrigger("Attack2");
                Debug.Log("����Ÿ�� ����");
                attack = true;
                Invoke("DetectionTower", 0.5f);
                StartCoroutine(TakeDamage());
            }
        }
    }

    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(waitingTime);
        while (attack)
        {
            if (currentHit != null)
            {
                GameManager.instance.health -= enemy.damage; // SubTower�� �ƴ� �ٸ� ������Ʈ�� ������ ���� ����Ÿ���� ü���� ���ҽ�Ŵ
            }
            yield return new WaitForSeconds(attackTimer);
        }
    }


    private void DetectionTower()
    {
        detection = true;
        attacking = true;
    }

    private void OnEnable()
    {
        detection = false;
        attacking = false;
    }
}
