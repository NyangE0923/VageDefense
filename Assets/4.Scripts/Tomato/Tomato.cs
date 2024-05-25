using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : Enemy
{
    TomatoAttack tomatoAttack;

    protected override void Start()
    {
        base.Start();
        tomatoAttack = GetComponent<TomatoAttack>();
    }

    protected override void EnemyMove()
    {
        if (!isLive || !isMovingToTower || !tomatoAttack.canMoving)
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

    protected override void OnDisable()
    {
        base.OnDisable();
        
    }
}
