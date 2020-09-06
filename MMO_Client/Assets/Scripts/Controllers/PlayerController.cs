using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    public Grid _grid;
    float _speed = 5.0f;

    Vector3Int _cellPos = Vector3Int.zero;
    MoveDirection _moveDir = MoveDirection.None;
    bool _isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = _grid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.0f);
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionInput();
        UpdateMovingStatus();
        UpdatePosition();
    }

    //
    // Summary:
    //     이동 가능한 상태일 때 실제 좌표를 이동
    //     캐릭터 객체가 이동할 Grid 목표 위치 좌표
    void UpdateMovingStatus()
    {
        if (!_isMoving)
        {
            switch (_moveDir)
            {
                case MoveDirection.None:
                    break;
                case MoveDirection.Up:
                    _cellPos += Vector3Int.up;
                    break;
                case MoveDirection.Down:
                    _cellPos += Vector3Int.down;
                    break;
                case MoveDirection.Left:
                    _cellPos += Vector3Int.left;
                    break;
                case MoveDirection.Right:
                    _cellPos += Vector3Int.right;
                    break;
            }
            _isMoving = true;
            _moveDir = MoveDirection.None;
        }
    }

    //
    // Summary:
    //     캐릭터가 Grid상에서 한 칸 움직이게 하는 함수
    //     destPos : 캐릭터 스프라이트가 표시될 위치
    void UpdatePosition()
    {
        if (_isMoving == false)
            return;

        Vector3 destPos = _grid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.0f);
        Vector3 moveDir = destPos - transform.position;

        float dist = moveDir.magnitude;

        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            _isMoving = false;
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            _isMoving = true;
        }

    }

    //
    // Summary:
    //     키보드 입력을 통해 캐릭터의 이동방향을 결정하는 함수
    void GetDirectionInput()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _moveDir = MoveDirection.Up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _moveDir = MoveDirection.Down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _moveDir = MoveDirection.Left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _moveDir = MoveDirection.Right;
            }
        }
        else
        {
            _moveDir = MoveDirection.None;
        }
    }
}
