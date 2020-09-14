using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{
    protected override void Init()
    {
        base.Init();
    }

    protected override void UpdateController()
    {
        // GetDirectionInput();
        base.UpdateController();
    }

    //
    // Summary:
    //     키보드 입력을 통해 캐릭터의 이동방향을 결정하는 함수
    void GetDirectionInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            Dir = MoveDirection.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = MoveDirection.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Dir = MoveDirection.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = MoveDirection.Right;
        }
        else
        {
            Dir = MoveDirection.None;
        }
    }
}
