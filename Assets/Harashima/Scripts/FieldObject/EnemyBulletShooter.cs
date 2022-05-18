using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBulletShooter : EnemyAttack
{
    [SerializeField,Tooltip("�X�g���[�g�e�v���n�u")]
    GameObject _straightBulletPrefab = null;
    [SerializeField, Tooltip("�z�[�~���O�e�v���n�u")]
    GameObject _homingBulletPrefab = null;
    [SerializeField,Tooltip("���̒e�̔��ˈʒu")]
    Transform _leftMuzzle = null;
    [SerializeField, Tooltip("���̒e�̔��ˈʒu")]
    Transform _rightMuzzle = null;
    [SerializeField, Tooltip("�U���p�^�[�����ς�鋗��")]
    float _attackPatternDistance = 5f;

    [SerializeField]
    EnemyMove _enemyMove = null;
    [SerializeField]
    SpriteRenderer _spriteRenderer = null;
    [SerializeField]
    float _stopTime = 1f;
    protected override void OnUpdate()
    {
        if (_rb.velocity == Vector2.zero)
        {
            _timer += Time.deltaTime;
        }

        float dis = Vector3.Distance(this.transform.position,CharacterManager.Instance.PlayerPosition(this.transform).position);
        if (_attackTime <= _timer && dis > _attackPatternDistance)
        {
            StartCoroutine(_enemyMove.StopMove(_stopTime));
            Shoot();
            _timer = 0;
        }
        else if (_attackTime <= _timer )
        {
            StartCoroutine(_enemyMove.StopMove(_stopTime));
            Attack();
            _timer = 0;
        }

    }
    void Shoot()
    {
        int random = Random.Range(0, 2);
        if (!_spriteRenderer.flipX)
        {
            _leftMuzzle.gameObject.SetActive(true);
            DOVirtual.DelayedCall(_stopTime, () =>
            {
                _leftMuzzle.gameObject.SetActive(false);
            });
            if (random == 0)
            {
                Instantiate(_straightBulletPrefab, _leftMuzzle.transform.position, Quaternion.identity, this.transform);
            }
            else
            {
                Instantiate(_homingBulletPrefab, _leftMuzzle.transform.position, Quaternion.identity, this.transform);
            }
        }
        else
        {
            _rightMuzzle.gameObject.SetActive(true);
            DOVirtual.DelayedCall(_stopTime, () =>
            {
                _rightMuzzle.gameObject.SetActive(false);
            });
            if (random == 0)
            {
                Instantiate(_straightBulletPrefab, _rightMuzzle.transform.position, Quaternion.identity, this.transform);
            }
            else
            {
                Instantiate(_homingBulletPrefab, _rightMuzzle.transform.position, Quaternion.identity, this.transform);
            }
        }
        
      

    }
}
