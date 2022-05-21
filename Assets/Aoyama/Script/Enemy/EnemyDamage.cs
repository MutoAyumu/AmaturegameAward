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
    [SerializeField, Tooltip("Enemyの初期化HP")]
    int _initialHp = 2;
    [SerializeField, Tooltip("EnemyのHP")]
    int _enemyHp = 0;

    [Header("GameObject")]
    [SerializeField, Tooltip("死んだときのプレハブ")]
    GameObject _deathPrefab;

    [SerializeField, Tooltip("AudioClip")]
    AudioClip _audio;

    [Header("とりあえず参照したいやつ")]
    [SerializeField] EnemyMove _enemyMove;
    [SerializeField] EnemyDamageText _enemyDamageText = default;
    [SerializeField] Animator _anim;
    [SerializeField] bool _testDeath = false;
    [SerializeField] bool _testDamage = false;
    [Header("音")]
    [SerializeField] CriAtomSource _criAtomSource;
    [SerializeField] string _damageS = "EnemyDeath";
    [SerializeField] string _deathS = "EnemyDeath";

    int _groupNumber;

    private void Start()
    {
        if(!_enemyDamageText)
        {
            _enemyDamageText = GetComponentInChildren<EnemyDamageText>();
        }
        InitHP();
    }

    public void InitHP()
    {
        _enemyHp = _initialHp;
    }
    private void Update()
    {
        if (_testDeath)
        {
            EnemyDeath();
        }
    }

    /// <summary>
    /// 呼び出すとダメージを与える
    /// </summary>
    public void Damage(int damage)
    {
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _damageS);

        _anim.SetTrigger("Damage");
        _enemyHp -= damage; ;
        _enemyMove.KnockBack();

        _enemyDamageText?.DamageText(damage);

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
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _deathS);

        if (_groupNumber != 0)
        {
            EnemyManager.Instance.DecreaseInNumbers(_groupNumber);
        }

        Debug.Log("EnemyDeathが呼び出された");
        //Destroy(gameObject);
        this.gameObject.SetActive(false);

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
