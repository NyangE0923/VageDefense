using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float LaunchSpeed;
    public float cornAttackTimer;
    const float TimerZero = 0;
    CornTower corn;
    MainTower tower;

    private void Awake()
    {
        corn = GetComponentInParent<CornTower>();
        tower = GetComponentInParent<MainTower>();
    }
    private void Start()
    {
        Init();
    }
    void Update()
    {
        switch (id)
        {
            case 0:
                if (Input.GetMouseButtonDown(1))
                {
                    Fire();
                    tower.Idle();
                }
                break;
            case 1:
                cornAttackTimer += Time.deltaTime;

                if (cornAttackTimer > LaunchSpeed)
                {
                    cornAttackTimer = TimerZero;
                    AutoFire();
                    tower.Idle();
                }
                break;

        }
    }

    public void LevelUp(float _damage, int _count)
    {
        damage = _damage;
        count = _count;
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                LaunchSpeed = 0.5f;
                break;
            case 1:
                LaunchSpeed = 0.5f;
                break;
            default:
                break;
        }
    }

    void AutoFire()
    {
        if (!corn.scanner.nearestTarget)
            return;

        tower.Attacking();

        Vector3 targetPos = corn.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;

        bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir); //회전 방향

        bullet.GetComponent<Bullet>().Init(damage, count, dir, 15f);
    }

    void Fire()
    {
        tower.Attacking();

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        pos = pos.normalized;

        Transform bullet = GameManager.Instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.left, pos); //회전 방향
        bullet.GetComponent<Bullet>().Init(damage, count, pos, 25);
    }
}
