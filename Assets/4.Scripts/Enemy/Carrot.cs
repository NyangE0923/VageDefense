using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Carrot : Enemy
{
    private HashSet<Transform> registeredTowers = new HashSet<Transform>();

    protected override void DetectionTower()
    {
        foreach (var tower in GameObject.FindGameObjectsWithTag("MainTower").Select(tower => tower.transform))
        {
            if (!registeredTowers.Contains(tower))
            {
                towers.Add(tower);
                registeredTowers.Add(tower);
            }
        }
    }

    protected override void EnemyMove() // 오버라이드
    {
        if (!isLive || !isMovingToTower)
            return;

        float shortestDistance = float.MaxValue;
        Transform nearestTower = null;

        // SubTower를 탐지하지 않고 MainTower로 이동
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
}