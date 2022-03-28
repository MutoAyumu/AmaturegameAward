using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 鱗粉用のBullet
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class RinpunBullet : MonoBehaviour
{
    [Header("各種ステータス")]
    [SerializeField] MovePatern _patern;
    [SerializeField] float _speed = 3; 

    [Header("とりあえず参照したいやつ")]
    [SerializeField] Rigidbody2D _rb;

    enum MovePatern
    {
        right,
        left,
        up,
        down
    }

    Vector2 _dir = new Vector2();
    void Start()
    {
        switch (_patern)
        {
            case MovePatern.right:
                _dir = new Vector2(1, 0);
                return;
            case MovePatern.left:
                _dir = new Vector2(-1, 0);
                return;
            case MovePatern.up:
                _dir = new Vector2(0, 1);
                return;
            case MovePatern.down:
                _dir = new Vector2(0, -1);
                return;
        }
    }

    void Update()
    {
        _rb.velocity = _dir * _speed;
    }
}
