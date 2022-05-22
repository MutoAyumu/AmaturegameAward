using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃判定を行うコライダー")]
    protected Collider2D _attackCol;
    [SerializeField, Tooltip("攻撃するまでの時間")]
    protected float _attackTime = 1;
    [SerializeField, Tooltip("AudioClip")]
    AudioClip _audio;
    [Header("とりあえず参照したいやつ")]
    [SerializeField]
    protected Rigidbody2D _rb;
    [SerializeField]
    protected Animator _anim;
    [Header("音")]
    [SerializeField] protected CriAtomSource _criAtomSource;
    [SerializeField] protected string _cuename = "GaAttack";

    protected ContactFilter2D _filter;
    protected List<Collider2D> _result = new List<Collider2D>(7);

    int _count = 0;
    protected float _timer = 0;

    Vector2 _dir = new Vector2(0, 0);

    void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        if (_rb.velocity == Vector2.zero)
        {
            _timer += Time.deltaTime;
        }

        if (_attackTime <= _timer)
        {
            Attack();
            _timer = 0;
        }
    }

    /// <summary>
    /// Enemyの攻撃用の関数
    /// </summary>
    public void Attack()
    {
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, _cuename);

        if (_anim)
        {
            _anim.SetTrigger("Attack");
            //Debug.Log($"{gameObject.name}の攻撃");
        }

        //[ToDo] ContactFilter2DをSerializeすることでLayerMaskを指定できるので、余裕があればする
        _count = _attackCol.OverlapCollider(_filter, _result);
        _result.ForEach(go =>
        {
            go.transform.parent?.GetComponent<PlayerHP>()?.Damage();
        });
        
    }
}
