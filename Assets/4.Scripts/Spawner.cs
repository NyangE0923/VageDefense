using UnityEngine;



public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public SpawnTime[] spawn;

    [SerializeField] private int level;
    [SerializeField] float spawnTimer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        spawnTimer += Time.deltaTime;
        //소수점 아래는 버리고 Int형으로 바꾸는 함수 FloorToInt 반올림 하는 함수는 CeilToInt
        //즉 10초마다 레벨이 1씩 오르는 형태
        level = Mathf.FloorToInt(GameManager.Instance.gameTime / 10f);

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
                    GameObject enemy = GameManager.Instance.pool.Get(0);
                    enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
                    enemy.GetComponent<Enemy>().Init(spawnData[0]);
                    return;
                }

            case 1:
                {
                    if (Random.value < .1f)
                    {
                        GameObject enemy = GameManager.Instance.pool.Get(0);
                        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
                        enemy.GetComponent<Enemy>().Init(spawnData[0]);

                        return;
                    }

                    if (Random.value < .9f)
                    {
                        GameObject enemy = GameManager.Instance.pool.Get(1);
                        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
                        enemy.GetComponent<Enemy>().Init(spawnData[1]);

                        return;
                    }
                    break;
                }
            default:
                {
                    GameObject enemy = GameManager.Instance.pool.Get(0);
                    enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
                    enemy.GetComponent<Enemy>().Init(spawnData[0]);
                    break;
                }
        }
    }
}

[System.Serializable]
public class SpawnData
{
    public int health;
    public float damage;
    public int vitamin;
    public int mineral;
    public int dietaryFiber;
    public float speed;
}

[System.Serializable]
public class SpawnTime
{
    public float spawnTime;

}
