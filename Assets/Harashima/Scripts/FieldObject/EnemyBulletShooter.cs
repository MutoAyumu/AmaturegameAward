using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using System;

public class EnemyBulletShooter : EnemyAttack
{
    [SerializeField, Tooltip("���g�̃R���|�[�l���g")]
    EnemyMove _enemyMove = null;
    [SerializeField, Tooltip("���g�̃R���|�[�l���g")]
    SpriteRenderer _spriteRenderer = null;

    [SerializeField, Tooltip("�U���p�^�[�����ς�鋗��")]
    float _attackPatternDistance = 5f;

    [Header("�ˌ�")]
    [SerializeField, Tooltip("�X�g���[�g�e�v���n�u")]
    GameObject _straightBulletPrefab = null;
    [SerializeField, Tooltip("�z�[�~���O�e�v���n�u")]
    GameObject _homingBulletPrefab = null;
    [SerializeField, Tooltip("���̒e�̔��ˈʒu")]
    Transform _leftMuzzle = null;
    [SerializeField, Tooltip("���̒e�̔��ˈʒu")]
    Transform _rightMuzzle = null;
    [SerializeField, Tooltip("�ˌ�����ۂɎ~�܂鎞��")]
    float _stopTime = 1f;
    [SerializeField, Tooltip("�X�g���[�g�e�����˂����e�̐�")]
    int _straightBulletValue = 3;
    [SerializeField, Tooltip("�z�[�~���O�e�����˂����e�̐�")]
    int _homingBulletValue = 1;
    [SerializeField, Tooltip("�e�����˂����Ԋu")]
    float _bulletInterval = 0.3f;

    [Header("���e���@�w")]
    [SerializeField, Tooltip("���e���@�w���o�����鋗��")]
    float _bomPosDistans = 1.5f;
    [SerializeField, Tooltip("���@�w���e�̃v���n�u")]
    GameObject _mahojinPrefab = null;
    [SerializeField, Tooltip("���@�w���e����o������e�̃v���n�u")]
    GameObject _bombBulletPrefab = null;

    protected override void OnUpdate()
    {
        if (_rb.velocity == Vector2.zero)
        {
            _timer += Time.deltaTime;
        }

        float dis = Vector3.Distance(this.transform.position, CharacterManager.Instance.PlayerPosition(this.transform).position);
        if (_attackTime <= _timer && dis > _attackPatternDistance)
        {
            StartCoroutine(_enemyMove.StopMove(_stopTime));
            //Shoot();
            Bomb();
            _timer = 0;
        }
        else if (_attackTime <= _timer)
        {
            Attack();
            _timer = 0;
        }

    }
    void Shoot()
    {
        int random = UnityEngine.Random.Range(0, 2);
        if (!_spriteRenderer.flipX)
        {
            _leftMuzzle.gameObject.SetActive(true);
            DOVirtual.DelayedCall(_stopTime, () =>
            {
                _leftMuzzle.gameObject.SetActive(false);
            });
            if (random == 0)
            {
                StartCoroutine(InstantiateBullet(_straightBulletPrefab, _straightBulletValue, _leftMuzzle));
            }
            else
            {
                StartCoroutine(InstantiateBullet(_homingBulletPrefab, _homingBulletValue, _leftMuzzle));
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
                StartCoroutine(InstantiateBullet(_straightBulletPrefab, _straightBulletValue, _rightMuzzle));
            }
            else
            {
                StartCoroutine(InstantiateBullet(_homingBulletPrefab, _homingBulletValue, _rightMuzzle));

            }
        }

        IEnumerator InstantiateBullet(GameObject prefab, int bulletValue, Transform muzzle)
        {
            for (int i = 0; i < bulletValue; i++)
            {
                yield return new WaitForSeconds(_bulletInterval);
                Instantiate(prefab, muzzle.transform.position, Quaternion.identity, this.transform);
            }

        }

    }

    void Bomb()
    {

        Vector3 pos = (CharacterManager.Instance.PlayerPosition(this.transform).position * _bomPosDistans + this.transform.position) / 2;
        var go = Instantiate(_mahojinPrefab, pos, Quaternion.identity);

        ObservableStateMachineTrigger trigger =
        go.GetComponent<Animator>().GetBehaviour<ObservableStateMachineTrigger>();


        //���@�w�̃A�j���[�V�����̃C�x���g�𔭍s
        IDisposable exitState = trigger.OnStateExitAsObservable()
        .Where(b => b.StateInfo.IsName("Mahojin")) 
        .Subscribe(x =>
        {
            AnimatorStateInfo info = x.StateInfo;

            Instantiate(_bombBulletPrefab, go.transform.position, Quaternion.identity);
            Destroy(go,2f);
        }).AddTo(this);


    }
}
