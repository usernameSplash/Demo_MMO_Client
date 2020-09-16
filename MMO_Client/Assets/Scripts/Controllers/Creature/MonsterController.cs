using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : CreatureController
{
    Coroutine _coPatrol;
    Vector3Int _destCellPos;

    public override CreatureState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;
            base.State = value;

            if (_coPatrol != null)
            {
                StopCoroutine(_coPatrol);
                _coPatrol = null;
            }
        }
    }

    protected override void Init()
    {
        base.Init();
        Dir = MoveDirection.None;
        State = CreatureState.Idle;
    }

    protected override void UpdateIdle()
    {
        base.UpdateIdle();

        if (_coPatrol == null)
        {
            _coPatrol = StartCoroutine("CoPatrol");
        }
    }

    protected override void UpdateController()
    {
        // GetDirectionInput();
        base.UpdateController();
    }

    public override void OnDamaged()
    {
        GameObject effect = Managers.Resource.Instantiate("Effect/DeathEffect");
        effect.transform.position = transform.position;
        effect.GetComponent<Animator>().Play("DeathEffectStart");
        GameObject.Destroy(effect, 0.5f);

        Managers.Object.Remove(gameObject);
        Managers.Resource.Destroy(gameObject);
    }

    protected override void MoveToNextPos()
    {
        Vector3Int moveCellDir = _destCellPos - CellPos;

        if (moveCellDir.x > 0)
            Dir = MoveDirection.Right;
        else if (moveCellDir.x < 0)
            Dir = MoveDirection.Left;
        else if (moveCellDir.y > 0)
            Dir = MoveDirection.Up;
        else if (moveCellDir.y < 0)
            Dir = MoveDirection.Down;
        else
            Dir = MoveDirection.None;

        Vector3Int destPos = CellPos;

        switch (Dir)
        {
            case MoveDirection.Up:
                destPos += Vector3Int.up;
                break;
            case MoveDirection.Down:
                destPos += Vector3Int.down;
                break;
            case MoveDirection.Left:
                destPos += Vector3Int.left;
                break;
            case MoveDirection.Right:
                destPos += Vector3Int.right;
                break;
        }

        State = CreatureState.Moving;
        if (Managers.Map.CanMove(destPos) && Managers.Object.Find(destPos) == null)
        {
            CellPos = destPos;
        }
        else
        {
            State = CreatureState.Idle;
            Dir = MoveDirection.None;
        }
    }

    IEnumerator CoPatrol()
    {
        int waitSeconds = Random.Range(1, 4);
        yield return new WaitForSeconds(waitSeconds);

        for (int i = 0; i < 10; i++)
        {
            int xRange = Random.Range(-5, 6);
            int yRange = Random.Range(-5, 6);
            Vector3Int randPos = CellPos + new Vector3Int(xRange, yRange, 0);

            if (Managers.Map.CanMove(randPos) && Managers.Object.Find(randPos) == null)
            {
                _destCellPos = randPos;
                State = CreatureState.Moving;
                yield break;
            }
        }
        State = CreatureState.Idle;

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
            //?
        }
        else if (State == CreatureState.Dead)
        {
            //Maybe?
        }
    }
}
