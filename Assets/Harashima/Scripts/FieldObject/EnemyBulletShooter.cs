using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShooter : EnemyAttack
{
    [SerializeField,Tooltip("�X�g���[�g�e�v���n�u")]
    GameObject _straightBulletPrefab = null;
    [SerializeField, Tooltip("�z�[�~���O�e�v���n�u")]
    GameObject _homingBulletPrefab = null;
    [SerializeField,Tooltip("�e�̔��ˈʒu")]
    Transform _muzzle = null;
    [SerializeField, Tooltip("�U���p�^�[�����ς�鋗��")]
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
