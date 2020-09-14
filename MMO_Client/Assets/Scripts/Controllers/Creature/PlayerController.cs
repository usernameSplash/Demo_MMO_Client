using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : CreatureController
{
    Coroutine _coSkill;
    protected override void Init()
    {
        base.Init();
    }

    protected override void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Idle:
                GetDirectionInput();
                GetIdleInput();
                break;
            case CreatureState.Moving:
                GetDirectionInput();
                break;
        }
        base.UpdateController();
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = transform.position - new Vector3(0.0f, 0.0f, 10.0f);
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

    void GetIdleInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            State = CreatureState.Skill;
            _coSkill = StartCoroutine("CoStartPunch");
        }
    }

    IEnumerator CoStartPunch()
    {
        GameObject go = Managers.Object.Find(GetFrontCellPos());

        if (go != null)
        {
            Debug.Log(go.name);
        }

        yield return new WaitForSeconds(0.5f);
        State = CreatureState.Idle;
        _coSkill = null;
    }
}
