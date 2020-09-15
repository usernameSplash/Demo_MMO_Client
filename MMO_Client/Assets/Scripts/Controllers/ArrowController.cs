using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class ArrowController : CreatureController
{
    protected override void Init()
    {
        base.Init();
        _speed = 15.0f;

        switch (Dir)
        {
            case MoveDirection.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case MoveDirection.Down:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case MoveDirection.Left:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case MoveDirection.Right:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }
    }

    protected override void UpdateController()
    {
        switch (State)
        {
            case CreatureState.Idle:
                UpdateIdle();
                break;
            case CreatureState.Moving:
                UpdateMoving();
                break;
        }
    }

    protected override void UpdateAnimation() { }

    protected override void UpdateIdle()
    {
        if (State == CreatureState.Idle && _moveDir != MoveDirection.None)
        {
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
            if (Managers.Map.CanMove(destPos))
            {
                GameObject go = Managers.Object.Find(destPos);
                if (go == null)
                {
                    CellPos = destPos;
                }
                else
                {
                    Debug.Log(go.name);
                    CreatureController cc = go.GetComponent<CreatureController>();
                    if (cc != null)
                        cc.OnDamaged();

                    Managers.Resource.Destroy(gameObject);
                }
            }
            else
            {
                Managers.Resource.Destroy(gameObject);
            }
        }
    }
}
