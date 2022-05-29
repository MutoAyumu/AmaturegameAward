using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossDamage : MonoBehaviour, IDamage
{
    [Header("各種ステータス")]
    [SerializeField, Tooltip("EnemyのHP")]
    int _enemyHp = 30;
    [SerializeField]
    Slider _enemyHpSlider;
    [SerializeField] bool death = false;
    [SerializeField] bool _damage = false;

    [Header("死んだとき")]
    [SerializeField] bool _timeLineCall = true;
    [SerializeField, Tooltip("死んだときのプレハブ")]
    GameObject _deathPrefab;
    [SerializeField] UnityEvent _deathEvent;

    [Header("とりあえず参照したいやつ")]
    [SerializeField] Animator _anim;
    [SerializeField] EnemyDamageText _enemyDamageText = default;
    [SerializeField] GameObject _destroy;
 
    void Start()
    {
        _enemyHpSlider.maxValue = _enemyHp;
        _enemyHpSlider.value = _enemyHp;
    }

    void Update()
    {
        if (death)
        {
            EnemyDeath();
        }

        if(_damage)
        {
            _anim.SetTrigger("Damage");
            _damage = false;
        }
    }

    /// <summary>
    /// 呼び出すとダメージを与える
    /// </summary>
    public void Damage(int damage)
    {
        Debug.Log($"{gameObject.name}にダメージを与えた");

        _enemyDamageText?.DamageText(damage);
        _enemyHp -= damage;
        _enemyHpSlider.value = _enemyHp;

        if(_anim)
        {
            _anim.SetTrigger("Damage");
        }

        if (_enemyHp <= 0)
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
        SoundManager.Instance.CriAtomStop();
        _destroy.GetComponent<OnOffEnemy>().Decrease();
        Debug.Log("EnemyDeathが呼び出された");
        Destroy(gameObject);
        Destroy(_destroy, 2f);

        if(_timeLineCall)
        {
            _deathEvent.Invoke();
        }
        else
        {
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
        
    }

}
