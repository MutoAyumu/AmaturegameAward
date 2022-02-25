using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("各種ステータス")]
    [SerializeField, Tooltip("EnemyのHP")]
    int _enemyHp = 2;

    [Header("GameObject")]
    [SerializeField, Tooltip("死んだときのプレハブ")]
    GameObject _deathPrefab;

    [Header("とりあえず参照したいやつ")]
    [SerializeField] EnemyMove _enemyMove;

    /// <summary>
    /// 呼び出すとダメージを与える
    /// </summary>
    public void Damage()
    {
        Debug.Log($"{gameObject.name}にダメージを与えた");

        _enemyHp--;
        _enemyMove.KnockBack();

        if (_enemyHp == 0)
        {
            EnemyDeath();
        }
    }

    /// <summary>
    /// HPがゼロになると呼ばれる
    /// 死ぬときの処理
    /// </summary>
    void EnemyDeath()
    {
        Debug.Log("EnemyDeathが呼び出された");

        if (_deathPrefab)
        {
            var go = Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }
}
