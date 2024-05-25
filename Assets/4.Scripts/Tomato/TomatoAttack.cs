using System.Collections;
using UnityEngine;

public class TomatoAttack : EnemyAttack
{
    float spawnDelay = 0.2f;

    public Transform[] spawnPoint;
    public bool canMoving = true;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    protected override void Start()
    {
        base.Start();
        InvokeRepeating("SpawnCherryTomato", 0f, 3f);
    }

    private void SpawnCherryTomato()
    {
        if (!gameObject.activeSelf)
        {
            return; // 게임 오브젝트가 비활성화된 경우 함수를 즉시 종료
        }

        StartCoroutine(CherryTomatoSpawn());
    }

    IEnumerator CherryTomatoSpawn()
    {
        if (!gameObject.activeSelf)
        {
            yield break;
        }

        canMoving = false;
        anim.isIdle = true;
        anim.anim.SetBool(anim.Idle, anim.isIdle);

        int randomSpawnCount = Random.Range(1, 5);
        for (int SpawnCount = 0; SpawnCount < randomSpawnCount; SpawnCount++)
        {
            GameObject enemy = GameManager.instance.pool.Get(7);
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(new SpawnData
            {
                name = "CherryTomato",
                health = 10,
                damage = 20,
                vitamin = 0,
                mineral = 5,
                dietaryFiber = 0,
                speed = 7,
            });

            yield return new WaitForSeconds(spawnDelay);
        }
        //움직임을 관리하는 변수

        yield return new WaitForSeconds(3f);

        if (!isAttacking)
        {
            canMoving = true;
            anim.isIdle = false;
            anim.anim.SetBool(anim.Idle, anim.isIdle);
        }

    }

    #region 기본 공격 로직
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
                        isAttacking = true;
                        StartCoroutine(AttackSubTower());
                        Debug.Log("서브타워 발견");
                    }
                    break;
                case "MainTower":
                    if (gameObject.activeSelf)
                    {
                        isAttacking = true;
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
        if (other != null && other == currentHit)
        {
            currentHit = null;

            switch (other.tag)
            {
                case "SubTower":
                    if (gameObject.activeSelf)
                    {
                        isAttacking = false;
                        StopAllCoroutines(); // 해당 서브타워 공격 중단
                        Debug.Log("서브타워 파괴");
                    }
                    break;
                case "MainTower":
                    if (gameObject.activeSelf)
                    {
                        isAttacking = false;
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
            Invoke("BossMainAttack", .4f);
            Debug.Log("공격완료!");
            yield return new WaitForSeconds(attackTime);
        }
    }

    private void BossMainAttack()
    {
        GameManager.instance.health -= enemy.damage;
        AudioManager.instance.PlaySfx(AudioManager.sfx.BossAttack);
    }

    IEnumerator AttackSubTower()
    {
        while (true)
        {
            Invoke("BossSubAttack", .4f);
            yield return new WaitForSeconds(attackTime);
        }
    }

    private void BossSubAttack()
    {
        if (currentHit != null)
        {
            anim.anim.SetTrigger(anim.Attack);

            SubTower towerScript = currentHit.GetComponent<SubTower>();
            if (towerScript != null)
            {
                towerScript.BeDamaged(enemy.damage);
                AudioManager.instance.PlaySfx(AudioManager.sfx.BossAttack);
            }
            Debug.Log("공격완료!");
        }
    }

    private void OnDisable()
    {
        isAttacking = false;
        canMoving = true;
        anim.isIdle = false;
        anim.anim.SetBool(anim.Idle, anim.isIdle);
    }
    #endregion
}
