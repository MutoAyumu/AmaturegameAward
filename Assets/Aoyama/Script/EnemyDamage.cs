using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("�e��X�e�[�^�X")]
    [SerializeField, Tooltip("Enemy��HP")]
    int _enemyHp = 2;

    [Header("GameObject")]
    [SerializeField, Tooltip("���񂾂Ƃ��̃v���n�u")]
    GameObject _deathPrefab;

    [Header("�Ƃ肠�����Q�Ƃ��������")]
    [SerializeField] EnemyMove _enemyMove;

    /// <summary>
    /// �Ăяo���ƃ_���[�W��^����
    /// </summary>
    public void Damage()
    {
        Debug.Log($"{gameObject.name}�Ƀ_���[�W��^����");

        _enemyHp--;
        _enemyMove.KnockBack();

        if (_enemyHp == 0)
        {
            EnemyDeath();
        }
    }

    /// <summary>
    /// HP���[���ɂȂ�ƌĂ΂��
    /// ���ʂƂ��̏���
    /// </summary>
    void EnemyDeath()
    {
        Debug.Log("EnemyDeath���Ăяo���ꂽ");

        if (_deathPrefab)
        {
            var go = Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }
}