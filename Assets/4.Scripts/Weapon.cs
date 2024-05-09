using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Info")]
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float duration;
    public float durationTimer;


    private float LaunchSpeed;
    private float cornAttackTimer;
    private float attackTimer;
    const float timerZero = 0;
    CornTower corn;
    Tower tower;

    public bool canAttack = false;

    private void Awake()
    {
        corn = GetComponentInParent<CornTower>();
        tower = GetComponentInParent<Tower>();
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
                attackTimer += Time.deltaTime;

                if (GameManager.instance.towerSelect.towerType == SelectTower.TowerType.DefalutSelect)
                {
                    if (Input.GetMouseButtonDown(1) && attackTimer > LaunchSpeed)
                    {
                        attackTimer = timerZero;
                        Fire();
                        tower.Idle();
                    }
                }
                break;
            case 1:
                cornAttackTimer += Time.deltaTime;

                if (cornAttackTimer > LaunchSpeed)
                {
                    cornAttackTimer = timerZero;
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
                LaunchSpeed = 0.2f;
                break;
            case 1:
                LaunchSpeed = 1f;
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

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;

        bullet.rotation = Quaternion.FromToRotation(Vector3.left, dir); //회전 방향

        bullet.GetComponent<Bullet>().Init(damage, count, dir, 15f, duration, durationTimer);
    }

    void Fire()
    {
        tower.Attacking();

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        pos = pos.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.left, pos);
        bullet.GetComponent<Bullet>().Init(damage, count, pos, 25, duration, durationTimer);
    }
}
