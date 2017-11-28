using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossState
{
    void Execute();
    void Enter(BossManager boss);
    void Exit();
    void OnTriggerEnter(Collider2D collision);
}
