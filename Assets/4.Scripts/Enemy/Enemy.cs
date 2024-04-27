using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy info")]
    public float health;
    public float maxHealth;
    public bool isLive;
    public int mineral;
    public int vitamin;
    public int dietaryFiber;
    public float damage;

    [Header("Enemy Component")]
    public RuntimeAnimatorController controller;
    public Rigidbody2D rb;
    public AnimationController anim;

    [Header("Move info")]
    public List<Transform> towers = new List<Transform>();
    public float moveSpeed = 3;
    public CircleCollider2D detectionDistance;
    public bool isMovingToTower = true;
    public Transform currentTargetTower;

    WaitForFixedUpdate wait;
    SpriteRenderer sr;

    private void Start()
    {
        detectionDistance = GetComponentInChildren<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<AnimationController>();
        sr = GetComponentInChildren<SpriteRenderer>();

        foreach (Transform tower in towers)
        {
            MainTower towerScript = tower.GetComponent<MainTower>();
            if (towerScript != null)
            {
                towerScript.OnTowerDisabled += HandleTowerDisabled;
            }
        }
    }
    private void FixedUpdate()
    {
        EnemyMove();
    }

    public void HandleTowerDisabled(Transform disabledTower)
    {
        if (towers.Contains(disabledTower))
        {
            towers.Remove(disabledTower);
            if (currentTargetTower == disabledTower)
            {
                currentTargetTower = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainTower") && !towers.Contains(other.transform))
        {
            towers.Add(other.transform);
        }

        if (other != null)
        {
            if (other.CompareTag("Bullet"))
            {
                health -= other.GetComponent<Bullet>().damage;

                if (health > 0)
                {
                }
                else
                {
                    Dead();
                    ResourcesDrop();

                }
            }
        }
    }

    private void ResourcesDrop()
    {
        GameManager.Instance.vitamin += vitamin;
        GameManager.Instance.mineral += mineral;
        GameManager.Instance.dietaryFiber += dietaryFiber;
    }

    private void EnemyMove()
    {
        if (!isLive || !isMovingToTower)
            return;

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

        if (nearestTower != null)
        {
            if (shortestDistance >= detectionDistance.radius)
            {
                isMovingToTower = true;
                currentTargetTower = nearestTower;

                Vector2 dirVec = nearestTower.position - transform.position;
                Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + nextVec);
                rb.velocity = Vector2.zero;
                anim.FlipX(nearestTower, transform);
            }
        }
    }

    private void ArrivedAtTower()
    {
        isMovingToTower = false; // 타워를 향해 이동 중이 아니라고 설정
    }

    //스크립트가 활성화 되면 tower의 rigidbody2D 컴포넌트를 게임매니저를 통해 가져온다.
    private void OnEnable()
    {
        towers = new List<Transform>(GameObject.FindGameObjectsWithTag("MainTower").Select(tower => tower.transform));
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
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
}
