using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///モブから出る弾の基底コントローラークラス
/// </summary>
public class EnemyBulletControllerBase : MonoBehaviour
{
    [Header("このオブジェクトのコンポーネント")]
    [SerializeField, Tooltip("RigidBody")]
    protected Rigidbody2D _rb = default;


    [Header("パラメータ")]
    [SerializeField, Tooltip("弾のスピード")]
    protected float _speed = 4f;
    [SerializeField, Tooltip("弾のダメージ")]
    protected int _damage = 1;
    [SerializeField, Tooltip("弾が消えるまでの時間")]
    protected float _destroyTime = 4f;
    protected Transform _playerPos = null;

    public virtual void OnStart()
    {

    }
    private void Start()
    {
        _playerPos = CharacterManager.Instance.PlayerPosition(this.transform);
        Vector2 v = _playerPos.position - this.transform.position;
        _rb.velocity = v.normalized * _speed;
        OnStart();
    }



    private void Update()
    {
        Move();
    }

    public virtual void Move()
    {

    }

    PlayerHP _cashPlayerHP = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerHP>(out PlayerHP hp))
        {
            _cashPlayerHP = hp;
            OnTriggerMethod();
            Destroy(this.gameObject);
        }

    }

    protected virtual void OnTriggerMethod()
    {
        _cashPlayerHP?.Damage();
    }
}
