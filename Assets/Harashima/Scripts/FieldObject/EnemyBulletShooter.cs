using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using System;

public class EnemyBulletShooter : EnemyAttack
{
    [SerializeField, Tooltip("自身のコンポーネント")]
    EnemyMove _enemyMove = null;
    [SerializeField, Tooltip("自身のコンポーネント")]
    SpriteRenderer _spriteRenderer = null;

    [SerializeField, Tooltip("攻撃パターンが変わる距離")]
    float _attackPatternDistance = 5f;

    [Header("射撃")]
    [SerializeField, Tooltip("ストレート弾プレハブ")]
    GameObject _straightBulletPrefab = null;
    [SerializeField, Tooltip("ホーミング弾プレハブ")]
    GameObject _homingBulletPrefab = null;
    [SerializeField, Tooltip("左の弾の発射位置")]
    Transform _leftMuzzle = null;
    [SerializeField, Tooltip("左の弾の発射位置")]
    Transform _rightMuzzle = null;
    [SerializeField, Tooltip("射撃する際に止まる時間")]
    float _stopTime = 1f;
    [SerializeField, Tooltip("ストレート弾が発射される弾の数")]
    int _straightBulletValue = 3;
    [SerializeField, Tooltip("ホーミング弾が発射される弾の数")]
    int _homingBulletValue = 1;
    [SerializeField, Tooltip("弾が発射される間隔")]
    float _bulletInterval = 0.3f;

    [Header("爆弾魔法陣")]
    [SerializeField, Tooltip("爆弾魔法陣が出現する距離")]
    float _bomPosDistans = 1.5f;
    [SerializeField, Tooltip("魔法陣爆弾のプレハブ")]
    GameObject _mahojinPrefab = null;
    [SerializeField, Tooltip("魔法陣爆弾から出現する弾のプレハブ")]
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


        //魔法陣のアニメーションのイベントを発行
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
