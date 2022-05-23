using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///���u����o��e�̊��R���g���[���[�N���X
/// </summary>
public class EnemyBulletControllerBase : MonoBehaviour
{
    [Header("���̃I�u�W�F�N�g�̃R���|�[�l���g")]
    [SerializeField, Tooltip("RigidBody")]
    protected Rigidbody2D _rb = default;


    [Header("�p�����[�^")]
    [SerializeField, Tooltip("�e�̃X�s�[�h")]
    protected float _speed = 4f;
    [SerializeField, Tooltip("�e�̃_���[�W")]
    protected int _damage = 1;
    [SerializeField, Tooltip("�e��������܂ł̎���")]
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
