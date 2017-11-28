using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IBossState
{
    private BossManager boss;
    private float patrolTimer = 0f;
    private float patrolDuration = 10f;
    public void Enter(BossManager boss)
    {
        this.boss = boss;
    }

    public void Execute()
    {
        Patrol();
        boss.Move();
        if (boss.Target != null)
        {
            boss.ChangeState(new RangeState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D collision)
    {
        if (collision.tag == "Edge")
            boss.ChangeDirection();
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            boss.ChangeState(new IdleState());
        }
    }
}
