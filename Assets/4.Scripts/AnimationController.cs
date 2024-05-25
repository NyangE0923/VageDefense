using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public string Moving = "Move";
    public string Attack = "Attack";
    public string Attack2 = "Attack2";
    public string Idle = "Idle";

    public bool isMoving = false;
    public bool isIdle;
    public bool isAttacking = true;

    public Animator anim;
    public SpriteRenderer sr;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void FlipX(Transform _target, Transform _enemy)
    {
        sr.flipX = _target.position.x < _enemy.position.x;
    }
}
