using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingBullet : EnemyBulletControllerBase
{
    [SerializeField,Tooltip("���񑬓x")]
    float _rotSpeed = 3.0f;

    /// �ړ��p�x
    float Direction
    {
        get { return Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg; }
    }

    /// �p�x�Ƒ��x����ړ����x��ݒ肷��
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

        // �摜�̊p�x���ړ������Ɍ�����
        var renderer = GetComponent<SpriteRenderer>();
        renderer.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Direction));

        // �^�[�Q�b�g���W���擾
        _playerPos = CharacterManager.Instance.PlayerPosition(this.transform);
        Vector3 now = transform.position;
        // �ړI�ƂȂ�p�x���擾����
        var d = _playerPos.position - now;
        var targetAngle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        // �p�x�������߂�
        var deltaAngle = Mathf.DeltaAngle(Direction, targetAngle);
        var newAngle = Direction;
        if (Mathf.Abs(deltaAngle) < _rotSpeed)
        {
            // ���񑬓x�������p�x���Ȃ̂ŉ������Ȃ�
        }
        else if (deltaAngle > 0)
        {
            // �����
            newAngle += _rotSpeed ;
        }
        else
        {
            // �E���
            newAngle -= _rotSpeed;
        }

        // �V�������x��ݒ肷��
        SetVelocity(newAngle, _speed);
    }
}
