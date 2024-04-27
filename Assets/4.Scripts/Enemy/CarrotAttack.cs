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
        if (hit != null) //객체가 담겼는지 확인
        {
            if (hit.CompareTag("MainTower") && !attacking) //해당 객체가 MainTower를 가지고 있다면
            {
                anim.anim.SetTrigger(anim.Attack);
                anim.anim.SetTrigger("Attack2");
                Debug.Log("얘 타워 가지고 있어");
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
            yield return new WaitForSeconds(attackTimer);
            GameManager.Instance.health -= enemy.damage;

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
