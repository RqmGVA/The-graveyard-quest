using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IBossState
{

    private BossManager boss;
    private float idleTimer = 0f;
    private float idleDuration = 3.5f;

    public void Enter(BossManager boss)
    {
        this.boss = boss;
    }

    public void Execute()
    {
        Idle();
        if (boss.Target != null)
        {
            boss.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D collision)
    {

    }

    private void Idle()
    {
        boss.bossAnimator.SetFloat("Speed", 0);

        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            boss.ChangeState(new PatrolState());
        }
    }
}
