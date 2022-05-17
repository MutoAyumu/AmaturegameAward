using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingBullet : EnemyBulletControllerBase
{
    [SerializeField,Tooltip("旋回速度")]
    float _rotSpeed = 3.0f;

    /// 移動角度
    float Direction
    {
        get { return Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg; }
    }

    /// 角度と速度から移動速度を設定する
    void SetVelocity(float direction, float speed)
    {
        var vx = Mathf.Cos(Mathf.Deg2Rad * direction) * speed;
        var vy = Mathf.Sin(Mathf.Deg2Rad * direction) * speed;

        _rb.velocity = new Vector2(vx, vy);
    }
    public override void OnStart(){}

    public override void Move()
    {
        //_rb.velocity = (_playerPos.position - transform.position).normalized * _speed;

        // 画像の角度を移動方向に向ける
        var renderer = GetComponent<SpriteRenderer>();
        renderer.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Direction));

        // ターゲット座標を取得
        _playerPos = CharacterManager.Instance.PlayerPosition(this.transform);
        Vector3 now = transform.position;
        // 目的となる角度を取得する
        var d = _playerPos.position - now;
        var targetAngle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        // 角度差を求める
        var deltaAngle = Mathf.DeltaAngle(Direction, targetAngle);
        var newAngle = Direction;
        if (Mathf.Abs(deltaAngle) < _rotSpeed)
        {
            // 旋回速度を下回る角度差なので何もしない
        }
        else if (deltaAngle > 0)
        {
            // 左回り
            newAngle += _rotSpeed ;
        }
        else
        {
            // 右回り
            newAngle -= _rotSpeed;
        }

        // 新しい速度を設定する
        SetVelocity(newAngle, _speed);
    }
}
