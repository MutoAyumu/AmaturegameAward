using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Tooltip("攻撃判定を行うコライダー")]
    Collider2D _attackCol;
    [SerializeField, Tooltip("攻撃するまでの時間")]
    float _attackTime = 1;
    [SerializeField, Tooltip("AudioClip")]
    AudioClip _audio;
    [Header("とりあえず参照したいやつ")]
    [SerializeField]
    Rigidbody2D _rb;
    [SerializeField]
    Animator _anim;

    ContactFilter2D _filter;
    List<Collider2D> _result = new List<Collider2D>(5);

    int _count = 0;
    float _timer = 0;

    Vector2 _dir = new Vector2(0, 0);

    void Update()
    {
        //Playerの近くにいる状態で、_attackTimeの時間がたつと攻撃する
        if(_rb.velocity == _dir)
        {
            _timer += Time.deltaTime;
            if(_attackTime <= _timer)
            {
                Attack();
                _timer = 0;
            }
        }
        else
        {
            _timer = 0;
        }
    }

    /// <summary>
    /// Enemyの攻撃用の関数
    /// </summary>
    public void Attack()
    {
        if(_audio)
        {
            SoundManager.Instance.SoundPlay(_audio);
        }

        if(_anim)
        {
            _anim.SetTrigger("Attack");
            Debug.Log($"{gameObject.name}の攻撃");
        }

        //[ToDo] ContactFilter2DをSerializeすることでLayerMaskを指定できるので、余裕があればする
        _count = _attackCol.OverlapCollider(_filter, _result);
        _result.ForEach(go => go.GetComponent<PlayerHp>()?.Damage());
    }
}
