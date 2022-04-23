using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDamage : MonoBehaviour, IDamage
{
    [Header("各種ステータス")]
    [SerializeField, Tooltip("EnemyのHP")]
    int _enemyHp = 30;
    [SerializeField]
    Slider _enemyHpSlider;
    [SerializeField] bool death = false;

    [Header("GameObject")]
    [SerializeField, Tooltip("死んだときのプレハブ")]
    GameObject _deathPrefab;

    void Start()
    {
        _enemyHpSlider.maxValue = _enemyHp;
        _enemyHpSlider.value = _enemyHp;
    }

    void Update()
    {
        if(death)
        {
            EnemyDeath();
        }
    }

    /// <summary>
    /// 呼び出すとダメージを与える
    /// </summary>
    public void Damage()
    {
        Debug.Log($"{gameObject.name}にダメージを与えた");

        _enemyHpSlider.value = --_enemyHp;

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
        Destroy(gameObject);

        if (_deathPrefab)
        {
            var go = Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }
}
