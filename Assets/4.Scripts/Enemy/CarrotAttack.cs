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
        if (hit != null) // 객체가 담겼는지 확인
        {
            currentHit = hit;
            if (hit.CompareTag("MainTower") && !attacking) // 메인타워에 충돌했을 때
            {
                enemy.currentTargetTower = hit.transform;
                anim.anim.SetTrigger(anim.Attack);
                anim.anim.SetTrigger("Attack2");
                Debug.Log("메인타워 공격");
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
                AudioManager.instance.PlaySfx(AudioManager.sfx.CarrotAttack);
                GameManager.instance.health -= enemy.damage; // SubTower가 아닌 다른 오브젝트를 공격할 때는 메인타워의 체력을 감소시킴
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
