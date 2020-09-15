using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : CreatureController
{
    Coroutine _coSkill;
    bool _rangeSkill = false;

    protected override void Init()
    {
        base.Init();
    }

    protected override void UpdateAnimation()
    {
        //만약 State, Dir의 set이 호출되면 애니메이션 수정
        if (State == CreatureState.Idle)
        {
            if (_moveDir != MoveDirection.None)
            {
                return;
            }

            switch (_lastDir)
            {
                case MoveDirection.Up:
                    _animator.Play("IDLE_BACK");
                    _sprite.flipX = false;
                    break;
                case MoveDirection.Down:
                    _animator.Play("IDLE_FRONT");
                    _sprite.flipX = false;
                    break;
                case MoveDirection.Left:
                    _animator.Play("IDLE_RIGHT");
                    //The player looks the left, must be flipped horizontally.
                    _sprite.flipX = true;
                    break;
                case MoveDirection.Right:
                    _animator.Play("IDLE_RIGHT");
                    _sprite.flipX = false;
                    break;
            }
        }
        else if (State == CreatureState.Moving)
        {

            switch (_moveDir)
            {
                //Walking Animation
                case MoveDirection.Up:
                    _animator.Play("WALK_BACK");
                    _sprite.flipX = false;
                    break;
                case MoveDirection.Down:
                    _animator.Play("WALK_FRONT");
                    _sprite.flipX = false;
                    break;
                case MoveDirection.Left:
                    _animator.Play("WALK_RIGHT");
                    //The player looks the left, must be flipped horizontally.
                    _sprite.flipX = true;
                    break;
                case MoveDirection.Right:
                    _animator.Play("WALK_RIGHT");
                    _sprite.flipX = false;
                    break;
            }
        }
        else if (State == CreatureState.Skill)
        {
            //공격할 땐 _lastDir로 해야 한다.
            switch (_lastDir)
            {
                //Skill Animation
                case MoveDirection.Up:
                    _animator.Play(_rangeSkill ? "ATTACK_WEAPON_BACK" : "ATTACK_BACK");
                    _sprite.flipX = false;
                    break;
                case MoveDirection.Down:
                    _animator.Play(_rangeSkill ? "ATTACK_WEAPON_FRONT" : "ATTACK_FRONT");
                    _sprite.flipX = false;
                    break;
                case MoveDirection.Left:
                    _animator.Play(_rangeSkill ? "ATTACK_WEAPON_RIGHT" : "ATTACK_RIGHT");
                    //The player looks the left, must be flipped horizontally.
                    _sprite.flipX = true;
                    break;
                case MoveDirection.Right:
                    _animator.Play(_rangeSkill ? "ATTACK_WEAPON_RIGHT" : "ATTACK_RIGHT");
                    _sprite.flipX = false;
                    break;
            }
        }
        else if (State == CreatureState.Dead)
        {
            //Maybe?
        }
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
            // _coSkill = StartCoroutine("CoStartPunch");
            _coSkill = StartCoroutine("CoStartArrow");
        }
    }

    IEnumerator CoStartPunch()
    {
        GameObject go = Managers.Object.Find(GetFrontCellPos());

        if (go != null)
        {
            Debug.Log(go.name);
            CreatureController cc = go.GetComponent<CreatureController>();
            if (cc != null)
                cc.OnDamaged();
        }

        _rangeSkill = false;

        yield return new WaitForSeconds(0.5f);
        State = CreatureState.Idle;
        _coSkill = null;
    }

    IEnumerator CoStartArrow()
    {
        GameObject arrow = Managers.Resource.Instantiate("Creature/Arrow");
        ArrowController ac = arrow.GetComponent<ArrowController>();
        ac.Dir = _lastDir;
        ac.CellPos = CellPos;

        _rangeSkill = true;

        yield return new WaitForSeconds(0.3f);
        State = CreatureState.Idle;
        _coSkill = null;

    }
}
