using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShooter : EnemyAttack
{
    [SerializeField,Tooltip("ストレート弾プレハブ")]
    GameObject _straightBulletPrefab = null;
    [SerializeField, Tooltip("ホーミング弾プレハブ")]
    GameObject _homingBulletPrefab = null;
    [SerializeField,Tooltip("弾の発射位置")]
    Transform _muzzle = null;
    [SerializeField, Tooltip("攻撃パターンが変わる距離")]
    float _attackPatternDistance = 5f;
    protected override void OnUpdate()
    {
        if (_rb.velocity == Vector2.zero)
        {
            _timer += Time.deltaTime;
        }

        float dis = Vector3.Distance(this.transform.position,CharacterManager.Instance.PlayerPosition(this.transform).position);
        if (_attackTime <= _timer && dis>_attackPatternDistance)
        {
            Attack();
            _timer = 0;
        }
        else if(_attackTime <= _timer)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        _muzzle.gameObject.SetActive(true);
        int random = Random.Range(0,2);
        if(random==0)
        {
            Instantiate(_straightBulletPrefab, _muzzle.transform.position,Quaternion.identity,this.transform);
        }
        else
        {
            Instantiate(_homingBulletPrefab, _muzzle.transform.position, Quaternion.identity, this.transform);
        }        
    }
}
