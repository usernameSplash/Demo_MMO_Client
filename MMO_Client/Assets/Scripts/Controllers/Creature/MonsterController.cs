using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{
    protected override void Init()
    {
        base.Init();
        Dir = MoveDirection.None;
        State = CreatureState.Idle;
    }

    protected override void UpdateController()
    {
        // GetDirectionInput();
        base.UpdateController();
    }

}
