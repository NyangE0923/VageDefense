using System.Collections;
using UnityEngine;



public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public SpawnTime[] spawn;
    public float levelTime = 60;

    int BossSpawning = 15;

    public int level; //현재 레벨
    [SerializeField] float spawnTimer;  //적 생성 타이머

    private bool bossMonsterSpawned = false;
    private GameObject bossMonster;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / levelTime); //게임 진행 시간에 따른 레벨

        if (spawnTimer > spawn[level].spawnTime)
        {
            EnemySpawn();
            spawnTimer = 0;
        }
    }

    private void EnemySpawn()
    {
        switch (level)
        {
            case 0:
                {
                    SpawnEnemy(0, 0); //감자 생성 메서드
                    return;
                }

            case 1:
                {
                    if (Random.value < .2f)
                    {
                        SpawnEnemy(1, 1); //당근 생성 메서드

                        return;
                    }

                    if (Random.value < .8f)
                    {
                        SpawnEnemy(0, 0); //감자 생성 메서드

                        return;
                    }
                    break;
                }
            case 2:
                {
                    if(Random.value < .3f)
                    {
                        SpawnEnemy(1, 1);
                    }
                    if(Random.value < .7f)
                    {
                        SpawnEnemy(0, 0);
                    }
                    break;
                }
            case 3:
                {
                    if(Random.value < .1f)
                    {
                        SpawnEnemy(0, 3);
                    }
                    if (Random.value < .3f)
                    {
                        SpawnEnemy(1, 1);
                    }
                    if (Random.value < .6f)
                    {
                        SpawnEnemy(0, 0);
                    }
                    break;
                }
            case 4:
                {
                    if (Random.value < .2f)
                    {
                        SpawnEnemy(0, 3);
                    }
                    if (Random.value < .3f)
                    {
                        SpawnEnemy(1, 1);
                    }
                    if (Random.value < .5f)
                    {
                        SpawnEnemy(0, 0);
                    }
                    break;
                }
            case 5:
                {
                    if (Random.value < .2f)
                    {
                        SpawnEnemy(0, 3);
                    }
                    if (Random.value < .3f)
                    {
                        SpawnEnemy(1, 1);
                    }
                    if (Random.value < .5f)
                    {
                        SpawnEnemy(0, 0);
                    }
                    break;
                }
            case 6:
                {
                    if (Random.value < .05f)
                    {
                        SpawnEnemy(1, 4); //스페셜 당근
                    }
                    if (Random.value < .2f)
                    {
                        SpawnEnemy(0, 3); //스페셜 감자
                    }
                    if (Random.value < .25f)
                    {
                        SpawnEnemy(1, 1);
                    }
                    if (Random.value < .5f)
                    {
                        SpawnEnemy(0, 0);
                    }
                    break;
                }
            case 7:
                {
                    if (Random.value < .1f)
                    {
                        SpawnEnemy(1, 4); //스페셜 당근
                    }
                    if (Random.value < .25f)
                    {
                        SpawnEnemy(0, 3); //스페셜 감자
                    }
                    if (Random.value < .25f)
                    {
                        SpawnEnemy(1, 1);
                    }
                    if (Random.value < .4f)
                    {
                        SpawnEnemy(0, 0);
                    }
                    break;
                }
            case 8:
                {
                    if (Random.value < .15f)
                    {
                        SpawnEnemy(1, 4); //스페셜 당근
                    }
                    if (Random.value < .3f)
                    {
                        SpawnEnemy(0, 3); //스페셜 감자
                    }
                    if (Random.value < .2f)
                    {
                        SpawnEnemy(1, 1);
                    }
                    if (Random.value < .35f)
                    {
                        SpawnEnemy(0, 0);
                    }
                    break;
                }
            case 9:
                {
                    if (Random.value < .2f)
                    {
                        SpawnEnemy(1, 4); //스페셜 당근
                    }
                    if (Random.value < .4f)
                    {
                        SpawnEnemy(0, 3); //스페셜 감자
                    }
                    if (Random.value < .1f)
                    {
                        SpawnEnemy(1, 1);
                    }
                    if (Random.value < .3f)
                    {
                        SpawnEnemy(0, 0);
                    }
                    break;
                }
            default:
                {
                    if (Random.value < .25f)
                    {
                        SpawnEnemy(1, 4); //스페셜 당근
                    }
                    if (Random.value < .5f)
                    {
                        SpawnEnemy(0, 3); //스페셜 감자
                    }
                    if (Random.value < .05f)
                    {
                        SpawnEnemy(1, 1);
                    }
                    if (Random.value < .2f)
                    {
                        SpawnEnemy(0, 0);
                    }
                    break;
                }
        }
    }

    public void SpawnEnemy(int PoolNumber, int DataNumber)
    {
        GameObject enemy = GameManager.instance.pool.Get(PoolNumber);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[DataNumber]);

        if(GetCurrentActiveEnemiesCount() >= BossSpawning)
        {
            StartCoroutine(SpawnTomato());
        }
    }

    private int GetCurrentActiveEnemiesCount() //현재 활성화 된 적의 수 반환
    {
        int count = 0;

        foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.activeSelf)
            {
                count++;
            }
        }
        return count; //Enemy 태그를 가지고 있는 수를 int형 변수로 반환
    }

    #region 토마토 생성

    private IEnumerator SpawnTomato()
    {

        yield return new WaitForSeconds(3f);

        int activeEnemiesCount = GetCurrentActiveEnemiesCount();

        if (activeEnemiesCount >= BossSpawning && !bossMonsterSpawned)
        {
            float totalHealth = 0f;
            float totalDamage = 0f;

            foreach (var enemyData in spawnData)
            {
                totalHealth += enemyData.health;
                totalDamage += enemyData.damage;
            }

            SpawnData tomatoData = new SpawnData();
            tomatoData.name = "BossEnemy : TOMATO";
            tomatoData.health = totalHealth * 1.5f;
            tomatoData.damage = totalDamage / 1.5f;
            tomatoData.vitamin = Random.Range(50, 200);
            tomatoData.mineral = Random.Range(50, 200);
            tomatoData.dietaryFiber = 5;
            tomatoData.speed = 1;

            bossMonster = GameManager.instance.pool.Get(6);
            bossMonster.transform.position = GetAveragePositionOfEnemies();
            bossMonster.GetComponent<Tomato>().Init(tomatoData);



            bossMonsterSpawned = true;

            foreach(var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if(enemy.activeSelf && enemy != bossMonster)
                {
                    enemy.SetActive(false);
                }
            }

            Invoke("BossSpawnCool", 60f);
        }
    }

    private void BossSpawnCool()
    {
        bossMonsterSpawned = false;

    }
    #endregion
    private Vector3 GetAveragePositionOfEnemies()
    {
        Vector3 sumPosition = Vector3.zero;

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            sumPosition += enemy.transform.position;
        }

        return sumPosition / GetCurrentActiveEnemiesCount(); // 현재 활성화된 적의 수를 기준으로 평균 위치 계산
    }

    //적이 10마리 이상 뭉쳐있다면 3초 뒤에 그 자리에 모든 적이 Active False가 되며
    //그 자리에 있던 적들의 체력과 공격력을 모두 합한 토마토가 생성 된다.
}

[System.Serializable]
public class SpawnData
{
    public string name;
    public float health;
    public float damage;
    public int vitamin;
    public int mineral;
    public int dietaryFiber;
    public float speed;
}

[System.Serializable]
public class SpawnTime
{
    public string level;
    public float spawnTime;
}
