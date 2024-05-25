using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Info")]
    public float health;
    public float maxHealth;
    public bool isLive;
    public int mineral;
    public int vitamin;
    public int dietaryFiber;
    public float damage;
    public float moveSpeed;

    [Header("Enemy Components")]
    public RuntimeAnimatorController controller;
    public Rigidbody2D rb;
    public AnimationController anim;

    [Header("Movement")]
    public List<Transform> towers = new List<Transform>();
    public CircleCollider2D detectionDistance;
    public bool isMovingToTower = true;
    public Transform currentTargetTower;
    private HashSet<Transform> registeredTowers = new HashSet<Transform>();

    protected virtual void Start()
    {
        detectionDistance = GetComponentInChildren<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<AnimationController>();

        foreach (Transform tower in towers)
        {
            Tower towerScript = tower.GetComponent<Tower>();
            if (towerScript != null)
            {
                towerScript.OnTowerDisabled += HandleTowerDisabled;
            }
        }
    }
    protected virtual void Update()
    {
        DetectionTower();
    }
    private void FixedUpdate()
    {
        EnemyMove();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainTower") && !towers.Contains(other.transform))
        {
            towers.Add(other.transform);
        }

        if (other.CompareTag("Bullet"))
        {
            health -= other.GetComponent<Bullet>().damage;
            AudioManager.instance.PlaySfx(AudioManager.sfx.EnemyDamage);

            if (health <= 0)
            {
                Dead();
                ResourcesDrop();
            }
        }
    }
    private void ResourcesDrop()
    {
        GameManager.instance.vitamin += vitamin;
        GameManager.instance.mineral += mineral;
        GameManager.instance.dietaryFiber += dietaryFiber;
    }
    protected virtual void EnemyMove()
    {
        if (!isLive || !isMovingToTower)
            return;

        // 만약 현재 타겟 타워가 비활성화되었거나 null이라면 다음 타워를 찾음
        if (currentTargetTower == null || !currentTargetTower.gameObject.activeSelf)
        {
            currentTargetTower = null;
            FindNearestTower();
        }


        float shortestDistance = float.MaxValue;
        Transform nearestTower = null;

        foreach (Transform towerTransform in towers)
        {
            if (!towerTransform.gameObject.activeSelf)
                continue;

            float distanceToTower = Vector2.Distance(rb.position, towerTransform.position);
            if (distanceToTower < shortestDistance)
            {
                shortestDistance = distanceToTower;
                nearestTower = towerTransform;
            }
        }

        if (nearestTower != null && shortestDistance >= detectionDistance.radius)
        {
            currentTargetTower = nearestTower;
            Vector2 dirVec = nearestTower.position - transform.position;
            Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + nextVec);
            rb.velocity = Vector2.zero;
            anim.FlipX(nearestTower, transform);
        }
    }
    protected virtual void DetectionTower()
    {
        // SubTower 탐지
        foreach (var tower in GameObject.FindGameObjectsWithTag("SubTower").Select(tower => tower.transform))
        {
            if (!registeredTowers.Contains(tower))
            {
                towers.Add(tower);
                registeredTowers.Add(tower);
            }
        }

        // MainTower 탐지
        foreach (var tower in GameObject.FindGameObjectsWithTag("MainTower").Select(tower => tower.transform))
        {
            if (!registeredTowers.Contains(tower))
            {
                towers.Add(tower);
                registeredTowers.Add(tower);
            }
        }
    }
    private void HandleTowerDisabled(Transform disabledTower)
    {
        if (towers.Contains(disabledTower))
        {
            towers.Remove(disabledTower);
            if (currentTargetTower == disabledTower)
            {
                currentTargetTower = null;
                FindNearestTower();
            }
        }
        else if (registeredTowers.Contains(disabledTower))
        {
            registeredTowers.Remove(disabledTower);
        }
    }
    public void FindNearestTower()
    {
        float shortestDistance = float.MaxValue;
        Transform nearestTower = null;

        foreach (Transform towerTransform in towers)
        {
            float distanceToTower = Vector2.Distance(rb.position, towerTransform.position);
            if (distanceToTower < shortestDistance)
            {
                shortestDistance = distanceToTower;
                nearestTower = towerTransform;
            }
        }

        currentTargetTower = nearestTower;
    }

    #region 적의 상태 및 죽음
    public void Init(SpawnData data)
    {
        name = data.name;
        moveSpeed = data.speed;
        damage = data.damage;
        maxHealth = data.health;
        health = data.health;
        vitamin = data.vitamin;
        mineral = data.mineral;
        dietaryFiber = data.dietaryFiber;
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnDisable()
    {
        registeredTowers.Clear();
    }
    #endregion
    private void OnEnable()
    {
        // 기존에 등록된 타워만 다시 탐지하여 리스트에 추가
        foreach (var tower in registeredTowers)
        {
            if (!towers.Contains(tower))
            {
                towers.Add(tower);
            }
        }
        isLive = true;
        health = maxHealth;
    }
}
