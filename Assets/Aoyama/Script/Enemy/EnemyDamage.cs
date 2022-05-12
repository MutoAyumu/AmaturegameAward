using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// EnemyのHP関係を管理するクラス
/// </summary>
public class EnemyDamage : MonoBehaviour, IDamage
{
    [Header("各種ステータス")]
    [SerializeField, Tooltip("EnemyのHP")]
    int _enemyHp = 2;

    [Header("GameObject")]
    [SerializeField, Tooltip("死んだときのプレハブ")]
    GameObject _deathPrefab;

    [SerializeField, Tooltip("AudioClip")]
    AudioClip _audio;

    [Header("とりあえず参照したいやつ")]
    [SerializeField] EnemyMove _enemyMove;
    [SerializeField] Animator _anim;
    [SerializeField] bool _testDeath = false;
    [SerializeField] bool _testDamage = false;
    [SerializeField] Text _damageText;

    int _groupNumber;

    private void Update()
    {
        if (_testDeath)
        {
            EnemyDeath();
        }

        //if (_testDamage)
        //{
        //    Damage();
        //    _testDamage = false;
        //}
    }

    /// <summary>
    /// 呼び出すとダメージを与える
    /// </summary>
    public void Damage(int damage)
    {
        Debug.Log($"{gameObject.name}にダメージを与えた");
        _anim.SetTrigger("Damage");
        _enemyHp -= damage; ;
        _enemyMove.KnockBack();

        //ダメージを表示      
        if (_damageText)
        {
            _damageText.gameObject.SetActive(true);
            _damageText.text = damage.ToString();
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
        if(_audio)
        {
            SoundManager.Instance.SoundPlay(_audio);
        }

        if (_groupNumber != 0)
        {
            EnemyManager.Instance.DecreaseInNumbers(_groupNumber);
        }

        Debug.Log("EnemyDeathが呼び出された");
        Destroy(gameObject);

        if (_deathPrefab)
        {
            var go = Instantiate(_deathPrefab, transform.position, Quaternion.identity);
            SpriteRenderer sp = go.GetComponent<SpriteRenderer>();
            sp.flipX = this.GetComponent<SpriteRenderer>().flipX;
        }
    }

    public void SetNumber(int i)
    {
        _groupNumber = i;
    }
}

/// <summary>
/// 敵や壊せるアイテムがプレイヤーからの攻撃を受けた時用のinterface
/// </summary>
interface IDamage
{
    void Damage(int damage);
}
