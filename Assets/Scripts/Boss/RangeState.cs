using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeState : IBossState
{
    private float throwTimer;
    private float throwCoolDown = 2.5f;
    private bool canThrow = true;
    private BossManager boss;

    public void Enter(BossManager boss)
    {
        this.boss = boss;
    }

    public void Execute()
    {
        if (boss.Target != null)
        {
            boss.Move();
            ThrowKnife();
        }
        else
        {
            boss.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D collision)
    {

    }
    private void ThrowKnife()
    {
        throwTimer += Time.deltaTime;

        if (throwTimer >= throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0;
        }
        if (canThrow)
        {
            canThrow = false;
            boss.bossAnimator.SetTrigger("Throw");
        }
    }
}
